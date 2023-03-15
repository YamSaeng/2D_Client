using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoxManager
{    
    public SkillCharacteristic _PublicCharacteristic = new SkillCharacteristic();
    public SkillCharacteristic _Characteristic = new SkillCharacteristic();

    public SkillBoxManager()
    {

    }

    public void Init()
    {
        
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

    public void CreateChracteristicPassive(en_SkillCharacteristic CharacteristicType,
        byte PassiveSkillCount, st_SkillInfo[] PassiveSkillInfos)
    {
        _Characteristic.PassiveSkillCharacteristicInit(CharacteristicType, PassiveSkillCount, PassiveSkillInfos);
    }

    public void CreateChracteristicActive(en_SkillCharacteristic ChracteristicType,
        byte ActiveSkillCount, st_SkillInfo[] ActiveSkillInfos)
    {
        _Characteristic.ActiveSkillCharacteristicInit(ChracteristicType, ActiveSkillCount, ActiveSkillInfos);
    }

    public void SetSkillLearn(short SkillType, bool IsSkillLearn)
    {
        st_SkillInfo Skill = _Characteristic.FindSkill(SkillType);
        if(Skill != null)
        {
            Skill.IsSkillLearn = IsSkillLearn;
        }        
    }
}
