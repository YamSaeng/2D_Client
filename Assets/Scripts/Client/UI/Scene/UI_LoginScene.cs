using ServerCore;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_LoginScene : UI_Scene
{
    public struct st_LoginServerInfo
    {
        public IPAddress IPAddress;
        public int Port;
        public IPEndPoint EndPoint;
    }
 
    enum en_LoginUIGameObject
    {
        Account,
        Password,
        UI_Login,
        LoginPannel
    }

    enum en_LoginUIButtons
    {
        CreateButton,
        LoginButton
    }
    
    st_LoginServerInfo _LoginServerInfo;

    public GameObject _LoginUI;    
    public UI_CharacterChoice _CharacterChoiceUI;
    public UI_CharacterCreate _CharacterCreateUI;
    public UI_SelectServer _SelectServerUI;

    public UI_GlobalMessageBox _GlobalMessageBoxUI { get; private set; } // 개인 메세지 UI    

    public override void Init()
    {
        base.Init();                

        // 서버 IP , Port 입력
        _LoginServerInfo.IPAddress = IPAddress.Parse("124.254.187.159");        
        _LoginServerInfo.Port = 8889;
        _LoginServerInfo.EndPoint = new IPEndPoint(_LoginServerInfo.IPAddress, _LoginServerInfo.Port);

        _GlobalMessageBoxUI = GetComponentInChildren<UI_GlobalMessageBox>();

        // 캐릭터 선택 UI 저장
        _CharacterChoiceUI = GetComponentInChildren<UI_CharacterChoice>();
        _CharacterChoiceUI.gameObject.SetActive(false);

        // 캐릭터 생성 UI 저장
        _CharacterCreateUI = GetComponentInChildren<UI_CharacterCreate>();
        _CharacterCreateUI.gameObject.SetActive(false);

        // 서버 선택 UI 저장
        _SelectServerUI = GetComponentInChildren<UI_SelectServer>();
        _SelectServerUI.gameObject.SetActive(false);        

        // en_GameObject에 선언해둔 변수이름과 똑같은 GameObject 찾아서 내부에 저장해둔다.
        Bind<GameObject>(typeof(en_LoginUIGameObject));
        // en_Buttons에 선언해둔 변수이름과 똑같은 Button 찾아서 내부에 저장해둔다.
        Bind<Button>(typeof(en_LoginUIButtons));

        // CreateButton, LoginButton 꺼내서 각각 클릭이벤트 연동
        BindEvent(GetButton((int)en_LoginUIButtons.CreateButton).gameObject, OnClickCreateButton, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_LoginUIButtons.LoginButton).gameObject, OnClickLoginButton, Define.en_UIEvent.MouseClick);
        BindEvent(GetGameObject((int)en_LoginUIGameObject.LoginPannel).gameObject, OnLoginPannelClick, Define.en_UIEvent.MouseClick);
        
        if(Managers.NetworkManager._ServerState == en_ServerState.SERVER_STATE_NONE)
        {
            Managers.NetworkManager._ServerState = en_ServerState.SERVER_STATE_LOGIN_SERVER_CHOICE;

            CConnector Connector = new CConnector();
            Connector.Connect(_LoginServerInfo.EndPoint, () => { return Managers.NetworkManager._LoginServerSession; }, 1);
        }
        else if (Managers.NetworkManager._ServerState == en_ServerState.SERVER_STATE_LOGIN_SERVER_CHOICE)
        {
            LoginUIVisible(false);

            _SelectServerUI.SetSelectServer(Managers.NetworkManager._ServerLists, 1);
        }
        else if(Managers.NetworkManager._ServerState == en_ServerState.SERVER_STATE_LOGIN_CHARACTER_CHOICE)
        {
            LoginUIVisible(false);

            _CharacterChoiceUI.SetCharacterChoiceItem(Managers.NetworkManager._MyCharacterInfos);
        }
    }

    private void OnLoginPannelClick(PointerEventData Event)
    {
        
    }

    // 계정 생성 버튼 눌럿을때 호출
    public void OnClickCreateButton(PointerEventData Event)
    {        
        // UI에 입력된 Account와 Password를 가져온다.
        string Account = Get<GameObject>((int)en_LoginUIGameObject.Account).GetComponent<InputField>().text;
        string Password = Get<GameObject>((int)en_LoginUIGameObject.Password).GetComponent<InputField>().text;

        // 회원가입할 ID Password 전송
        CMessage ReqAccountNew = Packet.MakePacket.ReqAccountNewPacket(Account, Password);
        Managers.NetworkManager.LoginServerSend(ReqAccountNew);        

        //// 웹에 전송
        //Managers.WebManager.SendPostRequest<ResCreateAccountPacket>("account/create", CreateAccountPacket, (ResPacket) =>
        //{
        //    Debug.Log($"CreateAccount : {ResPacket.CreateOk}");

        //    // ID Password 박스 밀어줌
        //    Get<GameObject>((int)en_GameObject.Account).GetComponent<InputField>().text = "";
        //    Get<GameObject>((int)en_GameObject.Password).GetComponent<InputField>().text = "";
        //});
    }

    // 로그인 버튼 눌럿을때 호출
    public void OnClickLoginButton(PointerEventData Event)
    {
        // UI에 입력된 Account와 Password를 가져온다.
        string Account = Get<GameObject>((int)en_LoginUIGameObject.Account).GetComponent<InputField>().text;
        string Password = Get<GameObject>((int)en_LoginUIGameObject.Password).GetComponent<InputField>().text;

        CMessage ReqAccountLogin = Packet.MakePacket.ReqAccountServerLoginPacket(Account, Password);
        Managers.NetworkManager.LoginServerSend(ReqAccountLogin);

        //ReqLoginAccountPacket LoginAccountPacket = new ReqLoginAccountPacket();
        //LoginAccountPacket.AccountName = Account;
        //LoginAccountPacket.Password = Password;

        //// 웹에 전송
        //Managers.WebManager.SendPostRequest<ResLoginAccountPacket>("account/login", LoginAccountPacket, (ResPacket) =>
        //{
        //    Debug.Log($"LoginAccount : {ResPacket.LoginOk}");            

        //    if (ResPacket.LoginOk)
        //    {
        //        Get<GameObject>((int)en_GameObject.Account).GetComponent<InputField>().text = "";
        //        Get<GameObject>((int)en_GameObject.Password).GetComponent<InputField>().text = "";

        //        // 서버에서 발급받은 AccountID와 Token을 저장해둔다.
        //        Managers.NetworkManager._AccountId = ResPacket.AccountId;
        //        Managers.NetworkManager._Token = ResPacket.Token;
        //        Managers.NetworkManager._ClientId = ResPacket.ClientId;

        //        Get<GameObject>((int)en_GameObject.UI_Login).gameObject.SetActive(false);

        //        // 서버선택 UI를 띄워준다.
        //        UI_SelectServerPopup SelectServerPopup = Managers.UI.ShowPopupUI<UI_SelectServerPopup>();
        //        SelectServerPopup.SetSelectServer(ResPacket.ServerList);
        //    }
        //    else
        //    {

        //    }            
        //});
    }
    
    public void AccountPasswordFieldInit()
    {
        Get<GameObject>((int)en_LoginUIGameObject.Account).GetComponent<InputField>().text = "";
        Get<GameObject>((int)en_LoginUIGameObject.Password).GetComponent<InputField>().text = "";
    }

    public void LoginUIVisible(bool Visible)
    {
        if(_LoginUI)
        {
            _LoginUI.gameObject.SetActive(Visible);
        }
    }    
}
