using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UI_QuickSlotKeyItem : UI_Base
{
    st_BindingKey _BindingKey;

    enum en_QuickSlotKeyItemText
    {
        QuickSlotKeyText
    }

    enum en_QuickSlotKeyItemInputField
    {
        UserQuickSlotKeyInputField
    }

    private bool _IsQuickSlotKeyInput = false;

    public override void Binding()
    {
        Bind<TextMeshProUGUI>(typeof(en_QuickSlotKeyItemText));
        Bind<TMP_InputField>(typeof(en_QuickSlotKeyItemInputField));

        _BindingKey = new st_BindingKey();

        GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).onEndEdit.AddListener(OnQuickSlotKeyItemEndEdit);        
        GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).onValueChanged.AddListener(OnQuickSlotKeyItemOnValueChanged);
        GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).onSelect.AddListener(OnQuickSlotKeyItemSelect);
        GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).onDeselect.AddListener(OnQuickSlotKeyItemDeSelect);

        GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).readOnly = true;
    }

    public override void Init()
    {

    }

    public override void ShowCloseUI(bool IsShowClose)
    {

    }

    private void OnQuickSlotKeyItemOnValueChanged(string ValueChangedString)
    {   
    }

    private void OnQuickSlotKeyItemEndEdit(string QuickSlotKeyString)
    {     
    }    

    private void OnQuickSlotKeyItemSelect(string QuickSlotKeySelect)
    {
        _IsQuickSlotKeyInput = true;                
    }

    private void OnQuickSlotKeyItemDeSelect(string QuickSlotKeyDeSelect)
    {
        _IsQuickSlotKeyInput = false;                
    }

    public void SetQuickSlotKeyItemBindingKey(st_BindingKey BindingKey)
    {
        _BindingKey = BindingKey;

        GetTextMeshPro((int)en_QuickSlotKeyItemText.QuickSlotKeyText).text = Managers.String._UserQuickSlotString[_BindingKey.UserQuickSlot];                
    }

    private bool QuickSlotKetInput(en_KeyCode KeyCode)
    {
        return Managers.Key.KeyBoardGetKeyActions(KeyCode);
    }

    private void Update()
    {        
        if (_IsQuickSlotKeyInput == true)
        {
            if(QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_W))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + W";

                _IsQuickSlotKeyInput = false;
            }
            else if(QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_W))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + W";

                _IsQuickSlotKeyInput = false;
            }
            else if(QuickSlotKetInput(en_KeyCode.KEY_CODE_W))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "W";                

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_S))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + S";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_S))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + S";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_S))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "S";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_A))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + A";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_A))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + A";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_A))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "A";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_D))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + D";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_D))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + D";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_D))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "D";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_I))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + I";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_I))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + I";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_I))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "I";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_E))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + E";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_E))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + E";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_E))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "E";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_K))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + K";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_K))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + K";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_K))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "K";

                _IsQuickSlotKeyInput = false;
            }                       

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_ONE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 1";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_ONE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 1";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ONE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "1";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_TWO))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 2";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_TWO))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 2";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_TWO))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "2";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_THREE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 3";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_THREE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 3";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_THREE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "3";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_FOUR))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 4";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_FOUR))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 4";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_FOUR))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "4";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_FIVE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 5";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_FIVE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 5";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_FIVE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "5";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_SIX))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 6";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_SIX))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 6";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_SIX))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "6";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_SEVEN))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 7";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_SEVEN))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 7";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_SEVEN))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "7";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_EIGHT))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 8";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_EIGHT))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 8";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_EIGHT))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "8";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_NINE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 9";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_NINE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 9";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_NINE))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "9";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_ZERO))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 0";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_ZERO))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 0";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ZERO))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "0";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_Q))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + Q";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_Q))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + Q";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_Q))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Q";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_R))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + R";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_R))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + R";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_R))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "R";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_T))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + T";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_T))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + T";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.T))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "T";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_Y))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + Y";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_Y))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + Y";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_Y))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Y";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_U))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + U";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_U))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + U";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_U))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "U";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_O))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + O";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_O))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + O";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_O))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "O";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_P))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + P";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_P))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + P";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_P))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "P";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_F))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + F";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_F))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + F";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_F))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "F";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_G))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + G";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_G))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + G";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_G))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "G";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_H))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + H";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_H))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + H";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_H))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "H";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_J))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + J";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_J))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + J";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_J))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "J";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_L))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + L";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_L))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + L";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_L))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "L";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_Z))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + Z";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_Z))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + Z";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_Z))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Z";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_X))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + X";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_X))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + X";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_X))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "X";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_C))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + C";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_C))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + C";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_C))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "C";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_V))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + V";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_V))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + V";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_V))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "V";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_B))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + B";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_B))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + B";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_B))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "B";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_N))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + N";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_N))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + N";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_N))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "N";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_M))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + M";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_M))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + M";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_M))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "M";

                _IsQuickSlotKeyInput = false;
            }

            if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_CAPSLOCK))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + CapsLock";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_CAPSLOCK))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + CapsLock";

                _IsQuickSlotKeyInput = false;
            }
            else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CAPSLOCK))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "CapsLock";

                _IsQuickSlotKeyInput = false;
            }
        }        
    }
}
