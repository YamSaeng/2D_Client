using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_TargetHUD : UI_Base
{
    public CBaseObject _TargetObject;

    public Dictionary<en_SkillType, UI_BufDebufItem> _BufItems = new Dictionary<en_SkillType, UI_BufDebufItem>();
    public Dictionary<en_SkillType, UI_BufDebufItem> _DeBufItems = new Dictionary<en_SkillType, UI_BufDebufItem>();    

    enum en_TargetHUDGameObject
    {
        TargetBufList,
        TargetDeBufList,
        DP
    }

    enum en_TargetHUDSlider
    {
        TargetHealthBar,
        TargetManaBar,
        TargetDivineBar
    }

    enum en_TextHUDText
    {
        TargetNameText,
        TargetLevelText,
        TargetHPText,
        TargetMaxHPText,
        TargetMPText,
        TargetMaxMPText,
        TargetDPText,
        TargetMaxDPText,
        TargetDistanceText
    }

    public override void Init()
    {
        
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_TargetHUDGameObject));
        Bind<Slider>(typeof(en_TargetHUDSlider));
        Bind<TextMeshProUGUI>(typeof(en_TextHUDText));

        GetComponent<RectTransform>().localPosition = new Vector3(380.0f, -150.0f, 0);
    }

    public void TargetHUDUpdate()
    {
        if(_TargetObject != null)
        {
            float CurrentHPRatio = 0.0f;
            float CurrentMPRatio = 0.0f;
            float CurrentDPRatio = 0.0f;

            CurrentHPRatio = ((float)_TargetObject._GameObjectInfo.ObjectStatInfo.HP) / _TargetObject._GameObjectInfo.ObjectStatInfo.MaxHP;
            CurrentMPRatio = ((float)_TargetObject._GameObjectInfo.ObjectStatInfo.MP) / _TargetObject._GameObjectInfo.ObjectStatInfo.MaxMP;
            CurrentDPRatio = ((float)_TargetObject._GameObjectInfo.ObjectStatInfo.DP) / _TargetObject._GameObjectInfo.ObjectStatInfo.MaxDP;

            GetTextMeshPro((int)en_TextHUDText.TargetNameText).text = _TargetObject._GameObjectInfo.ObjectName;
            GetTextMeshPro((int)en_TextHUDText.TargetLevelText).text = _TargetObject._GameObjectInfo.ObjectStatInfo.Level.ToString();

            GetSlider((int)en_TargetHUDSlider.TargetHealthBar).value = CurrentHPRatio;
            GetTextMeshPro((int)en_TextHUDText.TargetHPText).text = _TargetObject._GameObjectInfo.ObjectStatInfo.HP.ToString();
            GetTextMeshPro((int)en_TextHUDText.TargetMaxHPText).text = _TargetObject._GameObjectInfo.ObjectStatInfo.MaxHP.ToString();
            
            GetSlider((int)en_TargetHUDSlider.TargetManaBar).value = CurrentMPRatio;
            GetTextMeshPro((int)en_TextHUDText.TargetMPText).text = _TargetObject._GameObjectInfo.ObjectStatInfo.MP.ToString();
            GetTextMeshPro((int)en_TextHUDText.TargetMaxMPText).text = _TargetObject._GameObjectInfo.ObjectStatInfo.MaxMP.ToString();

            GetSlider((int)en_TargetHUDSlider.TargetDivineBar).value = CurrentDPRatio;
            GetTextMeshPro((int)en_TextHUDText.TargetDPText).text = _TargetObject._GameObjectInfo.ObjectStatInfo.DP.ToString();
            GetTextMeshPro((int)en_TextHUDText.TargetMaxDPText).text = _TargetObject._GameObjectInfo.ObjectStatInfo.MaxDP.ToString();
        }        
    }

    public void TargetHUDOn(CBaseObject Target)
    {
        _TargetObject = Target;
        
        gameObject.SetActive(true);

        gameObject.transform.SetAsLastSibling();

        if (_TargetObject._GameObjectInfo.ObjectStatInfo.MaxDP == 0)
        {
            GetGameObject((int)en_TargetHUDGameObject.DP).gameObject.SetActive(false);
        }
        else
        {
            GetGameObject((int)en_TargetHUDGameObject.DP).gameObject.SetActive(true);
        }
    }

    public void TargetHUDOff()
    {
        _TargetObject = null;
        gameObject.SetActive(false);
    }

    // 버프 아이템 업데이트
    public void TargetHUDBufUpdate(en_SkillType SkillType)
    {
        // 해당 스킬 정보 가지고 옴
        st_SkillInfo BufSkillInfo = _TargetObject._Bufs.Values
                .FirstOrDefault(FindSkillInfo => FindSkillInfo.SkillType == SkillType);

        if(_BufItems.Count > 0)
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
               GetGameObject((int)en_TargetHUDGameObject.TargetBufList).transform);
                // 강화효과 스킬아이템의 정보를 설정한다.
                UI_BufDebufItem BufItemUI = BufItem.GetComponent<UI_BufDebufItem>();
                BufItemUI.SetSkillBufItem(1.0f, BufSkillInfo);
                _BufItems.Add(BufSkillInfo.SkillType, BufItemUI);
            }
        }
        else
        {           
            GameObject BufItem = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_BUF_DEBUF,
                GetGameObject((int)en_TargetHUDGameObject.TargetBufList).transform);
            // 강화효과 스킬아이템의 정보를 설정한다.
            UI_BufDebufItem BufItemUI = BufItem.GetComponent<UI_BufDebufItem>();
            BufItemUI.SetSkillBufItem(1.0f, BufSkillInfo);
            _BufItems.Add(BufSkillInfo.SkillType, BufItemUI);
        } 
    }

    public void TargetHUDDeBufUpdate(en_SkillType SkillType)
    {
        // 해당 스킬 정보 가지고 옴
        st_SkillInfo DeBufSkillInfo = _TargetObject._DeBufs.Values
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
               GetGameObject((int)en_TargetHUDGameObject.TargetDeBufList).transform);
                // 강화효과 스킬아이템의 정보를 설정한다.
                UI_BufDebufItem DeBufItemUI = DeBufItem.GetComponent<UI_BufDebufItem>();
                DeBufItemUI.SetSkillBufItem(1.0f, DeBufSkillInfo);
                _DeBufItems.Add(DeBufSkillInfo.SkillType, DeBufItemUI);
            }
        }
        else
        {
            GameObject DeBufItem = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_SKILL_BUF_DEBUF,
                GetGameObject((int)en_TargetHUDGameObject.TargetDeBufList).transform);
            // 강화효과 스킬아이템의 정보를 설정한다.
            UI_BufDebufItem DeBufItemUI = DeBufItem.GetComponent<UI_BufDebufItem>();
            DeBufItemUI.SetSkillBufItem(1.0f, DeBufSkillInfo);
            _DeBufItems.Add(DeBufSkillInfo.SkillType, DeBufItemUI);
        }
    }    

    public void TargetHUDBufUIDelete(en_SkillType DeleteSkillType)
    {
        Destroy(_BufItems.Values
                .FirstOrDefault(BufItem => BufItem._SkillInfo.SkillType == DeleteSkillType).gameObject);
                
        _BufItems.Remove(DeleteSkillType);        
    }

    public void TargetHUDDeBufUIDelete(en_SkillType DeleteSkillType)
    {
        Destroy(_DeBufItems.Values
                .FirstOrDefault(DeBufItem => DeBufItem._SkillInfo.SkillType == DeleteSkillType).gameObject);

        _DeBufItems.Remove(DeleteSkillType);       
    }

    public void TargetHUDBufDebufEmpty()
    {        
        if(_BufItems.Count > 0)
        {
            foreach (UI_BufDebufItem BufItem in _BufItems.Values)
            {
                Destroy(BufItem.gameObject);                
            }
        }        

        if(_DeBufItems.Count > 0)
        {
            foreach (UI_BufDebufItem DeBufItem in _DeBufItems.Values)
            {
                Destroy(DeBufItem.gameObject);                
            }
        }

        _BufItems.Clear();
        _DeBufItems.Clear();
    }

    private void Update()
    {
        if(_TargetObject != null)
        {
            if(_TargetObject._GameObjectInfo.ObjectId == Managers.NetworkManager._PlayerDBId)
            {                
                if(GetTextMeshPro((int)en_TextHUDText.TargetDistanceText))
                {
                    GetTextMeshPro((int)en_TextHUDText.TargetDistanceText).text = "";
                }                
            }
            else
            {
                UI_GameScene gameScene = Managers.UI._SceneUI as UI_GameScene;

                PlayerObject MyPlayer = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId).GetComponent<PlayerObject>();

                Vector3 CenterTopObjectPosition = _TargetObject.transform.position;
                Vector3 MyPlayerPosition = MyPlayer.transform.position;

                float Distance = Vector3.Distance(CenterTopObjectPosition, MyPlayerPosition);

                GetTextMeshPro((int)en_TextHUDText.TargetDistanceText).text = "거리 : " + Distance.ToString("F1");
            }
        }
    }
}
