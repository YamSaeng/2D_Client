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
        //Send�ϰ��� �ϴ� ��Ŷ �ӽ� �����
        Queue<ArraySegment<byte>> _SendQue = new Queue<ArraySegment<byte>>();

        // Send�ϰ��� �ϴ� �޼��� ���
        List<ArraySegment<byte>> _SendBuffPendingList = new List<ArraySegment<byte>>();
        // Recv���� �� �ʿ��� ���� 
        List<ArraySegment<byte>> _RecvBuffList = new List<ArraySegment<byte>>();

        SocketAsyncEventArgs _SendArgs = new SocketAsyncEventArgs();
        SocketAsyncEventArgs _RecvArgs = new SocketAsyncEventArgs();

        public abstract void OnConnected(EndPoint endPoint);
        public abstract void OnSend(int NumOfBytes);
        public abstract void OnDisconnected(EndPoint endPoint);

        //���� ������ ������ �޾ƿ´�.
        public void Start(Socket Socket)
        {
            _ServerSocket = Socket;

            // RecvBuf�� �����Ͱ� ���� �� ȣ������ �Լ� ����
            _RecvArgs.Completed += OnRecvCompleted;
            // Send �Ϸ�� �������� �Լ� ����
            _SendArgs.Completed += OnSendCompleted;

            //�߰��� ������ ���� ������ ������ UserToken�� ��Ƽ� ������ ��
            //RecvArgs.UserToken =                        

            RegisterRecv();
        }

        void Clear()
        {
            //SendLock ��� ����
            lock (_SendLock)
            {
                //SendQue , SendBuffPenginList ����
                _SendQue.Clear();
                _SendBuffPendingList.Clear();

                _SendQue = null;
                _SendBuffPendingList = null;
            }
        }

        //�ϳ��� �����忡�� Send�� �ϸ� ��� ������
        //�ټ��� �����忡�� Send�� �Ҷ� _SendArgs�� �ϳ��� ����ϰ� �ȴٸ� SetBuffer�ϴ� �κп��� ���� ���� ���۷� ������ ���ְ� �ǹǷ�
        //�̻��� ������ ������ �մ� �κ��� �����. _SendArgs.SetBuffer(SendBuff) ���� SendBuff�� ��� ��������� ����
        //���� Send�Ҷ� �ش缼���� Send�۾����̶�� Send�� ������ �켱 que�� ��Ƶΰ� ������ 
        //���߿� Send�� �Ϸᰡ �ɶ� que�� ���� ������ �մ��� Ȯ���ؼ� �׶� �ѹ��� ���� �� �� �ֵ��� �Ѵ�.
        //���� Send�Ҷ��� ����������� ���� ������ ������ Send�� ��û�ؼ� �����͸� ������ �ϹǷ� ��Ƽ ������ ȯ�濡�� ������ ����� �ֱ� ������
        //Lock�� ����Ͽ� ������ �������ش�.
        //RegitsterSend�κ��� �� ���� ������ Send���� Lock���� �����⶧���̰� 
        //OnSendCompleted�� �ٽ� ���� ������ Ȥ�ó� SendAsyncd�� �񵿱�� ó�� �Ǹ� 
        //RegisterSend���� ȣ�������� �ʰ� �ٸ� ������ ���� ȣ���� �ֹǷ� Lock���� �����ش�.
        #region Send
        public void Send(ArraySegment<byte> SendBuff)
        {
            lock (_SendLock)
            {
                if (_SendQue != null)
                {
                    _SendQue.Enqueue(SendBuff);

                    //_SendBuffPendingList�� 0�̶�� �ǹ̴�
                    //���״�� ���� ������ �� ó�� �آZ���Ƿ� ���� Send�� �϶�� �ǹ�
                    if (_SendBuffPendingList.Count == 0)
                    {
                        RegisterSend();
                    }
                }
            }
        }

        //�̺κп��� �����Ʈ�� ���´����� �����ؼ� �ʹ� ���ϰ� ������ ���鼭 �����°��� ����.
        //���� �ٹ������� ��Ŷ�� ������ ������� ������ ���� �� ���µ� ������ �ȵǱ⿡

        //��쿡 ���� ��Ŷ��ü�� ���ļ� �������ϴ� ��쵵 ����
        //���� ��� õ���� ������ �����̰� ��ų ���� ��� �������� �ϳ��� ��Ŷ���� ���� ������ ���� ���� ��� �մ�.
        //��Ŷ�� ���� ������ �����°��� �ƴ� ��Ƽ� ������ �κ��� �����ܿ��� �Ұ����� �������ܿ��� �Ұ����� ����ؾ���
        void RegisterSend()
        {
            if (_Disconnect == 1)
            {
                return;
            }

            //���� ���������� SendQue�� �ִ� ������ �ϳ��� ������ Send��û�� �ߴٸ�
            //������ SendQue�� �ִ� ������� ��� �� ������
            //List�� ���� �Ŀ� �ش� List�� �����ϴ� ������� �ٲ㼭
            //Send��û���ִ��� ���� �� �ֵ��� ���ش�.
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
                Debug.Log($"RegisterSend ���� {e}");
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
                        //Send��û�� �Ϸ� ������ �̺κп��� BufferList�� û�����ְ�
                        //_SendBuffPendingList�� ����ϰ� û���Ѵ�.
                        _SendArgs.BufferList = null;
                        _SendBuffPendingList.Clear();

                        OnSend(_SendArgs.BytesTransferred);

                        //���� SendQue�� ���� ������ ������ �ѹ��� ȣ�����ش�. 
                        if (_SendQue.Count > 0)
                        {
                            RegisterSend();
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.Log($"OnSendCompleted �Լ� ���� : {e}");
                    }
                }
                else
                {
                    Disconnect();
                }
            }
        }
        #endregion

        //Recv�� ������ �ϳ��� Recv�� �ɰ� 
        //�Ϸ�Ǹ� �ش� �����尡 Recv�� �ٽ� �ɱ� ������ ���� �ٹ������� �����尡 RegisterRecv�� OnRecvCompleted�� ���������� �ʴ´�.        
        #region Recv
        void RegisterRecv()
        {
            if (_Disconnect == 1)
            {
                return;
            }

            // Recv ���� ���� ���� �����ϰ� ����
            _RecvArgs.BufferList = null;
            _RecvBuffList.Clear();

            // �ѹ��� ������ �մ� ũ�⸦ ���Ѵ�.
            int DirectEnqueSize = _RecvBuffer.GetDirectEnqueueSize();
            // �����ִ� RecvBuffer�� ���� ũ�⸦ ���Ѵ�. 
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
                //Listenr�� Accept�ϴ� �κа� �����ϰ� Pending�� false��� 
                //�ٷ� ó���Ǿ ���°Ŵϱ� OnRecvCompleted�Լ��� ȣ�����ش�.
                bool Pending = _ServerSocket.ReceiveAsync(_RecvArgs);
                if (Pending == false)
                {
                    OnRecvCompleted(null, _RecvArgs);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"RegisterRecv �Լ� ���� : {e}");
            }
        }

        //Recv�Ϸ�� ������ �Լ�
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
                        // �ּ� ���ũ�⸸ŭ�� �����Ͱ� �Դ��� Ȯ���Ѵ�.
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
                            Debug.Log("���ڵ� ����");
                            break;
                        }

                        OnRecvPacket(Packet);
                    }

                    RegisterRecv();
                }
                catch (Exception e)
                {
                    Debug.Log($"OnRecvCompleted �Լ� ���� : {e}");
                }
            }
            else
            {
                //0�� ���� ������ ������ ��������     
                Disconnect();
            }
        }
        #endregion

        public abstract void OnRecvPacket(CMessage CheckCompletePacket);

        //���� ���� �����ִ� �Լ�
        public void Disconnect()
        {
            //�Ѽ��� ������� ������ Disconnect�� ȣ�� �� �� �����Ƿ�
            //Interlocked�Լ��� �̿��� Disconnect�� �ѹ��� ȣ�� �� �� �ֵ��� ���ش�.
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