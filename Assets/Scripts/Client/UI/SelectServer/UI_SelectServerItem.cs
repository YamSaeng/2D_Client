using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectServerItem : UI_Scene
{
    // 서버목록 버튼이 들고 있는 서버정보
    public st_ServerInfo _ServerInfo { get; set; }

    enum en_ServerSelectButton
    {
        SelectServerButton
    }

    enum en_ServerSelectText
    {
        NameText
    }

    enum en_Buttons
    {
        CreateButton,
        LoginButton
    }

    public override void Init()
    {
        // SelectServerButton를 가져온다.
        Bind<Button>(typeof(en_ServerSelectButton));
        // NameText를 가져온다.
        Bind<Text>(typeof(en_ServerSelectText));

        // SelectServerButton에 클릭이벤트를 달고 클릭했을때 OnServerSelectButtonClick를 호출 하도록 해준다.
        BindEvent(GetButton((int)en_ServerSelectButton.SelectServerButton).gameObject, OnSeverSelectButtonClick,Define.en_UIEvent.MouseClick);
    }

    // 버튼 UI 업데이트
    public void SelectServerPopupItemRefresh()
    {
        if(_ServerInfo.ServerName == null)
        {
            return;
        }

        // NameText를 서버이름으로 바꿔준다.
        GetText((int)en_ServerSelectText.NameText).text = _ServerInfo.ServerName;
    }

    // 서버 목록 버튼을 클릭할 경우 게임서버 접속
    void OnSeverSelectButtonClick(PointerEventData Event)
    {
        // 게임서버에 접속
        Managers.NetworkManager.ConnectToGame(_ServerInfo);

        UI_LoginScene LoginScene = Managers.UI._SceneUI as UI_LoginScene;
        if(LoginScene != null)
        {
            LoginScene._SelectServerUI.UISelectServerVisivle(false);
        }
    }
}
