using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MyCharacterHUD : UI_Base
{
    public BaseObject _MyCharacterObject;

    public Dictionary<en_SkillType, UI_BufDebufItem> _BufItems = new Dictionary<en_SkillType, UI_BufDebufItem>();
    public Dictionary<en_SkillType, UI_BufDebufItem> _DeBufItems = new Dictionary<en_SkillType, UI_BufDebufItem>();

    enum en_MyCharacterHUDSlider
    {
        MyCharacterHealthBar,
        MyCharacterManaBar,
        MyCharacterDivineBar,
    }

    enum en_MyCharacterHUDText
    {
        MyCharacterNameText,
        MyCharacterLevelText,
        CurrentHPText,
        MaxHPText,
        CurrentMPText,
        MaxMPText,
        CurrentDPText,
        MaxDPText
    }

    enum en_MyCharacterHUDGameObject
    {
        MyCharacterHUDBufList,
        MyCharacterHUDDeBufList
    }

    public override void Init()
    {        
    }

    public override void Binding()
    {
        Bind<Slider>(typeof(en_MyCharacterHUDSlider));
        Bind<TextMeshProUGUI>(typeof(en_MyCharacterHUDText));
        Bind<GameObject>(typeof(en_MyCharacterHUDGameObject));

        GetComponent<RectTransform>().localPosition = new Vector3(-340.0f, -250.0f, 0);
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public void MyCharacterHUDUpdate()
    {
        if(_MyCharacterObject != null)
        {
            float CurrentHPRatio = 0.0f;
            float CurrentMPRatio = 0.0f;
            float CurrentDPRatio = 0.0f;

            CurrentHPRatio = ((float)_MyCharacterObject._GameObjectInfo.ObjectStatInfo.HP) / _MyCharacterObject._GameObjectInfo.ObjectStatInfo.MaxHP;
            CurrentMPRatio = ((float)_MyCharacterObject._GameObjectInfo.ObjectStatInfo.MP) / _MyCharacterObject._GameObjectInfo.ObjectStatInfo.MaxMP;
            CurrentDPRatio = ((float)_MyCharacterObject._GameObjectInfo.ObjectStatInfo.DP) / _MyCharacterObject._GameObjectInfo.ObjectStatInfo.MaxDP;
                        
            GetTextMeshPro((int)en_MyCharacterHUDText.MyCharacterNameText).text = _MyCharacterObject._GameObjectInfo.ObjectName;
            GetTextMeshPro((int)en_MyCharacterHUDText.MyCharacterLevelText).text = _MyCharacterObject._GameObjectInfo.ObjectStatInfo.Level.ToString();

            GetSlider((int)en_MyCharacterHUDSlider.MyCharacterHealthBar).value = CurrentHPRatio;
            GetTextMeshPro((int)en_MyCharacterHUDText.CurrentHPText).text = _MyCharacterObject._GameObjectInfo.ObjectStatInfo.HP.ToString();
            GetTextMeshPro((int)en_MyCharacterHUDText.MaxHPText).text = _MyCharacterObject._GameObjectInfo.ObjectStatInfo.MaxHP.ToString();

            GetSlider((int)en_MyCharacterHUDSlider.MyCharacterManaBar).value = CurrentMPRatio;
            GetTextMeshPro((int)en_MyCharacterHUDText.CurrentMPText).text = _MyCharacterObject._GameObjectInfo.ObjectStatInfo.MP.ToString();
            GetTextMeshPro((int)en_MyCharacterHUDText.MaxMPText).text = _MyCharacterObject._GameObjectInfo.ObjectStatInfo.MaxMP.ToString();

            GetSlider((int)en_MyCharacterHUDSlider.MyCharacterDivineBar).value = CurrentDPRatio;
            GetTextMeshPro((int)en_MyCharacterHUDText.CurrentDPText).text = _MyCharacterObject._GameObjectInfo.ObjectStatInfo.DP.ToString();
            GetTextMeshPro((int)en_MyCharacterHUDText.MaxDPText).text = _MyCharacterObject._GameObjectInfo.ObjectStatInfo.MaxDP.ToString();
        }        
    }

    public void MyCharacterBufUpdate(en_SkillType SkillType)
    {
        // 해당 스킬 정보 가지고 옴
        st_SkillInfo BufSkillInfo = _MyCharacterObject._Bufs.Values
                .FirstOrDefault(FindSkillInfo => FindSkillInfo.SkillType == SkillType);

        if (_BufItems.Count > 0)
        {
            // CenterTopUI가 소유중인 강화효과 목록에서 위에서 찾은 스킬 아이콘을 찾음
            UI_BufDebufItem FindBufItemUI = _BufItems.Values
                .FirstOrDefault(BufItem => BufItem._SkillInfo.SkillType == BufSkillInfo.SkillType);
            if (FindBufItemUI != null)
            {
                FindBufItemUI.SetSkillBufItem(1.0f, BufSkillInfo);
            }
            else
            {
                GameObject BufItem = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_BUF_DEBUF,
               GetGameObject((int)en_MyCharacterHUDGameObject.MyCharacterHUDBufList).transform);
                // 강화효과 스킬아이템의 정보를 설정한다.
                UI_BufDebufItem BufItemUI = BufItem.GetComponent<UI_BufDebufItem>();
                BufItemUI.SetSkillBufItem(1.0f, BufSkillInfo);
                _BufItems.Add(BufSkillInfo.SkillType, BufItemUI);
            }
        }
        else
        {
            GameObject BufItem = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_BUF_DEBUF,
                GetGameObject((int)en_MyCharacterHUDGameObject.MyCharacterHUDBufList).transform);
            // 강화효과 스킬아이템의 정보를 설정한다.
            UI_BufDebufItem BufItemUI = BufItem.GetComponent<UI_BufDebufItem>();
            BufItemUI.SetSkillBufItem(1.0f, BufSkillInfo);
            _BufItems.Add(BufSkillInfo.SkillType, BufItemUI);
        }
    }

    public void MyCharacterDebufUpdate(en_SkillType SkillType)
    {
        // 해당 스킬 정보 가지고 옴
        st_SkillInfo DeBufSkillInfo = _MyCharacterObject._DeBufs.Values
                .FirstOrDefault(FindSkillInfo => FindSkillInfo.SkillType == SkillType);

        if (_DeBufItems.Count > 0)
        {
            // CenterTopUI가 소유중인 강화효과 목록에서 위에서 찾은 스킬 아이콘을 찾음
            UI_BufDebufItem FindBufItemUI = _DeBufItems.Values
                .FirstOrDefault(DeBufItem => DeBufItem._SkillInfo.SkillType == DeBufSkillInfo.SkillType);
            if (FindBufItemUI != null)
            {
                FindBufItemUI.SetSkillBufItem(1.0f, DeBufSkillInfo);
            }
            else
            {
                GameObject DeBufItem = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_BUF_DEBUF,
               GetGameObject((int)en_MyCharacterHUDGameObject.MyCharacterHUDDeBufList).transform);
                // 강화효과 스킬아이템의 정보를 설정한다.
                UI_BufDebufItem DeBufItemUI = DeBufItem.GetComponent<UI_BufDebufItem>();
                DeBufItemUI.SetSkillBufItem(1.0f, DeBufSkillInfo);
                _DeBufItems.Add(DeBufSkillInfo.SkillType, DeBufItemUI);
            }
        }
        else
        {
            GameObject DeBufItem = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_BUF_DEBUF,
                GetGameObject((int)en_MyCharacterHUDGameObject.MyCharacterHUDDeBufList).transform);
            // 강화효과 스킬아이템의 정보를 설정한다.
            UI_BufDebufItem DeBufItemUI = DeBufItem.GetComponent<UI_BufDebufItem>();
            DeBufItemUI.SetSkillBufItem(1.0f, DeBufSkillInfo);
            _DeBufItems.Add(DeBufSkillInfo.SkillType, DeBufItemUI);
        }
    }

    public void MyCharacterBufUIDelete(en_SkillType DeleteBufSkillType)
    {
        Destroy(_BufItems.Values
                       .FirstOrDefault(BufItem => BufItem._SkillInfo.SkillType == DeleteBufSkillType).gameObject);

        _BufItems.Remove(DeleteBufSkillType);
    }

    public void MyCharacterDeBufUIDelete(en_SkillType DeleteDeBufSkillType)
    {
        Destroy(_DeBufItems.Values
                .FirstOrDefault(DeBufItem => DeBufItem._SkillInfo.SkillType == DeleteDeBufSkillType).gameObject);

        _DeBufItems.Remove(DeleteDeBufSkillType);      
    }    
}
