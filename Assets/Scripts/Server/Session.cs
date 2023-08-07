using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace ServerCore
{
    public abstract class CSession
    {
        Socket _ServerSocket;

        int _Disconnect = 0;

        public RingBuf _RecvBuffer = new RingBuf(100001);

        object _SendLock = new object();
        //Send하고자 하는 패킷 임시 저장소
        Queue<ArraySegment<byte>> _SendQue = new Queue<ArraySegment<byte>>();

        // Send하고자 하는 메세지 목록
        List<ArraySegment<byte>> _SendBuffPendingList = new List<ArraySegment<byte>>();
        // Recv받을 때 필요한 버퍼 
        List<ArraySegment<byte>> _RecvBuffList = new List<ArraySegment<byte>>();

        SocketAsyncEventArgs _SendArgs = new SocketAsyncEventArgs();
        SocketAsyncEventArgs _RecvArgs = new SocketAsyncEventArgs();

        public abstract void OnConnected(EndPoint endPoint);
        public abstract void OnSend(int NumOfBytes);
        public abstract void OnDisconnected(EndPoint endPoint);

        //세션 시작할 소켓을 받아온다.
        public void Start(Socket Socket)
        {
            _ServerSocket = Socket;

            // RecvBuf에 데이터가 왓을 때 호출해줄 함수 연결
            _RecvArgs.Completed += OnRecvCompleted;
            // Send 완료시 오출해줄 함수 연결
            _SendArgs.Completed += OnSendCompleted;

            //추가로 보내고 싶은 내용이 있을때 UserToken에 담아서 보내면 됨
            //RecvArgs.UserToken =                        

            RegisterRecv();
        }

        void Clear()
        {
            //SendLock 잡고 진입
            lock (_SendLock)
            {
                //SendQue , SendBuffPenginList 정리
                _SendQue.Clear();
                _SendBuffPendingList.Clear();

                _SendQue = null;
                _SendBuffPendingList = null;
            }
        }

        //하나의 쓰레드에서 Send만 하면 상관 없지만
        //다수의 쓰레드에서 Send를 할때 _SendArgs를 하나만 사용하게 된다면 SetBuffer하는 부분에서 새로 들어온 버퍼로 셋팅을 해주게 되므로
        //이상한 내용을 보낼수 잇는 부분이 생긴다. _SendArgs.SetBuffer(SendBuff) 보낼 SendBuff가 계속 덮어씌워지기 때문
        //따라서 Send할때 해당세션이 Send작업중이라면 Send할 내용을 우선 que에 담아두고 빠지고 
        //나중에 Send가 완료가 될때 que에 보낼 내용이 잇는지 확인해서 그때 한번더 보내 줄 수 있도록 한다.
        //또한 Send할때는 어느곳에서나 내가 보내고 싶을때 Send를 요청해서 데이터를 보내야 하므로 멀티 쓰레드 환경에서 경합이 생길수 있기 때문에
        //Lock을 사용하여 경합을 방지해준다.
        //RegitsterSend부분을 안 묶은 이유는 Send에서 Lock으로 묶엇기때문이고 
        //OnSendCompleted를 다시 묶은 이유는 혹시나 SendAsyncd가 비동기로 처리 되면 
        //RegisterSend에서 호출해주지 않고 다른 쓰레드 에서 호출해 주므로 Lock으로 묶어준다.
        #region Send
        public void Send(ArraySegment<byte> SendBuff)
        {
            lock (_SendLock)
            {
                if (_SendQue != null)
                {
                    _SendQue.Enqueue(SendBuff);

                    //_SendBuffPendingList가 0이라는 의미는
                    //말그대로 보낸 내용을 다 처리 해줫으므로 다음 Send를 하라는 의미
                    if (_SendBuffPendingList.Count == 0)
                    {
                        RegisterSend();
                    }
                }
            }
        }

        //이부분에서 몇바이트를 보냈는지를 추적해서 너무 심하게 보내면 쉬면서 보내는것이 좋다.
        //동시 다발적으로 패킷이 몰릴때 어거지로 상대방이 받을 수 없는데 보내면 안되기에

        //경우에 따라서 패킷자체를 뭉쳐서 보내야하는 경우도 생김
        //예를 들어 천명의 유저가 움직이고 스킬 쓰는 모든 행위들을 하나의 패킷으로 만들어서 보내는 것을 예로 들수 잇다.
        //패킷을 작은 단위로 보내는것이 아닌 모아서 보내는 부분을 서버단에서 할것인지 컨텐츠단에서 할것인지 고민해야함
        void RegisterSend()
        {
            if (_Disconnect == 1)
            {
                return;
            }

            //이전 버전에서는 SendQue에 있는 내용을 하나씩 꺼내서 Send요청을 했다면
            //이제는 SendQue에 있는 내용들을 모두 다 꺼내서
            //List에 담은 후에 해당 List를 전달하는 방식으로 바꿔서
            //Send요청을최대한 줄일 수 있도록 해준다.
            while (_SendQue.Count > 0)
            {
                ArraySegment<byte> SendBuff = _SendQue.Dequeue();
                _SendBuffPendingList.Add(SendBuff);
            }

            try
            {
                _SendArgs.BufferList = _SendBuffPendingList;

                bool Pending = _ServerSocket.SendAsync(_SendArgs);
                if (Pending == false)
                {
                    OnSendCompleted(null, _SendArgs);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"RegisterSend 실패 {e}");
            }
        }

        void OnSendCompleted(object Sender, SocketAsyncEventArgs Args)
        {
            lock (_SendLock)
            {
                if (Args.BytesTransferred > 0
                && Args.SocketError == SocketError.Success)
                {
                    try
                    {
                        //Send요청이 완료 됏으면 이부분에서 BufferList를 청소해주고
                        //_SendBuffPendingList를 깔끔하게 청소한다.
                        _SendArgs.BufferList = null;
                        _SendBuffPendingList.Clear();

                        OnSend(_SendArgs.BytesTransferred);

                        //만약 SendQue에 보낼 내용이 잇으면 한번더 호출해준다. 
                        if (_SendQue.Count > 0)
                        {
                            RegisterSend();
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"OnSendCompleted 함수 실패 : {e}");
                    }
                }
                else
                {
                    Disconnect();
                }
            }
        }
        #endregion

        //Recv는 쓰레드 하나가 Recv를 걸고 
        //완료되면 해당 쓰레드가 Recv를 다시 걸기 때문에 동시 다발적으로 쓰레드가 RegisterRecv나 OnRecvCompleted로 진입하지는 않는다.        
        #region Recv
        void RegisterRecv()
        {
            if (_Disconnect == 1)
            {
                return;
            }

            // Recv 버퍼 설정 전에 깨끗하게 정리
            _RecvArgs.BufferList = null;
            _RecvBuffList.Clear();

            // 한번에 넣을수 잇는 크기를 구한다.
            int DirectEnqueSize = _RecvBuffer.GetDirectEnqueueSize();
            // 남아있는 RecvBuffer의 공간 크기를 구한다. 
            int FreeSize = _RecvBuffer.GetFreeSize();

            if (FreeSize > DirectEnqueSize)
            {
                ArraySegment<byte> RecvBufFirst = new ArraySegment<byte>(_RecvBuffer._RingBuf, _RecvBuffer._Rear, DirectEnqueSize);
                _RecvBuffList.Add(RecvBufFirst);
                ArraySegment<byte> RecvBufSecond = new ArraySegment<byte>(_RecvBuffer._RingBuf, 0, FreeSize - DirectEnqueSize);
                _RecvBuffList.Add(RecvBufSecond);
            }
            else
            {
                ArraySegment<byte> RecvBufFirst = new ArraySegment<byte>(_RecvBuffer._RingBuf, _RecvBuffer._Rear, DirectEnqueSize);
                _RecvBuffList.Add(RecvBufFirst);
            }

            _RecvArgs.BufferList = _RecvBuffList;

            try
            {
                //Listenr의 Accept하는 부분과 동일하게 Pending이 false라면 
                //바로 처리되어서 나온거니까 OnRecvCompleted함수를 호출해준다.
                bool Pending = _ServerSocket.ReceiveAsync(_RecvArgs);
                if (Pending == false)
                {
                    OnRecvCompleted(null, _RecvArgs);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"RegisterRecv 함수 실패 : {e}");
            }
        }

        //Recv완료시 콜해줄 함수
        void OnRecvCompleted(object Sender, SocketAsyncEventArgs Args)
        {
            if (Args.BytesTransferred > 0
                && Args.SocketError == SocketError.Success)
            {
                try
                {
                    _RecvBuffer.MoveRear(Args.BytesTransferred);                    

                    while (true)
                    {
                        // 최소 헤더크기만큼은 데이터가 왔는지 확인한다.
                        if (_RecvBuffer.GetUseSize() < Marshal.SizeOf(typeof(CMessage.st_ENCODE_HEADER)))
                        {
                            break;
                        }

                        byte[] Header = new byte[Marshal.SizeOf(typeof(CMessage.st_ENCODE_HEADER))];
                        _RecvBuffer.Peek(Header, Marshal.SizeOf(typeof(CMessage.st_ENCODE_HEADER)));

                        CMessage.st_ENCODE_HEADER HeaderStruct = Util.ByteToStruct<CMessage.st_ENCODE_HEADER>(Header);

                        if (HeaderStruct.PacketCode != 119)
                        {
                            break;
                        }

                        if (HeaderStruct.PacketLen + Marshal.SizeOf(typeof(CMessage.st_ENCODE_HEADER)) > _RecvBuffer.GetUseSize())
                        {
                            break;
                        }

                        CMessage Packet = new CMessage();

                        _RecvBuffer.MoveFront(Marshal.SizeOf(typeof(CMessage.st_ENCODE_HEADER)));

                        byte[] PacketBuffer = new byte[HeaderStruct.PacketLen];
                        _RecvBuffer.Dequeue(PacketBuffer, HeaderStruct.PacketLen);

                        Packet.SetHeader(HeaderStruct);
                        Packet.InsertData(PacketBuffer, Packet.GetRear(), HeaderStruct.PacketLen);

                        if (!Packet.Decode(HeaderStruct))
                        {
                            Debug.Log("디코딩 실패");
                            break;
                        }

                        OnRecvPacket(Packet);
                    }

                    RegisterRecv();
                }
                catch (Exception e)
                {
                    Debug.Log($"OnRecvCompleted 함수 실패 : {e}");
                }
            }
            else
            {
                //0이 오면 상대방이 연결을 끊엇을때     
                Disconnect();
            }
        }
        #endregion

        public abstract void OnRecvPacket(CMessage CheckCompletePacket);

        //소켓 연결 끊어주는 함수
        public void Disconnect()
        {
            //한소켓 대상으로 여러번 Disconnect를 호출 할 수 잇으므로
            //Interlocked함수를 이용해 Disconnect를 한번만 호출 할 수 있도록 해준다.
            if (Interlocked.Exchange(ref _Disconnect, 1) == 1)
            {
                return;
            }

            OnDisconnected(_ServerSocket.RemoteEndPoint);

            _ServerSocket.Close();
            Clear();

            Application.Quit();
        }
    }
}