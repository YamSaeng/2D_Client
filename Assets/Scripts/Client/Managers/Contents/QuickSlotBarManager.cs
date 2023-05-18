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
    public st_QuickSlotBarSlotInfo FindExekey(KeyCode FindKey)
    {
        for(byte SlotBarIndex = 0; SlotBarIndex < _SkillQuickSlotBars.Count;++SlotBarIndex)
        {
            for(byte SlotBarSlotIndex = 0; SlotBarSlotIndex < _SkillQuickSlotBars[SlotBarIndex]._QuickSlotBarSlotInfos.Count;++SlotBarSlotIndex)
            {
                if(_SkillQuickSlotBars[SlotBarIndex]._QuickSlotBarSlotInfos[SlotBarSlotIndex].QuickSlotKey == FindKey)
                {
                    return _SkillQuickSlotBars[SlotBarIndex]._QuickSlotBarSlotInfos[SlotBarSlotIndex];
                }                
            }
        }

        return null;
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
