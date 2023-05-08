using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CDay : MonoBehaviour
{
    bool IsServerDayTime = false;

    public st_Day _DayTime = new st_Day();
    
    public Light2D _GlobalLight2D;

    public void Init(st_Day ServerDayTime)
    {
        if(_DayTime != null)
        {
            _DayTime = ServerDayTime;

            IsServerDayTime = true;
        }
    }

    public void Start()
    {           
        
    }
        
    public void Update()
    {
        if(IsServerDayTime == true)
        {
            _DayTime.DayTimeCycle += Time.deltaTime;

            if (_DayTime.DayTimeCycle >= 1.0f)
            {
                _GlobalLight2D.intensity = _DayTime.DayRatio;

                _DayTime.DayTimeCheck += 1.0f;
                _DayTime.DayTimeCycle = 0;

                if (_DayTime.DayTimeCheck > 0 && _DayTime.DayTimeCheck <= Constants.DAWN)
                {
                    _DayTime.DayType = en_DayType.DAY_DAWN;
                    _DayTime.DayRatio = 0.1f;
                }
                else if (_DayTime.DayTimeCheck > Constants.DAWN && _DayTime.DayTimeCheck <= Constants.MORNING)
                {
                    _DayTime.DayType = en_DayType.DAY_MORNING;
                    _DayTime.DayRatio += (Constants.MORNING_SUNNIGHT / (float)(Constants.MORNING - Constants.DAWN)); // 0.00375
                }
                else if (_DayTime.DayTimeCheck > Constants.MORNING && _DayTime.DayTimeCheck <= Constants.AFTERNOON)
                {
                    _DayTime.DayType = en_DayType.DAY_AFTERNOON;
                    _DayTime.DayRatio += (Constants.AFTERNOON_SUNLIGHT / (float)(Constants.AFTERNOON - Constants.MORNING)); // 0.0004
                }
                else if (_DayTime.DayTimeCheck > Constants.AFTERNOON && _DayTime.DayTimeCheck <= Constants.EVENING)
                {
                    _DayTime.DayType = en_DayType.DAY_EVENING;
                    _DayTime.DayRatio -= (Constants.EVENING_SUNLIGHT / (float)(Constants.EVENING - Constants.AFTERNOON)); //0.0035f;
                }
                else if (_DayTime.DayTimeCheck > Constants.EVENING && _DayTime.DayTimeCheck <= Constants.NIGHT)
                {
                    _DayTime.DayType = en_DayType.DAY_NIGHT;
                    _DayTime.DayRatio -= (Constants.NIGHT_SUNLIGHT / (float)(Constants.NIGHT - Constants.EVENING)); //0.002f;
                }

                if (_DayTime.DayTimeCheck > Constants.MIDNIGHT)
                {
                    _DayTime.DayTimeCheck = 0;
                }
            }
        }        
    }
}
