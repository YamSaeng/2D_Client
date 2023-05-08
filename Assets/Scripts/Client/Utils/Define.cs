using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const byte GameSceneUIListMax = 100;
    
    public const short DAWN = 300;
    public const short MORNING = 540;
    public const short AFTERNOON = 1040;
    public const short EVENING = 1240;
    public const short NIGHT = 1440;
    public const float MIDNIGHT = 1440.0f;

    public const float MORNING_SUNNIGHT = 0.9f;
    public const float AFTERNOON_SUNLIGHT = 0.2f;
    public const float EVENING_SUNLIGHT = 0.7f;
    public const float NIGHT_SUNLIGHT = 0.4f;     
}

public class Define 
{
    public enum en_Scene
    {
        None,
        LoginScene,        
        GameScene
    }

    public enum en_Sound
    {
        Bgm,
        Effect,
    }

    public enum en_UIEvent
    {
        PointerEnter,
        PointerExit,
        MouseClick,
        BeginDrag,
        Drag,
        EndDrag,
        Drop,
        Scroll
    }

    public enum en_MouseEvent
    {
        PRESS,
        CLICK,
    }

    public enum en_CameraMode
    {
        QUARTER_VIEW,
    }
}
