using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_QuickSlotBarBox : UI_Base
{
    enum en_QuickSlotBoxGameObject
    {
        UI_QuickSlotBarBox,
        QuickBarList
    }    

    public Dictionary<byte, UI_QuickSlotBar> _QuickSlotBars { get; private set; } = new Dictionary<byte, UI_QuickSlotBar>();

    public List<UI_QuickSlotBarItem> _ComboSkillQuickSlotBars = new List<UI_QuickSlotBarItem>();

    public override void Init()
    {

    }

    public override void Binding()
    {
        Bind<GameObject>(typeof(en_QuickSlotBoxGameObject));
    }

    public override void ShowCloseUI(bool IsShowClose)
    {
        gameObject.SetActive(IsShowClose);
    }


    public void UIQuickSlotBarBoxCreate(byte QuickSlotBarSize, byte QuickSlotBarSlotSize)
    {
        Binding();

        _QuickSlotBars.Clear();

        foreach (Transform Child in GetGameObject((int)en_QuickSlotBoxGameObject.QuickBarList).transform)
        {
            Destroy(Child.gameObject);
        }

        for (byte i = 0; i < QuickSlotBarSize; i++)
        {
            GameObject UIQuickSlotBarGo = Managers.Resource.Instantiate(en_ResourceName.CLIENT_UI_QUICK_SLOT_BAR, GetGameObject((int)en_QuickSlotBoxGameObject.QuickBarList).transform);
            UI_QuickSlotBar QuickSlotBar = Util.GetOrAddComponent<UI_QuickSlotBar>(UIQuickSlotBarGo);
            QuickSlotBar.QuickSlotBarUICreate(i, QuickSlotBarSlotSize);

            _QuickSlotBars.Add(i, QuickSlotBar);
        }
    }

    public void QuickSlotBoxComboSkillOff()
    {
        foreach (UI_QuickSlotBarItem QuickSlotBarItem in _ComboSkillQuickSlotBars)
        {
            Destroy(QuickSlotBarItem.gameObject);
        }

        _ComboSkillQuickSlotBars.Clear();
    }

    public void RefreshQuickSlotBarBoxUI()
    {
        if (_QuickSlotBars.Count == 0)
        {
            return;
        }

        List<UI_QuickSlotBar> QuickSlotBars = _QuickSlotBars.Values.ToList();

        foreach (UI_QuickSlotBar QuickSlotBar in QuickSlotBars)
        {
            QuickSlotBar.RefreshSkillQuickBarUI();
        }
    }

    // 퀵슬롯이 가지고 있는 스킬 정보를 토대로 쿨타임 적용
    public void QuickSlotBarBoxCoolTimerStart(byte QuickSlotBarIndex, byte QuickSlotBarSlotIndex, float SkillCoolTimeSpeed)
    {
        // 관리하고 있는 QuickSlotBar를 가져온다.
        List<UI_QuickSlotBar> QuickSlotBars = _QuickSlotBars.Values.ToList();

        // 입력받은 인덱스와 같은 퀵슬롯바를 찾는다.
        foreach (UI_QuickSlotBar QuickSlotBar in QuickSlotBars)
        {
            if(QuickSlotBar._QuickSlotBarIndex == QuickSlotBarIndex)
            {
                // 찾았으면 퀵슬롯바 슬롯과 쿨타임 시간, 스피드를 전달한다.
                QuickSlotBar.QuickSlotBarCoolTimeStart(QuickSlotBarSlotIndex, SkillCoolTimeSpeed);
                break;
            }
        }
    }

    // 쿨타임 시간을 받아서 해당 퀵슬롯에 쿨타임 적용
    public void QuickSlotBarBoxCoolTimerStart(byte QuickSlotBarIndex, byte QuickSlotBarSlotIndex, int CoolTime)
    {
        // 관리하고 있는 QuickSlotBar를 가져온다.
        List<UI_QuickSlotBar> QuickSlotBars = _QuickSlotBars.Values.ToList();

        foreach(UI_QuickSlotBar QuickSlotBar in QuickSlotBars)
        {
            if(QuickSlotBar._QuickSlotBarIndex == QuickSlotBarIndex)
            {
                QuickSlotBar.QuickSlotBarCoolTimeStart(QuickSlotBarSlotIndex, CoolTime);
                break;
            }
        }
    }

    // 쿨타임 멈춤
    public void QuickSlotBarBoxCoolTimeStop(byte QuickSlotBarIndex, byte QuickSlotBarSlotIndex)
    {
        List<UI_QuickSlotBar> QuickSlotBars = _QuickSlotBars.Values.ToList();

        foreach(UI_QuickSlotBar QuickSlotBar in QuickSlotBars)
        {
            if(QuickSlotBar._QuickSlotBarIndex == QuickSlotBarIndex)
            {
                QuickSlotBar.QuickSlotBarCoolTimeStop(QuickSlotBarSlotIndex);
            }
        }
    }
    
    public bool IsCollision(UI_Base CollisionUI)
    {
        List<UI_QuickSlotBar> QuickSlotBars = _QuickSlotBars.Values.ToList();

        // 퀵슬롯 박스와 충돌하는지 판단해준다.
        foreach (UI_QuickSlotBar QuickSlotBar in QuickSlotBars)
        {
            RectTransform QuickSlotBarRect = QuickSlotBar.GetComponent<RectTransform>();
            RectTransform DragUIRect = CollisionUI.GetComponent<RectTransform>();          

            if(QuickSlotBarRect.transform.position.x - QuickSlotBarRect.rect.width / 2 < DragUIRect.transform.position.x + DragUIRect.rect.width / 2
               && QuickSlotBarRect.transform.position.x + QuickSlotBarRect.rect.width / 2 > DragUIRect.transform.position.x + DragUIRect.rect.width / 2
               && QuickSlotBarRect.transform.position.y - QuickSlotBarRect.rect.height / 2 < DragUIRect.transform.position.y
               && QuickSlotBarRect.transform.position.y + QuickSlotBarRect.rect.height / 2 > DragUIRect.transform.position.y)
            {
                // 부딪힘 true
                return true;
            }                      
        }

        // 안부딪힘 false
        return false;
    }    
}
