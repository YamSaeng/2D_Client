using System.Collections.Generic;
using System.Net;
using ServerCore;

public class CNetworkManager
{
    public st_ServerInfo[] _ServerLists = new st_ServerInfo[1];
    public st_GameObjectInfo[] _MyCharacterInfos = new st_GameObjectInfo[3];

    public en_ServerState _ServerState = en_ServerState.SERVER_STATE_NONE;
    // AccountServer에서 발급해준 AccountId
    public long _AccountId { get; set; }
    // AccountServer에서 발급해준 Token
    public byte[] _Token = new byte[50];
    // 로그인 할때 입력한 ID
    public string _LoginID { get; set; }
    // GameServer에서 발급해준 내가 선택한 캐릭터의 DBID
    public long _PlayerDBId { get; set; }
    // 내가 선택한 캐릭터의 이름
    public string _PlayerName { get; set; }
    
    public CServerSession _GameServerSession = new CServerSession();
    public CServerSession _LoginServerSession = new CServerSession();

    public void GameServerSend(CMessage GameServerSendPacket)
    {
        _GameServerSession.SendMessage(GameServerSendPacket);
    }

    public void LoginServerSend(CMessage LoginServerSendPacket)
    {
        _LoginServerSession.SendMessage(LoginServerSendPacket);
    }

    // 입력받은 서버 정보로 서버 연결
    public void ConnectToGame(st_ServerInfo ServerInfo)
    {       
        // IP 주소
        IPAddress IpAddr = IPAddress.Parse(ServerInfo.ServerIP);
        // 포트 번호
        IPEndPoint EndPoint = new IPEndPoint(IpAddr, ServerInfo.ServerPort);

        CConnector Connector = new CConnector();

        Connector.Connect(EndPoint, () => { return _GameServerSession; }, 1);
    }  

    public void Update()
    {
        List<CPacketMessage> list = CPacketQueue.GetInstance.PopAll();
        foreach (CPacketMessage Packet in list)
        {
            Packet.UnPackMessageFunc.Invoke(Packet.Message);
        }               
    }
}
