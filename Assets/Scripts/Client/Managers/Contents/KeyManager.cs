using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager
{    
    // 서버로부터 받은 단축키 키 정보
    public st_BindingKey[] _BindingKeys;    
    // 퀵슬롯바에 연동되어 있는 단축키 키 정보
    private List<st_BindingKey> _QuickSlotBarBindingKeys = new List<st_BindingKey>();
    // UI 열고 닫기 단축키 키 정보
    private List<st_BindingKey> _UIBindingKeys = new List<st_BindingKey>();

    // 상, 하, 좌, 우 단축키 키 정보
    private st_BindingKey _MoveUpKeyBinding;
    private st_BindingKey _MoveDownKeyBinding;
    private st_BindingKey _MoveLeftKeyBinding;
    private st_BindingKey _MoveRightKeyBinding;     

    // 서버로부터 받은 단축키 키 연동
    public void BindingKeyInit(short BindingKeysMax, st_BindingKey[] BindingKeys)
    {
        _BindingKeys = new st_BindingKey[BindingKeysMax];
        _BindingKeys = BindingKeys;         

        foreach (st_BindingKey BindingKey in _BindingKeys)
        {
            switch (BindingKey.UserQuickSlot)
            {                
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_UP:
                    _MoveUpKeyBinding = BindingKey;                    
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_DOWN:
                    _MoveDownKeyBinding = BindingKey;                                        
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_LEFT:
                    _MoveLeftKeyBinding = BindingKey;
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_RIGHT:
                    _MoveRightKeyBinding = BindingKey;                                     
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_INVENTORY:
                    _UIBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_SKILL_BOX:
                    _UIBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_EQUIPMENT_BOX:
                    _UIBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_ONE:
                    _QuickSlotBarBindingKeys.Add(BindingKey);                    
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_TWO:
                    _QuickSlotBarBindingKeys.Add(BindingKey);                    
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_THREE:
                    _QuickSlotBarBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_FOUR:
                    _QuickSlotBarBindingKeys.Add(BindingKey);                    
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_ONE_FIVE:
                    _QuickSlotBarBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_ONE:
                    _QuickSlotBarBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_TWO:
                    _QuickSlotBarBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_THREE:
                    _QuickSlotBarBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_FOUR:
                    _QuickSlotBarBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_TWO_FIVE:
                    _QuickSlotBarBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_FIND_AROUND_OBJECT:
                    _UIBindingKeys.Add(BindingKey);
                    break;
                case en_UserQuickSlot.USER_KEY_QUICK_SLOT_INTERACTION:
                    _UIBindingKeys.Add(BindingKey);
                    break;
            }
        }

        st_BindingKey EscapeBindingKey = new st_BindingKey();
        EscapeBindingKey.UserQuickSlot = en_UserQuickSlot.USER_KEY_ESCAPE;
        EscapeBindingKey.KeyCode = en_KeyCode.KEY_CODE_ESCAPE;

        st_BindingKey EnterChattingBindingKey = new st_BindingKey();
        EnterChattingBindingKey.UserQuickSlot = en_UserQuickSlot.USER_KEY_ENTER_CHATTING;
        EnterChattingBindingKey.KeyCode = en_KeyCode.KEY_CODE_ENTER;

        _UIBindingKeys.Add(EscapeBindingKey);
        _UIBindingKeys.Add(EnterChattingBindingKey);
    }

    public bool KeyBoardGetKeyActions(en_KeyCode eKeyCode)
    {
        bool IsKeyBoardKeyAction = false;

        switch (eKeyCode)
        {
            case en_KeyCode.KEY_CODE_ONE:
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_ONE:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_ONE:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_TWO:
                if (Input.GetKey(KeyCode.Alpha2))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_TWO:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_TWO:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha2))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_THREE:
                if (Input.GetKey(KeyCode.Alpha3))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_THREE:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha3))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_THREE:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha3))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_FOUR:
                if (Input.GetKey(KeyCode.Alpha4))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_FOUR:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha4))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_FOUR:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha4))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_FIVE:
                if (Input.GetKey(KeyCode.Alpha5))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_FIVE:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha5))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_FIVE:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha5))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_SIX:
                if (Input.GetKey(KeyCode.Alpha6))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_SIX:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha6))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_SIX:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha6))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_SEVEN:
                if (Input.GetKey(KeyCode.Alpha7))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_SEVEN:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha7))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_SEVEN:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha7))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_EIGHT:
                if (Input.GetKey(KeyCode.Alpha8))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_EIGHT:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha8))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_EIGHT:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha8))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_NINE:
                if (Input.GetKey(KeyCode.Alpha9))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_NINE:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha9))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_NINE:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha9))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ZERO:
                if (Input.GetKey(KeyCode.Alpha0))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_ZERO:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha0))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_ZERO:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha0))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_Q:
                if (Input.GetKey(KeyCode.Q))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_Q:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_Q:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_W:
                if (Input.GetKey(KeyCode.W))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_W:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_W:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.W))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;            
            case en_KeyCode.KEY_CODE_E:
                if (Input.GetKey(KeyCode.E))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_E:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_E:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.E))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;            
            case en_KeyCode.KEY_CODE_R:
                if (Input.GetKey(KeyCode.R))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_R:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_R:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.R))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_T:
                if (Input.GetKey(KeyCode.T))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_T:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_T:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.T))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_Y:
                if (Input.GetKey(KeyCode.Y))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_Y:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_Y:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Y))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_U:
                if (Input.GetKey(KeyCode.U))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_U:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.U))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_U:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.U))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_I:
                if (Input.GetKey(KeyCode.I))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_I:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.I))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_I:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.I))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_O:
                if (Input.GetKey(KeyCode.O))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_O:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.O))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_O:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.O))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_P:
                if (Input.GetKey(KeyCode.P))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_P:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.P))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_P:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.P))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_A:
                if (Input.GetKey(KeyCode.A))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_A:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_A:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.A))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_S:
                if (Input.GetKey(KeyCode.S))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_S:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_S:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.S))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_D:
                if (Input.GetKey(KeyCode.D))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_D:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_D:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.D))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_F:
                if (Input.GetKey(KeyCode.F))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_F:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_F:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_G:
                if (Input.GetKey(KeyCode.G))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_G:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_G:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.G))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_H:
                if (Input.GetKey(KeyCode.H))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_H:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.H))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_H:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.H))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_J:
                if (Input.GetKey(KeyCode.J))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_J:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.J))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_J:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.J))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_K:
                if (Input.GetKey(KeyCode.K))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_K:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.K))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_K:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.K))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_L:
                if (Input.GetKey(KeyCode.L))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_L:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_L:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.L))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_Z:
                if (Input.GetKey(KeyCode.Z))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_Z:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_Z:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Z))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_X:
                if (Input.GetKey(KeyCode.X))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_X:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.X))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_X:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.X))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_C:
                if (Input.GetKey(KeyCode.C))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_C:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_C:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.C))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_V:
                if (Input.GetKey(KeyCode.V))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_V:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_V:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.V))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_B:
                if (Input.GetKey(KeyCode.B))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_B:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.B))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_B:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.B))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_N:
                if (Input.GetKey(KeyCode.N))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_N:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_N:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.N))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_M:
                if (Input.GetKey(KeyCode.M))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_M:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_M:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.M))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CAPSLOCK:
                if (Input.GetKey(KeyCode.CapsLock))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_CTRL_CAPSLOCK:
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.CapsLock))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ALT_CAPSLOCK:
                if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.CapsLock))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;               
        }

        return IsKeyBoardKeyAction;
    }
        
    bool KeyBoardGetKeyDownActions(en_KeyCode eKeyCode)
    {
        bool IsKeyBoardKeyAction = false;

        switch (eKeyCode)
        {
            case en_KeyCode.KEY_CODE_I:
                if(Input.GetKeyDown(KeyCode.I))
                {
                    IsKeyBoardKeyAction = true;
                }                
                break;
            case en_KeyCode.KEY_CODE_K:
                if (Input.GetKeyDown(KeyCode.K))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_E:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_F:
                if(Input.GetKeyDown(KeyCode.F))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;                
            case en_KeyCode.KEY_CODE_ESCAPE:
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_TAB:
                if(Input.GetKeyDown(KeyCode.Tab))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
            case en_KeyCode.KEY_CODE_ENTER:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    IsKeyBoardKeyAction = true;
                }
                break;
        }

        return IsKeyBoardKeyAction;
    }
       
    public void MoveQuickSlotKeyUpdate()
    {
        Vector2 MoveDirecion = new Vector2();

        MoveDirecion.x = 0;
        MoveDirecion.y = 0;
        
        if (_MoveUpKeyBinding.UserQuickSlot == en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_UP)
        {            
            if (KeyBoardGetKeyActions(_MoveUpKeyBinding.KeyCode))
            {
                MoveDirecion.y += 1.0f;
                if (MoveDirecion.y > 0)
                {
                    MoveDirecion.y = 1.0f;
                }
            }
        }

        if(_MoveDownKeyBinding.UserQuickSlot == en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_DOWN)
        {
            if (KeyBoardGetKeyActions(_MoveDownKeyBinding.KeyCode))
            {
                MoveDirecion.y += -1.0f;
                if (MoveDirecion.y < 0)
                {
                    MoveDirecion.y = -1.0f;
                }
            }                
        }

        if(_MoveLeftKeyBinding.UserQuickSlot == en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_LEFT)
        {
            if (KeyBoardGetKeyActions(_MoveLeftKeyBinding.KeyCode))
            {
                MoveDirecion.x += -1.0f;
                if (MoveDirecion.x < 0)
                {
                    MoveDirecion.x = -1.0f;
                }
            }           
        }

        if(_MoveRightKeyBinding.UserQuickSlot == en_UserQuickSlot.USER_KEY_QUICK_SLOT_MOVE_RIGHT)
        {
            if (KeyBoardGetKeyActions(_MoveRightKeyBinding.KeyCode))
            {
                MoveDirecion.x += 1.0f;
                if (MoveDirecion.x > 0)
                {
                    MoveDirecion.x = 1.0f;
                }
            }                
        }       

        GameObject FindObject = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
        if(FindObject != null)
        {
            GameObjectInput GOInput = FindObject.GetComponent<GameObjectInput>();   
            if(GOInput != null)
            {
                GOInput.OnMovementKeyPressed?.Invoke(MoveDirecion);
            }
        }        
    }   

    public void QuickSlotBarActions()
    {
        foreach(st_BindingKey BindingKey in _QuickSlotBarBindingKeys)
        {
            if(KeyBoardGetKeyActions(BindingKey.KeyCode))
            {
                QuickSlotBarUpdate(BindingKey.UserQuickSlot);
            }
        }      
    }

    public void QuickSlotBarUpdate(en_UserQuickSlot UserQuiSlot)
    {
        // 실행하고자 하는 퀵슬롯 정보를 가져옴
        st_QuickSlotBarSlotInfo QuickSlotBarSlotInfo = Managers.QuickSlotBar.FindExekey(UserQuiSlot);
        // 퀵슬롯에 스킬정보와 아이템 정보가 설정 되어 있지 않을 경우 나감
        if (QuickSlotBarSlotInfo.QuickBarSkillInfo == null && QuickSlotBarSlotInfo.QuickBarItemInfo == null)
        {
            return;
        }

        PlayerObject Player = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId).GetComponent<PlayerObject>();

        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if (GameSceneUI != null)
        {
            if (GameSceneUI._EquipmentBoxUI != null)
            {
                UI_EquipmentItem EquipmentItem = GameSceneUI._EquipmentBoxUI.GetEquipment(en_EquipmentParts.EQUIPMENT_PARTS_RIGHT_HAND);
                if (EquipmentItem != null)
                {
                    st_ItemInfo EquipmentItemInfo = EquipmentItem.GetEquipmentItemInfo();
                    if (EquipmentItemInfo != null)
                    {
                        if (EquipmentItemInfo.ItemSmallCategory != en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE)
                        {
                            if (QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillKind == en_SkillKinds.SKILL_KIND_SPELL_SKILL)
                            {                                
                                if (Player._GameObjectInfo.ObjectPositionInfo.MoveDireciton.magnitude == 0)
                                {                                    
                                    CMessage ReqQuickSlotPacket = Packet.MakePacket.ReqMakeSkillProcessPacket(
                                        QuickSlotBarSlotInfo.QuickSlotBarIndex,
                                        QuickSlotBarSlotInfo.QuickSlotBarSlotIndex,
                                        QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillCharacteristic,
                                        QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType,
                                        Player._GameObjectInfo.ObjectPositionInfo.LookAtDireciton.x,
                                        Player._GameObjectInfo.ObjectPositionInfo.LookAtDireciton.y);
                                    Managers.NetworkManager.GameServerSend(ReqQuickSlotPacket);
                                }
                            }
                            else
                            {                                
                                CMessage ReqQuickSlotPacket = Packet.MakePacket.ReqMakeSkillProcessPacket(
                                        QuickSlotBarSlotInfo.QuickSlotBarIndex,
                                        QuickSlotBarSlotInfo.QuickSlotBarSlotIndex,
                                        QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillCharacteristic,
                                        QuickSlotBarSlotInfo.QuickBarSkillInfo.SkillType,
                                        Player._GameObjectInfo.ObjectPositionInfo.LookAtDireciton.x,
                                        Player._GameObjectInfo.ObjectPositionInfo.LookAtDireciton.y);
                                Managers.NetworkManager.GameServerSend(ReqQuickSlotPacket);
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("맨손 공격 시도");
                    }
                }
            }
        }

        if (QuickSlotBarSlotInfo.QuickBarItemInfo != null && QuickSlotBarSlotInfo.QuickBarItemInfo.ItemSmallCategory != en_SmallItemCategory.ITEM_SMALL_CATEGORY_NONE)
        {
            CMessage ReqMakeItemUsePacket = Packet.MakePacket.ReqMakeItemUsePacket(Managers.NetworkManager._AccountId, Managers.NetworkManager._PlayerDBId,
                    QuickSlotBarSlotInfo.QuickBarItemInfo.ItemSmallCategory,
                    QuickSlotBarSlotInfo.QuickBarItemInfo.ItemTileGridPositionX,
                    QuickSlotBarSlotInfo.QuickBarItemInfo.ItemTileGridPositionY);
            Managers.NetworkManager.GameServerSend(ReqMakeItemUsePacket);
        }
    }

    public void UIActions()
    {
        foreach (st_BindingKey BindingKey in _UIBindingKeys)
        {
            if (KeyBoardGetKeyDownActions(BindingKey.KeyCode))
            {
                UIUpdate(BindingKey.UserQuickSlot);
            }
        }
    }

    public void UIUpdate(en_UserQuickSlot UserQuickSlot)
    {
        UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
        if(GameSceneUI != null)
        {
            GameObject FindObject = Managers.Object.FindById(Managers.NetworkManager._PlayerDBId);
            if (FindObject != null)
            {
                PlayerObject Player = FindObject.GetComponent<PlayerObject>();
                if (Player != null)
                {
                    GameObjectInput GOInput = FindObject.GetComponent<GameObjectInput>();

                    if (Player._IsChattingFocus == false)
                    {
                        switch (UserQuickSlot)
                        {
                            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_INVENTORY:
                                GOInput.OnInventroyUIOpen?.Invoke();
                                break;
                            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_SKILL_BOX:
                                GOInput.OnSkillUIOpen?.Invoke();
                                break;
                            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_UI_EQUIPMENT_BOX:
                                GOInput.OnEquipmentUIOpen?.Invoke();
                                break;
                            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_INTERACTION:
                                break;
                            case en_UserQuickSlot.USER_KEY_QUICK_SLOT_FIND_AROUND_OBJECT:
                                Player.FindAroundObject();                                                              
                                break;
                            case en_UserQuickSlot.USER_KEY_ESCAPE:
                                if (GameSceneUI.IsGameSceneUIStackEmpty() == true)
                                {
                                    GameSceneUI.AddGameSceneUIStack(GameSceneUI._OptionUI);
                                }
                                else
                                {
                                    // GameSceneUIStack이 비어 있지 않을 경우
                                    // 하나씩 뽑아서 UI를 닫아줌
                                    UI_Base GameSceneUIStackUI = GameSceneUI.FindGameSceneUIStack();
                                    if (GameSceneUIStackUI != null)
                                    {
                                        UI_Furnace FurnaceUI = GameSceneUIStackUI as UI_Furnace;
                                        if (FurnaceUI != null)
                                        {
                                            CMessage ReqCraftingTableNonSelectPacket = Packet.MakePacket.ReqMakeCraftingTableNonSelectPacket(Managers.NetworkManager._AccountId,
                                                Managers.NetworkManager._PlayerDBId,
                                                FurnaceUI._FurnaceController._GameObjectInfo.ObjectId,
                                                FurnaceUI._FurnaceController._GameObjectInfo.ObjectType);
                                            Managers.NetworkManager.GameServerSend(ReqCraftingTableNonSelectPacket);
                                        }

                                        UI_TargetHUD TargetHUDUI = GameSceneUIStackUI as UI_TargetHUD;
                                        if (TargetHUDUI != null)
                                        {
                                            if(GameSceneUI._InteractionUI.gameObject.activeSelf == true)
                                            {
                                                GameSceneUI._InteractionUI.ShowCloseUI(false);
                                            }                                            
                                        }

                                        GameSceneUI.DeleteGameSceneUIStack(GameSceneUIStackUI);
                                    }
                                }
                                break;                            
                        }
                    }

                    if (Player._IsChattingFocus == false)
                    {
                        switch (UserQuickSlot)
                        {
                            case en_UserQuickSlot.USER_KEY_ENTER_CHATTING:
                                Player._GameObjectInfo.ObjectPositionInfo.State = en_CreatureState.STOP;

                                CMessage ReqMoveStopPacket = Packet.MakePacket.ReqMakeMoveStopPacket(
                                    Player._GameObjectInfo.ObjectPositionInfo.Position.x,
                                    Player._GameObjectInfo.ObjectPositionInfo.Position.y,
                                    Player._GameObjectInfo.ObjectPositionInfo.State);

                                Managers.NetworkManager.GameServerSend(ReqMoveStopPacket);

                                GameObjectMovement gameObjectMovement = Player.GetComponent<GameObjectMovement>();
                                if (gameObjectMovement != null)
                                {
                                    Player._IsChattingFocus = true;

                                    gameObjectMovement.OnMovementStop?.Invoke();

                                    InputField ChattingInputField = GameSceneUI._ChattingBoxGroup.GetChattingInputField();
                                    if (ChattingInputField != null)
                                    {
                                        ChattingInputField.text = "";
                                        ChattingInputField.gameObject.SetActive(true);
                                        ChattingInputField.ActivateInputField();
                                    }
                                }
                                break;
                        }
                    }
                    else if (Player._IsChattingFocus == true)
                    {
                        switch (UserQuickSlot)
                        {
                            case en_UserQuickSlot.USER_KEY_ENTER_CHATTING:
                                InputField ChattingInputField = GameSceneUI._ChattingBoxGroup.GetChattingInputField();
                                if (ChattingInputField != null)
                                {
                                    Player._IsChattingFocus = false;

                                    if (ChattingInputField.text.Length > 0)
                                    {
                                        CMessage ReqChattingMessage = Packet.MakePacket.ReqMakeChattingPacket(ChattingInputField.text);
                                        Managers.NetworkManager.GameServerSend(ReqChattingMessage);
                                    }

                                    ChattingInputField.text = "";
                                    ChattingInputField.gameObject.SetActive(false);
                                    ChattingInputField.DeactivateInputField();
                                }
                                break;
                        }
                    }
                }
            }
        }        
    }    
}
