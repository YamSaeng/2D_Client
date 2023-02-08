using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCharacteristic
{
    public en_SkillCharacteristic _SkillCharacteristicType;
    public byte _SkillBoxIndex;

    public List<st_SkillInfo> PassiveSkills { get; } = new List<st_SkillInfo> { };
    public List<st_SkillInfo> ActiveSkills { get; } = new List<st_SkillInfo> { };    

    public void PassiveSkillCharacteristicInit(en_SkillCharacteristic ChracteristicType,
        byte PassiveSkillCount, st_SkillInfo[] PassiveSkillInfo)
    {
        _SkillCharacteristicType = ChracteristicType;

        for (byte i = 0; i < PassiveSkillCount; i++)
        {
            PassiveSkills.Add(PassiveSkillInfo[i]);
        }        
    }

    public void ActiveSkillCharacteristicInit(en_SkillCharacteristic ChracteristicType, 
        byte ActiveSkillCount, st_SkillInfo[] ActiveSkillInfo)
    {
        _SkillCharacteristicType = ChracteristicType;

        for (byte i = 0; i < ActiveSkillCount; i++)
        {
            ActiveSkills.Add(ActiveSkillInfo[i]);
        }
    }

    public st_SkillInfo FindSkill(short SkillType)
    {
        foreach(st_SkillInfo PassiveSkill in PassiveSkills)
        {
            if(PassiveSkill.SkillType == (en_SkillType)SkillType)
            {
                return PassiveSkill;
            }
        }

        foreach(st_SkillInfo ActiveSkill in ActiveSkills)
        {
            if(ActiveSkill.SkillType == (en_SkillType)SkillType)
            {
                return ActiveSkill;
            }
        }

        return null;
    }
}
