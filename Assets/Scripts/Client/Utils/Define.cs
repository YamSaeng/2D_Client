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

    public const byte WRRIOR_CHOHONE =          0b00000001;
    public const byte WARRIOR_SHAEHONE =        0b00000010;
    public const byte SHAMAN_ROOT =             0b00000100;
    public const byte SHAMAN_ICE_CHAIN =        0b00001000;
    public const byte SHAMAN_ICE_WAVE =         0b00010000;
    public const byte SHAMAN_LIGHTNING_STRIKE = 0b00100000;
    public const byte TAIOIST_ROOT =            0b01000000;

    public const byte WRRIOR_CHOHONE_MASK =               0b11111110;
    public const byte WARRIOR_SHAEHONE_MASK =             0b11111101;
    public const byte SHAMAN_ROOT_MASK =                  0b11111011;
    public const byte SHAMAN_ICE_CHAIN_MASK =             0b11110111;
    public const byte SHAMAN_ICE_WAVE_MASK =              0b11101111;
    public const byte SHAMAN_LIGHTNING_STRIKE_MASK =      0b11011111;
    public const byte TAIOIST_ROOT_MASK =                 0b10111111;
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
