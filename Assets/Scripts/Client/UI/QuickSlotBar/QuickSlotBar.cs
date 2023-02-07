using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotBar
{
    public byte _QuickSlotBarIndex;
    public Dictionary<byte, st_QuickSlotBarSlotInfo> _QuickSlotBarSlotInfos { get; } = new Dictionary<byte, st_QuickSlotBarSlotInfo>();

    public void Init(byte QuickSlotBarSlotSize)
    {
        for (byte SlotIndex = 0; SlotIndex < QuickSlotBarSlotSize; ++SlotIndex)
        {
            st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo = new st_QuickSlotBarSlotInfo();
            QuickSlotBarSlotInfo.QuickSlotBarType = en_QuickSlotBarType.QUICK_SLOT_BAR_TYPE_NONE;
            QuickSlotBarSlotInfo.QuickSlotBarIndex = _QuickSlotBarIndex;
            QuickSlotBarSlotInfo.QuickSlotBarSlotIndex = SlotIndex;

            _QuickSlotBarSlotInfos.Add(SlotIndex, QuickSlotBarSlotInfo);
        }
    }

    public void UpdateQuickSlotBarSlot(st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo)
    {
        _QuickSlotBarSlotInfos[QuickSlotBarSlotInfo.QuickSlotBarSlotIndex] = QuickSlotBarSlotInfo;
    }

    public void QuickSlotBarSlotEmpty(byte QuickSlotBarSlotIndex)
    {
        _QuickSlotBarSlotInfos[QuickSlotBarSlotIndex].QuickSlotBarType = en_QuickSlotBarType.QUICK_SLOT_BAR_TYPE_NONE;
        _QuickSlotBarSlotInfos[QuickSlotBarSlotIndex].QuickBarSkillInfo = null;
        _QuickSlotBarSlotInfos[QuickSlotBarSlotIndex].QuickBarItemInfo = null;
    }
}
