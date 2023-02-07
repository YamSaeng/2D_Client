using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SelectServer : UI_Scene
{ 
    // 서버 목록 UI 버튼들
    public List<UI_SelectServerItem> _SelectServerItems { get; } = new List<UI_SelectServerItem>();

    enum en_SelectServerPopupGameObject
    {
        SelectServerPanel,
        ServerSelectClose
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(en_SelectServerPopupGameObject));

        BindEvent(GetGameObject((int)en_SelectServerPopupGameObject.SelectServerPanel).gameObject, OnSelectServerDrag, Define.en_UIEvent.Drag);
        BindEvent(GetGameObject((int)en_SelectServerPopupGameObject.ServerSelectClose).gameObject, OnSelectServerClose, Define.en_UIEvent.MouseClick);
    }

    private void OnSelectServerDrag(PointerEventData Event)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Event.delta;
    }

    private void OnSelectServerClose(PointerEventData Event)
    {
        UISelectServerVisivle(false);
    }

    public void UISelectServerVisivle(bool Active)
    {
        gameObject.SetActive(Active);
    }

    // 받은 서버정보로 버튼 생성 및 초기화 해준다.
    public void SetSelectServer(st_ServerInfo[] ServerInfos, byte Count)
    {
        UISelectServerVisivle(true);        

        // 기존에 가지고 있던 서버 목록 다 없애주고
        _SelectServerItems.Clear(); 

        // 프리팹에 있는 서버목록 버튼들도 다 없애준다.
        GameObject ServerGrid = GetComponentInChildren<GridLayoutGroup>().gameObject;
        foreach (Transform Child in ServerGrid.transform)
        {
            Destroy(Child.gameObject);
        }
        
        // 서버목록 개수 만큼 버튼 만들어서 Grid에 자식으로 붙이고 저장해준 후
        // 서버정보를 셋팅해준다.
        for (int i = 0; i < Count; i++)
        {
            GameObject ServerSelectPopItemGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SERVER_SELECT_ITEM, ServerGrid.transform);
            UI_SelectServerItem SelectServerItem = Util.GetOrAddComponent<UI_SelectServerItem>(ServerSelectPopItemGo);
            _SelectServerItems.Add(SelectServerItem);
            
            SelectServerItem._ServerInfo = ServerInfos[i];
            // 추가적으로 BusyScore에 따라 초록색에서 빨간색으로 표시
        }

        // RefreshUI를 이용해서 UI를 업데이트 해준다.
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (_SelectServerItems.Count == 0)
        {
            return;
        }

        // 가지고 있는 서버 목록 버튼 만큼 반복하면서 UI를 업데이트 해준다.
        foreach (UI_SelectServerItem SelectServerItem in _SelectServerItems)
        {
            SelectServerItem.SelectServerPopupItemRefresh();
        }
    }    
}
