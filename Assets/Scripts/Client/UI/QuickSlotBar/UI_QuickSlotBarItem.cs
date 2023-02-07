
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_QuickSlotBarItem : UI_Base
{
    Dictionary<KeyCode, string> _QuickSlotKeyDictionary = new Dictionary<KeyCode, string>();

    // 퀵슬롯바 박스
    UI_QuickSlotBarBox _QuickSlotBarBox;
    UI_GameScene _GameSceneUI;

    public st_QuickSlotBarSlotInfo _QuickSlotBarSlotInfo = null;
    public UI_QuickSlotBarItem _ComboSkillUI = null;

    private float _SkillCoolTime;
    private float _SkillRemainTime;
    private float _SkillCoolTimeSpeed;

    enum en_QuickSlotBarGameObject
    {
        QuickSlotSkillBarSlot
    }

    enum en_QuickSlotBarImage
    {
        QuickSlotSkillIconImage,
        QuickSlotSkillCoolTimeImage
    }

    enum en_QuickSlotBarText
    {
        QuickSlotSkillKeyText,
        QuickSlotCoolTimeText,
        QuickSlotItemCountText
    }

    private static byte _SwapQuickSlotBarIndexA;
    private static byte _SwapQuickSlotBarSlotIndexA;
    public Sprite _InitImage;
    private Animator _QuickSlotBarItemAnimator;

    private Coroutine _CoolTimeStartCO;

    public override void Init()
    {
        QuickSlotKeySave();

        _QuickSlotBarItemAnimator = GetComponent<Animator>();

        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;        
        _QuickSlotBarBox = _GameSceneUI._QuickSlotBarBoxUI;        

        Bind<GameObject>(typeof(en_QuickSlotBarGameObject));
        Bind<Image>(typeof(en_QuickSlotBarImage));
        Bind<TextMeshProUGUI>(typeof(en_QuickSlotBarText));
                
        BindEvent(GetGameObject((int)en_QuickSlotBarGameObject.QuickSlotSkillBarSlot).gameObject, OnQuickSlotBarItemPointerEnter, Define.en_UIEvent.PointerEnter);
        BindEvent(GetGameObject((int)en_QuickSlotBarGameObject.QuickSlotSkillBarSlot).gameObject, OnQuickSlotBarItemPointerExit, Define.en_UIEvent.PointerExit);

        BindEvent(GetGameObject((int)en_QuickSlotBarGameObject.QuickSlotSkillBarSlot).gameObject, OnQuickSlotBarItemDragBegin, Define.en_UIEvent.BeginDrag);
        BindEvent(GetGameObject((int)en_QuickSlotBarGameObject.QuickSlotSkillBarSlot).gameObject, OnQuickSlotBarItemDrag, Define.en_UIEvent.Drag);
        BindEvent(GetGameObject((int)en_QuickSlotBarGameObject.QuickSlotSkillBarSlot).gameObject, OnQuickSlotBarItemDragEnd, Define.en_UIEvent.EndDrag);
        BindEvent(GetGameObject((int)en_QuickSlotBarGameObject.QuickSlotSkillBarSlot).gameObject, OnQuickSlotBarOnDrop, Define.en_UIEvent.Drop);
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
    
    }

    public void SetQuickBarItem(st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo)
    {
        _QuickSlotBarSlotInfo = QuickSlotBarSlotInfo;

        GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotSkillKeyText).text = _QuickSlotKeyDictionary[_QuickSlotBarSlotInfo.QuickSlotKey];
        GetImage((int)en_QuickSlotBarImage.QuickSlotSkillCoolTimeImage).fillAmount = 0;
        GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotCoolTimeText).text = "";

        switch (_QuickSlotBarSlotInfo.QuickSlotBarType)
        {
            case en_QuickSlotBarType.QUICK_SLOT_BAR_TYPE_NONE:
                GetImage((int)en_QuickSlotBarImage.QuickSlotSkillIconImage).sprite = _InitImage;

                GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotItemCountText).text = "";
                break;
            case en_QuickSlotBarType.QUICK_SLOT_BAR_TYPE_SKILL:
                GetImage((int)en_QuickSlotBarImage.QuickSlotSkillIconImage).sprite = Managers.Sprite._SkillSprite[_QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType];

                GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotItemCountText).text = "";
                break;
            case en_QuickSlotBarType.QUICK_SLOT_BAR_TYPE_ITEM:
                GetImage((int)en_QuickSlotBarImage.QuickSlotSkillIconImage).sprite = Managers.Sprite._ItemSprite[_QuickSlotBarSlotInfo.QuickBarItemInfo.ItemSmallCategory];

                GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotItemCountText).text = _QuickSlotBarSlotInfo.QuickBarItemInfo.ItemCount.ToString();
                break;
        }        
    }

    public void QuickSlotBarItemCoolTimeStart(float SkillCoolTimeSpeed)
    {
        if (_CoolTimeStartCO != null)
        {
            QuickSlotBarItemCoolTimeStop();
        }

        _SkillRemainTime = _QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillRemainTime / 1000.0f;
        _SkillCoolTime = _QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillCoolTime / 1000.0f;
        _SkillCoolTimeSpeed = 1.0f / _SkillCoolTime;

        if (_SkillRemainTime > 0)
        {
            GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotCoolTimeText).text = _SkillRemainTime.ToString("F1") + "초";

            _CoolTimeStartCO = StartCoroutine("CoolTimeStart");
        }
    }

    public void QuickSlotBarItemCoolTimeStart(int CoolTime)
    {
        if (_CoolTimeStartCO != null)
        {
            QuickSlotBarItemCoolTimeStop();
        }

        _SkillRemainTime = CoolTime / 1000.0f;
        _SkillCoolTime = CoolTime / 1000.0f;
        _SkillCoolTimeSpeed = 1.0f / _SkillCoolTime;

        if (_SkillRemainTime > 0)
        {
            GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotCoolTimeText).text = _SkillRemainTime.ToString("F1") + "초";

            _CoolTimeStartCO = StartCoroutine("CoolTimeStart");
        }
    }

    public void QuickSlotBarItemCoolTimeStop()
    {
        if (_CoolTimeStartCO != null)
        {
            StopCoroutine(_CoolTimeStartCO);
            _CoolTimeStartCO = null;
        }

        _SkillRemainTime = 0;
        _SkillCoolTime = 0;
        _SkillCoolTimeSpeed = 0;

        GetImage((int)en_QuickSlotBarImage.QuickSlotSkillCoolTimeImage).fillAmount = 0;
        GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotCoolTimeText).text = "";
    }

    private void OnQuickSlotBarItemPointerEnter(PointerEventData PointerEnterEvent)
    {
        if (_QuickSlotBarSlotInfo.QuickBarSkillInfo != null && _GameSceneUI._DragQuickSlotItemUI.gameObject.active == false)
        {
            _GameSceneUI.SetSkillExplanation(_QuickSlotBarSlotInfo.QuickBarSkillInfo);
        }
    }

    private void OnQuickSlotBarItemPointerExit(PointerEventData PointerExitEvent)
    {
        _GameSceneUI.EmptySkillExplanation();
    }

    private void OnQuickSlotBarItemDragBegin(PointerEventData DragBeginEvent)
    {     
        _GameSceneUI.EmptySkillExplanation();

        _SwapQuickSlotBarIndexA = 0;
        _SwapQuickSlotBarSlotIndexA = 0;

        if (_QuickSlotBarSlotInfo.QuickBarSkillInfo == null && _QuickSlotBarSlotInfo.QuickBarItemInfo == null)
        {
            return;
        }

        _SwapQuickSlotBarIndexA = _QuickSlotBarSlotInfo.QuickSlotBarIndex;
        _SwapQuickSlotBarSlotIndexA = _QuickSlotBarSlotInfo.QuickSlotBarSlotIndex;

        _GameSceneUI._DragQuickSlotItemUI.SetRaycast(false);        

        switch (_QuickSlotBarSlotInfo.QuickSlotBarType)
        {
            case en_QuickSlotBarType.QUICK_SLOT_BAR_TYPE_SKILL:
                _GameSceneUI._DragQuickSlotItemUI.QuickSlotItemSet(Managers.Sprite._SkillSprite[_QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType]);
                break;
            case en_QuickSlotBarType.QUICK_SLOT_BAR_TYPE_ITEM:
                _GameSceneUI._DragQuickSlotItemUI.QuickSlotItemSet(Managers.Sprite._ItemSprite[_QuickSlotBarSlotInfo.QuickBarItemInfo.ItemSmallCategory]);
                break;
        }

        _GameSceneUI._DragQuickSlotItemUI.ShowCloseUI(true);
        _GameSceneUI._DragQuickSlotItemUI.GetComponent<RectTransform>().transform.position = GetGameObject((int)en_QuickSlotBarGameObject.QuickSlotSkillBarSlot).transform.parent.GetComponent<RectTransform>().position;
    }

    private void OnQuickSlotBarItemDrag(PointerEventData DragEvent)
    {
        if (_QuickSlotBarSlotInfo.QuickBarSkillInfo == null && _QuickSlotBarSlotInfo.QuickBarItemInfo == null)
        {
            return;
        }

        _GameSceneUI._DragQuickSlotItemUI.GetComponent<RectTransform>().anchoredPosition += DragEvent.delta;
    }

    private void OnQuickSlotBarItemDragEnd(PointerEventData DragEndEvent)
    {
        if (_QuickSlotBarSlotInfo.QuickBarSkillInfo == null && _QuickSlotBarSlotInfo.QuickBarItemInfo == null)
        {
            return;
        }

        UI_QuickSlotBar QuickSlotBar = transform.parent.parent.parent.GetComponent<UI_QuickSlotBar>();
        if (QuickSlotBar != null)
        {
            UI_QuickSlotBarBox QuickSlotBarBox = QuickSlotBar.transform.parent.parent.GetComponent<UI_QuickSlotBarBox>();
            if (QuickSlotBarBox != null)
            {
                if (!QuickSlotBarBox.IsCollision(_GameSceneUI._DragQuickSlotItemUI))
                {
                    CMessage ReqQuickSlotInitPacket = Packet.MakePacket.ReqMakeQuickSlotInitPacket
                        (Managers.NetworkManager._AccountId,
                        Managers.NetworkManager._PlayerDBId,
                        _QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillCharacteristic,
                        _QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType,
                        _SwapQuickSlotBarIndexA,
                        _SwapQuickSlotBarSlotIndexA);
                    Managers.NetworkManager.GameServerSend(ReqQuickSlotInitPacket);
                }
            }
        }

        _GameSceneUI._DragQuickSlotItemUI.ShowCloseUI(false);
        _GameSceneUI._DragQuickSlotItemUI.SetRaycast(true);
    }

    private void OnQuickSlotBarOnDrop(PointerEventData OnDropEvent)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if (GameSceneUI == null)
        {
            return;
        }

        // 스킬박스에서 스킬을 가져다가 놓았을 경우
        if (GameSceneUI._DragSkillItemUI._DragSkillInfo != null)
        {
            _QuickSlotBarSlotInfo.QuickBarSkillInfo = GameSceneUI._DragSkillItemUI._DragSkillInfo;
            _QuickSlotBarSlotInfo.QuickBarItemInfo = null;

            CMessage ReqQuickSlotSkillSavePacket = Packet.MakePacket.ReqMakeQuickSlotSavePacket(Managers.NetworkManager._AccountId,
                Managers.NetworkManager._PlayerDBId, 
                _QuickSlotBarSlotInfo);
            Managers.NetworkManager.GameServerSend(ReqQuickSlotSkillSavePacket);
            return;
        }        

        if (GameSceneUI._DragItemUI._DragItemInfo != null)
        {
            CMessage ReqPlaceItemPacket = Packet.MakePacket.ReqMakePlaceItemPacket(Managers.NetworkManager._AccountId,
            Managers.NetworkManager._PlayerDBId,
            GameSceneUI._DragItemUI._DragItemInfo.ItemTileGridPositionX,
            GameSceneUI._DragItemUI._DragItemInfo.ItemTileGridPositionY);
            Managers.NetworkManager.GameServerSend(ReqPlaceItemPacket);

            _QuickSlotBarSlotInfo.QuickBarSkillInfo = null;
            _QuickSlotBarSlotInfo.QuickBarItemInfo = GameSceneUI._DragItemUI._DragItemInfo;

            CMessage ReqQuickSlotItemSavePacket = Packet.MakePacket.ReqMakeQuickSlotSavePacket(Managers.NetworkManager._AccountId,
                Managers.NetworkManager._PlayerDBId,
                _QuickSlotBarSlotInfo);
            Managers.NetworkManager.GameServerSend(ReqQuickSlotItemSavePacket);
            return;
        }

        // 퀵슬롯 스왑
        CMessage ReqQuickSlotSwapPacekt = Packet.MakePacket.ReqMakeQuickSlotSwapPacket(Managers.NetworkManager._AccountId, Managers.NetworkManager._PlayerDBId,
            _SwapQuickSlotBarIndexA, _SwapQuickSlotBarSlotIndexA,
            _QuickSlotBarSlotInfo.QuickSlotBarIndex, _QuickSlotBarSlotInfo.QuickSlotBarSlotIndex);
        Managers.NetworkManager.GameServerSend(ReqQuickSlotSwapPacekt);
    }

    IEnumerator CoolTimeStart()
    {
        float TimePassed = Time.deltaTime;

        float Rate = _SkillCoolTimeSpeed;

        float RemainProgress = _SkillRemainTime / _SkillCoolTime;        

        float Progress = Mathf.Abs(1.0f - RemainProgress);

        while (Progress <= 1.0f)
        {            
            float FillAmount = Mathf.Lerp(0, 1, Progress);            
            GetImage((int)en_QuickSlotBarImage.QuickSlotSkillCoolTimeImage).fillAmount = FillAmount;
            Progress += Rate * Time.deltaTime;

            TimePassed += Time.deltaTime;

            GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotCoolTimeText).text = (_SkillRemainTime - TimePassed).ToString("F1") + "초";
            yield return null;
        }

        _SkillRemainTime = 0;
        _QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillRemainTime = 0;

        _CoolTimeStartCO = null;

        _QuickSlotBarItemAnimator.Play("QuickSlotBarCoolTimeEnd");
        StartCoroutine("QuickSlotBarIdle");

        GetImage((int)en_QuickSlotBarImage.QuickSlotSkillCoolTimeImage).fillAmount = 0;
        GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotCoolTimeText).text = "";
    }

    IEnumerator QuickSlotBarIdle()
    {
        yield return new WaitForSeconds(0.5f);
        _QuickSlotBarItemAnimator.Play("QuickSlotBarCoolTimeIdle");
    }

    public void QuickSlotBarComboSkillOn()
    {
        _QuickSlotBarItemAnimator.Play("QuickSlotBarComboSkillOn");
    }       

    public void Destroy()
    {
        _QuickSlotBarItemAnimator = null;
        _InitImage = null;
        _QuickSlotBarBox = null;
        _QuickSlotKeyDictionary.Clear();

        GetImage((int)en_QuickSlotBarImage.QuickSlotSkillCoolTimeImage).sprite = null;
        GetImage((int)en_QuickSlotBarImage.QuickSlotSkillIconImage).sprite = null;

        GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotSkillKeyText).text = "";
        GetTextMeshPro((int)en_QuickSlotBarText.QuickSlotCoolTimeText).text = "";

        Destroy(gameObject);
    }

    private void QuickSlotKeySave()
    {
        _QuickSlotKeyDictionary.Add(KeyCode.None, "");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha1, "1");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha2, "2");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha3, "3");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha4, "4");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha5, "5");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha6, "6");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha7, "7");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha8, "8");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha9, "9");
        _QuickSlotKeyDictionary.Add(KeyCode.Alpha0, "0");
    }
}