using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_QuickSlotBar : UI_Base
{
    enum en_SkillQuickBarGameObject
    {
        QuickSlotBar,
        QuickBarSlotList
    }

    public byte _QuickSlotBarIndex;
    public List<UI_QuickSlotBarItem> _QuickBarButtons = new List<UI_QuickSlotBarItem>();    

    public override void Init()
    {
        
    }

    public override void Binding()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }


    public void QuickSlotBarUICreate(byte QuickSlotBarIndex, byte QuickSlotBarSlotSize)
    {
        Bind<GameObject>(typeof(en_SkillQuickBarGameObject));

        foreach (Transform Child in GetGameObject((int)en_SkillQuickBarGameObject.QuickBarSlotList).transform)
        {
            Destroy(Child.gameObject);
        }

        _QuickSlotBarIndex = QuickSlotBarIndex;        

        for (byte i = 0; i < QuickSlotBarSlotSize; i++)
        {            
            UI_QuickSlotBarItem NewSkillQuickBar = QuickSlotCreate(i);            

            _QuickBarButtons.Add(NewSkillQuickBar);
        }
    }
                                                                                        
    public UI_QuickSlotBarItem QuickSlotCreate(byte QuickSlotBarSlotIndex)
    {        
        GameObject Go = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_QUICK_SLOT_BAR_BUTTON, GetGameObject((int)en_SkillQuickBarGameObject.QuickBarSlotList).transform);
        UI_QuickSlotBarItem NewSkillQuickBar = Util.GetOrAddComponent<UI_QuickSlotBarItem>(Go);
        RectTransform QuickSlotRectTransform = NewSkillQuickBar.GetComponent<RectTransform>();
        QuickSlotRectTransform.localPosition = new Vector3(QuickSlotBarSlotIndex * 82.0f, 0, 0);

        return NewSkillQuickBar;
    }

    public void RefreshSkillQuickBarUI()
    {
        if (_QuickBarButtons.Count == 0)
        {
            return;
        }

        List<QuickSlotBar> QuickSlotBars = Managers.QuickSlotBar._SkillQuickSlotBars.Values.ToList();

        foreach (QuickSlotBar quickSlotBar in QuickSlotBars)
        {
            foreach (st_QuickSlotBarSlotInfo quickSlotBarSlotInfo in quickSlotBar._QuickSlotBarSlotInfos.Values.ToList())
            {
                if(_QuickSlotBarIndex == quickSlotBar._QuickSlotBarIndex)
                {
                    _QuickBarButtons[quickSlotBarSlotInfo.QuickSlotBarSlotIndex].SetQuickBarItem(quickSlotBarSlotInfo);
                }                
            }          
        }
    }

    public void QuickSlotBarCoolTimeStart(byte QuickSlotBarSlotIndex, float SkillCoolTimeSpeed)
    {
        // 관리하고 있는 배열인덱스에 접근하고, 최종적으로 쿨타임을 기록한다.
        _QuickBarButtons[QuickSlotBarSlotIndex].QuickSlotBarItemCoolTimeStart(SkillCoolTimeSpeed);
    }

    public void QuickSlotBarCoolTimeStart(byte QuickSlotBarSlotIndex, int CoolTime)
    {
        _QuickBarButtons[QuickSlotBarSlotIndex].QuickSlotBarItemCoolTimeStart(CoolTime);
    }

    public void QuickSlotBarCoolTimeStop(byte QuickSlotBarSlotIndex)
    {
        _QuickBarButtons[QuickSlotBarSlotIndex].QuickSlotBarItemCoolTimeStop();
    }

    public GameObject GetQuickSlotBar()
    {
        return GetGameObject((int)en_SkillQuickBarGameObject.QuickSlotBar);
    }

    public bool IsCollision(UI_Base CollisionUI)
    {
        RectTransform QuickSlotBarRect = GetComponent<RectTransform>();
        RectTransform CollisitonUIRect = CollisionUI.GetComponent<RectTransform>();

        if(QuickSlotBarRect.transform.position.x - QuickSlotBarRect.rect.width / 2 < CollisitonUIRect.transform.position.x + CollisitonUIRect.rect.width / 2
               && QuickSlotBarRect.transform.position.x + QuickSlotBarRect.rect.width / 2 > CollisitonUIRect.transform.position.x + CollisitonUIRect.rect.width / 2
               && QuickSlotBarRect.transform.position.y - QuickSlotBarRect.rect.height / 2 < CollisitonUIRect.transform.position.y
               && QuickSlotBarRect.transform.position.y + QuickSlotBarRect.rect.height / 2 > CollisitonUIRect.transform.position.y)
        {
            return true;
        }

        return false;
    }
}
