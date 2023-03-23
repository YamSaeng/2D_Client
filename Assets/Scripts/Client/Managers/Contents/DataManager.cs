using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    Dictionary<en_SkillType, st_SkillInfo> _SkillDatas = new Dictionary<en_SkillType, st_SkillInfo>();

    public void InitSkillDatas(st_SkillInfo[] SkillDatas)
    {
        foreach(st_SkillInfo SkillData in SkillDatas)
        {
            _SkillDatas.Add(SkillData.SkillType, SkillData);
        }
    }
}
