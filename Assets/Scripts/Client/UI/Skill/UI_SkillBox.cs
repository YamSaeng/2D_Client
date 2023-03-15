using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// 스킬 박스 UI
public class UI_SkillBox : UI_Base
{
    public UI_SkillCharacteristic _PublicCharacteristic;
    public UI_SkillCharacteristic _SkillCharacteristic;
    public UI_SelectSkilCharacteristic _SelectSkilCharacteristic;

    enum en_SkillBoxGameObject
    {
        PublicActiveSkillPannel,
        PublicPassiveSkillPannel,

        SkillActiveSkillPannel,
        SkillPassiveSkillPannel,        

        SkillCharacteristicBackGround,
        UI_SkillBoxButtons,
        UI_SkillBox
    }

    enum en_SkillBoxText
    {
        SkillPointText,        
        SkillCharacteristicNameText
    }

    enum en_SkillBoxButton
    {
        PublicCharacteristicButton,
        SkillCharacteristicButton
    }

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_SkillBoxGameObject));
        Bind<Button>(typeof(en_SkillBoxButton));
        Bind<TextMeshProUGUI>(typeof(en_SkillBoxText));

        BindEvent(GetGameObject((int)en_SkillBoxGameObject.UI_SkillBox).gameObject, OnSkillBoxDrag, Define.en_UIEvent.Drag);
        BindEvent(GetButton((int)en_SkillBoxButton.PublicCharacteristicButton).gameObject, OnPublicCharacteristicButtonClick, Define.en_UIEvent.MouseClick);
        BindEvent(GetButton((int)en_SkillBoxButton.SkillCharacteristicButton).gameObject, OnSkillCharacteristicButtonClick, Define.en_UIEvent.MouseClick);

        _PublicCharacteristic = gameObject.transform.Find("UI_SkillPublicCharacteristicBox").GetComponent<UI_SkillCharacteristic>();
        _PublicCharacteristic.Binding();

        _SkillCharacteristic = gameObject.transform.Find("UI_SkillCharacteristic").gameObject.transform.Find("UI_SkillCharacteristicBox").GetComponent<UI_SkillCharacteristic>();        

        GameObject SelectSkillCharacteristicGO = gameObject.transform.Find("UI_SelectSkilCharacteristic").gameObject;
        _SelectSkilCharacteristic = SelectSkillCharacteristicGO.GetComponent<UI_SelectSkilCharacteristic>();
        _SelectSkilCharacteristic.Binding();

        SkillItemRemove();

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicBackGround).SetActive(false);

        _SelectSkilCharacteristic.ShowCloseUI(false);
        
        _SkillCharacteristic._SkillBox = this;
        _SkillCharacteristic.Binding();
        _SkillCharacteristic.ShowCloseUI(false);

        ShowCloseUI(false);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    private void PublicChracteristicShowClose(bool IsShowClose)
    {
        _PublicCharacteristic.ShowCloseUI(IsShowClose);
    }

    private void SkillCharacteristicShowClose(bool IsShowClose)
    {
        _SkillCharacteristic.ShowCloseUI(IsShowClose);        
    }

    public void SkillItemRemove()
    {
        foreach (Transform PublicActiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.PublicActiveSkillPannel).transform)
        {
            Destroy(PublicActiveSkillItem.gameObject);
        }

        foreach (Transform PublicPassiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.PublicPassiveSkillPannel).transform)
        {
            Destroy(PublicPassiveSkillItem.gameObject);
        }

        foreach (Transform SkillActiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.SkillActiveSkillPannel).transform)
        {
            Destroy(SkillActiveSkillItem.gameObject);
        }

        foreach (Transform SkillPassiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.SkillPassiveSkillPannel).transform)
        {
            Destroy(SkillPassiveSkillItem.gameObject);
        }        
    }

    public void RefreshSkillBoxUI()
    {
        SkillItemRemove();

        GameObject PlayerGO = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
        CBaseObject Player = PlayerGO.GetComponent<CBaseObject>();

        GetTextMeshPro((int)en_SkillBoxText.SkillPointText).text = Player._GameObjectInfo.ObjectSkillPoint.ToString();

        foreach (st_SkillInfo PublicPassiveSkillInfo in Managers.SkillBox._PublicCharacteristic.PassiveSkills)
        {
            GameObject PublicPassiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                GetGameObject((int)en_SkillBoxGameObject.PublicPassiveSkillPannel).transform);
            UI_SkillItem PublicPassiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(PublicPassiveSkillItemGO);
            PublicPassiveSkillItemUI.Binding();
            PublicPassiveSkillItemUI.SetSkillInfo(PublicPassiveSkillInfo);

            _PublicCharacteristic.GetPassiveSkills().Add(PublicPassiveSkillItemUI);
        }

        foreach (st_SkillInfo PublicActiveSkillInfo in Managers.SkillBox._PublicCharacteristic.ActiveSkills)
        {
            GameObject PublicActiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                GetGameObject((int)en_SkillBoxGameObject.PublicActiveSkillPannel).transform);
            UI_SkillItem PublicActiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(PublicActiveSkillItemGO);
            PublicActiveSkillItemUI.Binding();
            PublicActiveSkillItemUI.SetSkillInfo(PublicActiveSkillInfo);

            _PublicCharacteristic.GetActiveSkills().Add(PublicActiveSkillItemUI);
        }

        _PublicCharacteristic.CharcteristicSelectButtonShowClose(false);

        if (Managers.SkillBox._Characteristic != null
            && Managers.SkillBox._Characteristic._SkillCharacteristicType != en_SkillCharacteristic.SKILL_CATEGORY_NONE)
        {
            foreach (st_SkillInfo PassiveSkillInfo in Managers.SkillBox._Characteristic.PassiveSkills)
            {
                GameObject PassiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                    GetGameObject((int)en_SkillBoxGameObject.SkillPassiveSkillPannel).transform);
                UI_SkillItem PassiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(PassiveSkillItemGO);
                PassiveSkillItemUI.Binding();
                PassiveSkillItemUI.SetSkillInfo(PassiveSkillInfo);

                _SkillCharacteristic.GetPassiveSkills().Add(PassiveSkillItemUI);
            }

            foreach (st_SkillInfo ActiveSkillInfo in Managers.SkillBox._Characteristic.ActiveSkills)
            {
                GameObject ActiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                    GetGameObject((int)en_SkillBoxGameObject.SkillActiveSkillPannel).transform);
                UI_SkillItem ActiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(ActiveSkillItemGO);
                ActiveSkillItemUI.Binding();
                ActiveSkillItemUI.SetSkillInfo(ActiveSkillInfo);

                _SkillCharacteristic.GetActiveSkills().Add(ActiveSkillItemUI);
            }

            GetTextMeshPro((int)en_SkillBoxText.SkillCharacteristicNameText).text = Managers.String._SkillCharacteristicString[Managers.SkillBox._Characteristic._SkillCharacteristicType];
            CharacteristicSelectButtonShowClose(false);
        }
    }

    private void OnSkillBoxDrag(PointerEventData Event)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition += Event.delta;
    }

    private void OnPublicCharacteristicButtonClick(PointerEventData Event)
    {
        PublicChracteristicShowClose(true);
        SkillCharacteristicShowClose(false);

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicBackGround).SetActive(false);
        GetGameObject((int)en_SkillBoxGameObject.UI_SkillBoxButtons).GetComponent<RectTransform>().localPosition = new Vector3(0, 130.0f, 0);        
    }

    private void OnSkillCharacteristicButtonClick(PointerEventData Event)
    {
        PublicChracteristicShowClose(false);
        SkillCharacteristicShowClose(true);

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicBackGround).SetActive(true);
        GetGameObject((int)en_SkillBoxGameObject.UI_SkillBoxButtons).GetComponent<RectTransform>().localPosition = new Vector3(0, 130.0f, 0);
    }

    public void SkillBoxCharacteristicShowClose(bool IsShowClose)
    {
        _PublicCharacteristic.ShowCloseUI(IsShowClose);
        _SkillCharacteristic.ShowCloseUI(IsShowClose);        

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicBackGround).SetActive(IsShowClose);
    }

    public void SkillBoxSkillCharacteristicShowClose(bool IsShowClose)
    {
        _SkillCharacteristic.ShowCloseUI(IsShowClose);        

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicBackGround).SetActive(IsShowClose);
    }

    public void SkillBoxSelectCharacteristicShowClose(bool IsShowClose)
    {
        _SelectSkilCharacteristic.ShowCloseUI(IsShowClose);        
    }

    public void SkillBoxButtonShowClose(bool IsShowClose)
    {
        GetButton((int)en_SkillBoxButton.PublicCharacteristicButton).gameObject.SetActive(IsShowClose);
        GetButton((int)en_SkillBoxButton.SkillCharacteristicButton).gameObject.SetActive(IsShowClose);
    }

    public void CharacteristicSelectButtonShowClose(bool IsShowClose)
    {
        _SkillCharacteristic.CharcteristicSelectButtonShowClose(IsShowClose);
    }
}
