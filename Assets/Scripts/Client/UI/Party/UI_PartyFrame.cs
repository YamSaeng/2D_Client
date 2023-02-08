using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PartyFrame : UI_Base
{
    List<UI_PartyPlayerFrame> _PartyPlayerFrames = new List<UI_PartyPlayerFrame>();

    enum en_PartyFrameGameObject
    {
        PartyFrameSubject,
        PartyPlayerPannel
    }

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_PartyFrameGameObject));

        GetComponent<RectTransform>().anchoredPosition = new Vector3(150.0f, 250.0f, 0);

        BindEvent(GetGameObject((int)en_PartyFrameGameObject.PartyFrameSubject).gameObject, OnPartyFrameDrag, Define.en_UIEvent.Drag);

        ShowCloseUI(false);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void PartyPlayerFrameCreate(st_GameObjectInfo PartyPlayerGameObjectInfo)
    {
        GameObject PartyPlayerFrameGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_PARTY_PLAYER_INFO_FRAME, GetGameObject((int)en_PartyFrameGameObject.PartyPlayerPannel).transform);
        if (PartyPlayerFrameGO != null)
        {
            UI_PartyPlayerFrame PartyPlayerFrameUI = PartyPlayerFrameGO.GetComponent<UI_PartyPlayerFrame>();
            PartyPlayerFrameUI.Binding();
            PartyPlayerFrameUI.SetPartyPlayerGameObjectInfo(PartyPlayerGameObjectInfo);
            _PartyPlayerFrames.Add(PartyPlayerFrameGO.GetComponent<UI_PartyPlayerFrame>());

            ShowCloseUI(true);
        }
    }

    public void PartyPlayerFrameDestory(long PlayerID)
    {
        foreach (UI_PartyPlayerFrame PartyPlayerFrame in _PartyPlayerFrames)
        {
            if (PartyPlayerFrame.GetPartyPlayerGameObjectInfo().ObjectId == PlayerID)
            {
                _PartyPlayerFrames.Remove(PartyPlayerFrame);
                Destroy(PartyPlayerFrame.gameObject);
                break;
            }
        }

        if (_PartyPlayerFrames.Count == 1)
        {
            PartyPlayerFrameAllDestroy();
        }
    }

    public void PartyPlayerFrameAllDestroy()
    {
        foreach (UI_PartyPlayerFrame PartyPlayerFrame in _PartyPlayerFrames)
        {
            Destroy(PartyPlayerFrame.gameObject);
        }

        _PartyPlayerFrames.Clear();

        ShowCloseUI(false);
    }
        
    private void OnPartyFrameDrag(PointerEventData Event)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
     
        // �׷� �������� ���� ��ġ������ ȭ�� ��ġ���� ����
        Vector2 localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameSceneUI.GetComponent<RectTransform>(),
            Camera.main.WorldToScreenPoint(transform.position),
            Camera.main, out localPos);

        // �׷� �������� ȭ�� ���� ����� �ʵ��� ����
        // 220 = �׷� ���� ������ ���� ũ��
        // 60 = �׷� ���� ������ ���� ũ��
        // (200 * _PartyPlayerFrames.Count + _PartyPlayerFrames.Count * 10)) = �׷� ���� ������ ���� ��ŭ ũ�� 10 = �׷� ���� ������ ���� ����
        if (localPos.x < -(Screen.width - 220) / 2)
        {
            GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition += (Vector2.right * 10.0f);
            return;
        }
        else if(localPos.x > (Screen.width - 220) / 2)
        {
            GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition += (Vector2.left * 10.0f);
            return;
        }                
        else if(localPos.y > (Screen.height - 60) / 2)
        {
            GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition += (Vector2.down * 10.0f);
            return;
        }
        else if (localPos.y < -(Screen.height - (60 + (200 * _PartyPlayerFrames.Count + _PartyPlayerFrames.Count * 10))) / 2)
        {
            GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition += (Vector2.up * 10.0f);
            return;
        }        

        GetComponent<RectTransform>().anchoredPosition += Event.delta;                
    }
}