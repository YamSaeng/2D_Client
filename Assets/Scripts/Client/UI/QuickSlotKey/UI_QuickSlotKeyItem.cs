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

    private void Update()
    {        
        if (_IsQuickSlotKeyInput == true)
        {
            if(Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.W))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + W";

                _IsQuickSlotKeyInput = false;
            }
            else if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + W";

                _IsQuickSlotKeyInput = false;
            }
            else if(Input.GetKey(KeyCode.W))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "W";                

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.S))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + S";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + S";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "S";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.A))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + A";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + A";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "A";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.D))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + D";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + D";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "D";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.I))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + I";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.I))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + I";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.I))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "I";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.E))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + E";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + E";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "E";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.K))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + K";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.K))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + K";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.K))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "K";

                _IsQuickSlotKeyInput = false;
            }                       

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 1";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 1";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "1";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 2";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 2";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "2";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha3))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 3";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha3))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 3";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "3";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha4))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 4";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha4))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 4";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "4";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha5))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 5";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha5))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 5";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "5";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha6))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 6";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha6))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 6";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha6))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "6";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha7))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 7";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha7))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 7";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha7))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "7";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha8))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 8";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha8))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 8";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha8))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "8";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha9))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 9";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha9))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 9";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha9))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "9";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Alpha0))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + 0";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha0))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + 0";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Alpha0))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "0";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Q))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + Q";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + Q";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Q";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.R))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + R";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + R";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.R))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "R";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.T))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + T";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + T";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.T))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "T";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Y))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + Y";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Y))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + Y";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Y";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.U))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + U";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.U))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + U";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.U))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "U";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.O))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + O";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.O))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + O";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.O))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "O";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.P))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + P";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.P))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + P";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.P))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "P";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + F";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + F";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.F))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "F";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.G))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + G";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.G))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + G";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.G))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "G";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.H))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + H";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.H))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + H";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.H))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "H";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.J))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + J";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.J))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + J";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.J))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "J";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.L))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + L";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + L";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.L))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "L";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Z))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + Z";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + Z";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Z";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.X))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + X";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.X))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + X";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.X))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "X";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.C))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + C";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + C";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "C";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.V))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + V";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + V";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.V))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "V";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.B))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + B";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.B))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + B";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.B))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "B";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.N))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + N";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + N";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.N))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "N";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.M))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + M";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.M))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + M";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.M))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "M";

                _IsQuickSlotKeyInput = false;
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.CapsLock))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Alt + CapsLock";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.CapsLock))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "Ctrl + CapsLock";

                _IsQuickSlotKeyInput = false;
            }
            else if (Input.GetKey(KeyCode.CapsLock))
            {
                GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = "CapsLock";

                _IsQuickSlotKeyInput = false;
            }
        }        
    }
}
