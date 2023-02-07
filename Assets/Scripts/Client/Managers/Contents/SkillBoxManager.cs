using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoxManager
{    
    public SkillCharacteristic _PublicCharacteristic = new SkillCharacteristic();
    public SkillCharacteristic[] _Characteristics = new SkillCharacteristic[3];

    public SkillBoxManager()
    {

    }

    public void Init()
    {
        for (int i = 0; i < 3; i++)
        {
            _Characteristics[i] = new SkillCharacteristic();
        }
    }    

    public void CreatePublicChracteristicPassive(en_SkillCharacteristic PublicCharacteristicType,
        byte PublicPassiveSkillCount, st_SkillInfo[] PublicPassiveSkillInfos)
    {
        _PublicCharacteristic.PassiveSkillCharacteristicInit(PublicCharacteristicType, PublicPassiveSkillCount, PublicPassiveSkillInfos);
    }

    public void CreatePublicChracteristicActive(en_SkillCharacteristic PublicCharacteristicType,
        byte PublicActiveSkillCount, st_SkillInfo[] PassiveSkillInfos)
    {
        _PublicCharacteristic.ActiveSkillCharacteristicInit(PublicCharacteristicType, PublicActiveSkillCount, PassiveSkillInfos);
    }

    public void CreateChracteristicPassive(byte ChracteristicIndex, en_SkillCharacteristic CharacteristicType,
        byte PassiveSkillCount, st_SkillInfo[] PassiveSkillInfos)
    {
        _Characteristics[ChracteristicIndex].PassiveSkillCharacteristicInit(CharacteristicType, PassiveSkillCount, PassiveSkillInfos);
    }

    public void CreateChracteristicActive(byte ChracteristicIndex, en_SkillCharacteristic ChracteristicType,
        byte ActiveSkillCount, st_SkillInfo[] ActiveSkillInfos)
    {
        _Characteristics[ChracteristicIndex].ActiveSkillCharacteristicInit(ChracteristicType, ActiveSkillCount, ActiveSkillInfos);
    }

    public void SetSkillLearn(short SkillType, bool IsSkillLearn)
    {
        for (int i = 0; i < 3; i++)
        {
            st_SkillInfo Skill = _Characteristics[i].FindSkill(SkillType);
            if(Skill != null)
            {
                Skill.IsSkillLearn = IsSkillLearn;
                break;
            }
        }
    }
}
