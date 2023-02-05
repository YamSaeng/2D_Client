using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace ServerCore
{
    //서버에 Connector이 있는 이유
    //MMO를 만들때 분산 서버를 만들고자 하면 서버와 서버끼리의 연결을 Connector 클래스를 이용해 연결해주는 용도로 사용 
    public class CConnector
    {        
        Func<CSession> _SessionFactory;

        public void Connect(IPEndPoint EndPoint, Func<CSession> SessionFactory, int Count = 1)
        {
            for (int i = 0; i < Count; i++)
            {
                Socket ClientSocket = new Socket(EndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _SessionFactory = SessionFactory;

                SocketAsyncEventArgs Args = new SocketAsyncEventArgs();
                Args.Completed += OnConnectComplete;
                Args.RemoteEndPoint = EndPoint;
                //소켓을 UserToken에 담아서 보내준다.
                Args.UserToken = ClientSocket;

                RegisterConnect(Args);
            }
        }

        void RegisterConnect(SocketAsyncEventArgs Args)
        {
            //UserToken에 담겨져 있는 Socket을 꺼내온다
            Socket ClientSocket = Args.UserToken as Socket;
            if (ClientSocket == null)
            {
                return;
            }

            //Pending false면 바로 연결된것 OnConnectComplete함수를 호ㅜㄹ해준다.
            bool Pending = ClientSocket.ConnectAsync(Args);
            if (Pending == false)
            {
                OnConnectComplete(null, Args);
            }
        }

        //비동기 Connect가 완료되면 호출할 함수
        void OnConnectComplete(object Sendser, SocketAsyncEventArgs Args)
        {
            if (Args.SocketError == SocketError.Success)
            {
                CSession Session = _SessionFactory.Invoke();
                Session.Start(Args.ConnectSocket);
                //커넥트 하고나서 뒷작업 처리 함수 컨텐츠 
                Session.OnConnected(Args.RemoteEndPoint);
            }
            else
            {
                Debug.Log($"Connect 실패 : {Args.SocketError}");
            }
        }
    }
}
