using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotBarManager
{
    public Dictionary<byte, QuickSlotBar> _SkillQuickSlotBars { get; } = new Dictionary<byte, QuickSlotBar>();

    public void Init(byte QuickSlotBarSize, byte QuickSlotBarSlotSize)
    {
        for (byte BarSlotIndex = 0; BarSlotIndex < QuickSlotBarSize; ++BarSlotIndex)
        {
            QuickSlotBar quickSlotBar = new QuickSlotBar();
            quickSlotBar._QuickSlotBarIndex = BarSlotIndex;
            quickSlotBar.Init(QuickSlotBarSlotSize);

            _SkillQuickSlotBars.Add(BarSlotIndex, quickSlotBar);
        }
    }    

    public void UpdateQuickSlotBarSlot(st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo)
    {
        _SkillQuickSlotBars[QuickSlotBarSlotInfo.QuickSlotBarIndex].UpdateQuickSlotBarSlot(QuickSlotBarSlotInfo);
    }

    public void QuickSlotBarEmpty(byte QuickSlotBarIndex,byte QuickSlotBarSlotIndex)
    {
        _SkillQuickSlotBars[QuickSlotBarIndex].QuickSlotBarSlotEmpty(QuickSlotBarSlotIndex);
    }

    // 키가 등록되어 있는 퀵슬롯바를 찾아서 반환한다.
    public st_QuickSlotBarSlotInfo FindExekey(en_UserQuickSlot UserQuickSlot)
    {
        bool IsFindQuickSlotInfo = true;
        byte QuickSlotBarIndex = 0;
        byte QuickSlotBarSlotIndex = 0;

        switch (UserQuickSlot)
        {
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_ONE:
                QuickSlotBarIndex = 0;
                QuickSlotBarSlotIndex = 0;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_TWO:
                QuickSlotBarIndex = 0;
                QuickSlotBarSlotIndex = 1;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_THREE:
                QuickSlotBarIndex = 0;
                QuickSlotBarSlotIndex = 2;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_FOUR:
                QuickSlotBarIndex = 0;
                QuickSlotBarSlotIndex = 3;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_FIVE:
                QuickSlotBarIndex = 0;
                QuickSlotBarSlotIndex = 4;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_ONE:
                QuickSlotBarIndex = 1;
                QuickSlotBarSlotIndex = 0;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_TWO:
                QuickSlotBarIndex = 1;
                QuickSlotBarSlotIndex = 1;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_THREE:
                QuickSlotBarIndex = 1;
                QuickSlotBarSlotIndex = 2;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_FOUR:
                QuickSlotBarIndex = 1;
                QuickSlotBarSlotIndex = 3;
                break;
            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_FIVE:
                QuickSlotBarIndex = 1;
                QuickSlotBarSlotIndex = 4;
                break;
            default:
                IsFindQuickSlotInfo = false;
                break;
        }

        if(IsFindQuickSlotInfo == true)
        {
            return FindQuickSlot(QuickSlotBarIndex, QuickSlotBarSlotIndex);
        }
        else
        {
            return null;
        }        
    }  

    public st_QuickSlotBarSlotInfo FindQuickSlot(byte QuickSlotBarIndex, byte QuickSlotBarSlotIndex)
    {
        return _SkillQuickSlotBars[QuickSlotBarIndex]._QuickSlotBarSlotInfos[QuickSlotBarSlotIndex];
    }

    public void SwapQuickSlot(st_QuickSlotBarSlotInfo AQuickSlotInfo, st_QuickSlotBarSlotInfo BQuickSlotInfo)
    {
        foreach(QuickSlotBar quickSlotBar in _SkillQuickSlotBars.Values)
        {
            if(quickSlotBar._QuickSlotBarIndex == AQuickSlotInfo.QuickSlotBarIndex)
            {
                quickSlotBar.UpdateQuickSlotBarSlot(AQuickSlotInfo);
                break;
            }                
        }

        foreach (QuickSlotBar quickSlotBar in _SkillQuickSlotBars.Values)
        {
            if(quickSlotBar._QuickSlotBarIndex == BQuickSlotInfo.QuickSlotBarIndex)
            {
                quickSlotBar.UpdateQuickSlotBarSlot(BQuickSlotInfo);
                break;
            }   
        }
    }

    public void Clear()
    {
        _SkillQuickSlotBars.Clear();        
    }
}
