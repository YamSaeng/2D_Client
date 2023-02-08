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
    public UI_SkillCharacteristic[] _SkillCharacteristic = new UI_SkillCharacteristic[3];
    public UI_SelectSkilCharacteristic _SelectSkilCharacteristic;

    enum en_SkillBoxGameObject
    {
        PublicActiveSkillPannel,
        PublicPassiveSkillPannel,

        SkillOneActiveSkillPannel,
        SkillOnePassiveSkillPannel,

        SkillTwoActiveSkillPannel,
        SkillTwoPassiveSkillPannel,

        SkillThreeActiveSkillPannel,
        SkillThreePassiveSkillPannel,

        SkillCharacteristicsBackGround,
        UI_SkillBoxButtons,
        UI_SkillBox
    }

    enum en_SkillBoxText
    {
        SkillPointText
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

        _SkillCharacteristic[0] = gameObject.transform.Find("UI_SkillCharacteristics").gameObject.transform.Find("UI_SkillOneCharacteristicBox").GetComponent<UI_SkillCharacteristic>();
        _SkillCharacteristic[1] = gameObject.transform.Find("UI_SkillCharacteristics").gameObject.transform.Find("UI_SkillTwoCharacteristicBox").GetComponent<UI_SkillCharacteristic>();
        _SkillCharacteristic[2] = gameObject.transform.Find("UI_SkillCharacteristics").gameObject.transform.Find("UI_SkillThreeCharacteristicBox").GetComponent<UI_SkillCharacteristic>();

        GameObject SelectSkillCharacteristicGO = gameObject.transform.Find("UI_SelectSkilCharacteristic").gameObject;
        _SelectSkilCharacteristic = SelectSkillCharacteristicGO.GetComponent<UI_SelectSkilCharacteristic>();
        _SelectSkilCharacteristic.Binding();

        SkillItemRemove();

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicsBackGround).SetActive(false);

        _SelectSkilCharacteristic.ShowCloseUI(false);        

        for (byte i = 0; i < 3; i++)
        {
            _SkillCharacteristic[i]._SkillCharacteristicIndex = i;
            _SkillCharacteristic[i]._SkillBox = this;
            _SkillCharacteristic[i].Binding();
            _SkillCharacteristic[i].ShowCloseUI(false);
        }        

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
        for (int i = 0; i < 3; i++)
        {
            _SkillCharacteristic[i].ShowCloseUI(IsShowClose);
        }
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

        foreach (Transform SkillOneActiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.SkillOneActiveSkillPannel).transform)
        {
            Destroy(SkillOneActiveSkillItem.gameObject);
        }

        foreach (Transform SkillOnePassiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.SkillOnePassiveSkillPannel).transform)
        {
            Destroy(SkillOnePassiveSkillItem.gameObject);
        }

        foreach (Transform SkillTwoActiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.SkillTwoActiveSkillPannel).transform)
        {
            Destroy(SkillTwoActiveSkillItem.gameObject);
        }

        foreach (Transform SkillTwoPassiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.SkillTwoPassiveSkillPannel).transform)
        {
            Destroy(SkillTwoPassiveSkillItem.gameObject);
        }

        foreach (Transform SkillThreeActiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.SkillThreeActiveSkillPannel).transform)
        {
            Destroy(SkillThreeActiveSkillItem.gameObject);
        }

        foreach (Transform SkillThreePassiveSkillItem in GetGameObject((int)en_SkillBoxGameObject.SkillThreePassiveSkillPannel).transform)
        {
            Destroy(SkillThreePassiveSkillItem.gameObject);
        }
    }

    public void RefreshSkillBoxUI()
    {
        SkillItemRemove();

        GameObject PlayerGO = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
        BaseObject Player = PlayerGO.GetComponent<BaseObject>();
        
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

        if (Managers.SkillBox._Characteristics[0] != null
            && Managers.SkillBox._Characteristics[0]._SkillCharacteristicType != en_SkillCharacteristic.SKILL_CATEGORY_NONE)
        {
            foreach (st_SkillInfo PassiveSkillInfo in Managers.SkillBox._Characteristics[0].PassiveSkills)
            {
                GameObject PassiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                    GetGameObject((int)en_SkillBoxGameObject.SkillOnePassiveSkillPannel).transform);
                UI_SkillItem PassiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(PassiveSkillItemGO);
                PassiveSkillItemUI.Binding();
                PassiveSkillItemUI.SetSkillInfo(PassiveSkillInfo);

                _SkillCharacteristic[0].GetPassiveSkills().Add(PassiveSkillItemUI);
            }

            foreach (st_SkillInfo ActiveSkillInfo in Managers.SkillBox._Characteristics[0].ActiveSkills)
            {
                GameObject ActiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                    GetGameObject((int)en_SkillBoxGameObject.SkillOneActiveSkillPannel).transform);
                UI_SkillItem ActiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(ActiveSkillItemGO);
                ActiveSkillItemUI.Binding();
                ActiveSkillItemUI.SetSkillInfo(ActiveSkillInfo);

                _SkillCharacteristic[0].GetActiveSkills().Add(ActiveSkillItemUI);
            }

            CharacteristicSelectButtonShowClose(0, false);
        }        

        if(Managers.SkillBox._Characteristics[1] != null
            && Managers.SkillBox._Characteristics[1]._SkillCharacteristicType != en_SkillCharacteristic.SKILL_CATEGORY_NONE)
        {
            foreach (st_SkillInfo PassiveSkillInfo in Managers.SkillBox._Characteristics[1].PassiveSkills)
            {
                GameObject PassiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                    GetGameObject((int)en_SkillBoxGameObject.SkillTwoPassiveSkillPannel).transform);
                UI_SkillItem PassiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(PassiveSkillItemGO);
                PassiveSkillItemUI.Binding();
                PassiveSkillItemUI.SetSkillInfo(PassiveSkillInfo);

                _SkillCharacteristic[1].GetPassiveSkills().Add(PassiveSkillItemUI);
            }

            foreach (st_SkillInfo ActiveSkillInfo in Managers.SkillBox._Characteristics[1].ActiveSkills)
            {
                GameObject ActiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                    GetGameObject((int)en_SkillBoxGameObject.SkillTwoActiveSkillPannel).transform);
                UI_SkillItem ActiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(ActiveSkillItemGO);
                ActiveSkillItemUI.Binding();
                ActiveSkillItemUI.SetSkillInfo(ActiveSkillInfo);

                _SkillCharacteristic[1].GetActiveSkills().Add(ActiveSkillItemUI);
            }

            CharacteristicSelectButtonShowClose(1, false);
        }

        if (Managers.SkillBox._Characteristics[2] != null
            && Managers.SkillBox._Characteristics[2]._SkillCharacteristicType != en_SkillCharacteristic.SKILL_CATEGORY_NONE)
        {
            foreach (st_SkillInfo PassiveSkillInfo in Managers.SkillBox._Characteristics[2].PassiveSkills)
            {
                GameObject PassiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                    GetGameObject((int)en_SkillBoxGameObject.SkillThreePassiveSkillPannel).transform);
                UI_SkillItem PassiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(PassiveSkillItemGO);
                PassiveSkillItemUI.Binding();
                PassiveSkillItemUI.SetSkillInfo(PassiveSkillInfo);

                _SkillCharacteristic[2].GetPassiveSkills().Add(PassiveSkillItemUI);
            }

            foreach (st_SkillInfo ActiveSkillInfo in Managers.SkillBox._Characteristics[2].ActiveSkills)
            {
                GameObject ActiveSkillItemGO = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_ITEM,
                    GetGameObject((int)en_SkillBoxGameObject.SkillThreeActiveSkillPannel).transform);
                UI_SkillItem ActiveSkillItemUI = Util.GetOrAddComponent<UI_SkillItem>(ActiveSkillItemGO);
                ActiveSkillItemUI.Binding();
                ActiveSkillItemUI.SetSkillInfo(ActiveSkillInfo);

                _SkillCharacteristic[2].GetActiveSkills().Add(ActiveSkillItemUI);
            }

            CharacteristicSelectButtonShowClose(2, false);
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

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicsBackGround).SetActive(false);
        GetGameObject((int)en_SkillBoxGameObject.UI_SkillBoxButtons).GetComponent<RectTransform>().localPosition = new Vector3(0, 130.0f, 0);        
    }

    private void OnSkillCharacteristicButtonClick(PointerEventData Event)
    {
        PublicChracteristicShowClose(false);
        SkillCharacteristicShowClose(true);

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicsBackGround).SetActive(true);
        GetGameObject((int)en_SkillBoxGameObject.UI_SkillBoxButtons).GetComponent<RectTransform>().localPosition = new Vector3(-414.0f, 83.0f, 0);
    }

    public void SkillBoxCharacteristicShowClose(bool IsShowClose)
    {
        _PublicCharacteristic.ShowCloseUI(IsShowClose);

        for (int i = 0; i < 3; i++)
        {
            _SkillCharacteristic[i].ShowCloseUI(IsShowClose);
        }

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicsBackGround).SetActive(IsShowClose);
    }

    public void SkillBoxSkillCharacteristicShowClose(bool IsShowClose)
    {
        for (int i = 0; i < 3; i++)
        {
            _SkillCharacteristic[i].ShowCloseUI(IsShowClose);
        }

        GetGameObject((int)en_SkillBoxGameObject.SkillCharacteristicsBackGround).SetActive(IsShowClose);
    }

    public void SkillBoxSelectCharacteristicShowClose(bool IsShowClose, byte SkillCharacteristicIndex = 0)
    {
        _SelectSkilCharacteristic.ShowCloseUI(IsShowClose);
        _SelectSkilCharacteristic._SkillChracteristicIndex = SkillCharacteristicIndex;
    }

    public void SkillBoxButtonShowClose(bool IsShowClose)
    {
        GetButton((int)en_SkillBoxButton.PublicCharacteristicButton).gameObject.SetActive(IsShowClose);
        GetButton((int)en_SkillBoxButton.SkillCharacteristicButton).gameObject.SetActive(IsShowClose);
    }

    public void CharacteristicSelectButtonShowClose(byte SkillCharacteristicIndex, bool IsShowClose)
    {
        _SkillCharacteristic[SkillCharacteristicIndex].CharcteristicSelectButtonShowClose(IsShowClose);
    }
}
