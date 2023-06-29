using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UI_QuickSlotKeyItem : UI_Base
{
    public st_BindingKey _BindingKey;

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

        QuickSlotKeyItemUIUpdate();        
    }

    public void QuickSlotKeyItemUIUpdate()
    {
        GetTextMeshPro((int)en_QuickSlotKeyItemText.QuickSlotKeyText).text = Managers.String._UserQuickSlotString[_BindingKey.UserQuickSlot];
        GetTMPInputField((int)en_QuickSlotKeyItemInputField.UserQuickSlotKeyInputField).text = Managers.String._KeyCodeString[_BindingKey.KeyCode];
    }

    private bool QuickSlotKetInput(en_KeyCode KeyCode)
    {
        return Managers.Key.KeyBoardGetKeyActions(KeyCode);
    }

    private void Update()
    {        
        if (_IsQuickSlotKeyInput == true)
        {
            UI_GameScene GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
            if(GameSceneUI != null)
            {
                en_KeyCode NewkeyCode = en_KeyCode.KEY_CODE_NONE;

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_W))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_W;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_W))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_W;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_W))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_W;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_S))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_S;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_S))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_S;                    

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_S))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_S;                    

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_A))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_A;                    

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_A))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_A;                    

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_A))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_A;                    

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_D))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_D;                    

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_D))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_D;                    

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_D))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_D;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_I))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_I;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_I))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_I;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_I))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_I;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_E))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_E;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_E))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_E;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_E))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_E;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_K))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_K;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_K))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_K;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_K))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_K;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_ONE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_ONE;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_ONE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_ONE;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ONE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ONE;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_TWO))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_TWO;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_TWO))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_TWO;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_TWO))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_TWO;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_THREE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_THREE;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_THREE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_THREE;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_THREE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_THREE;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_FOUR))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_FOUR;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_FOUR))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_FOUR;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_FOUR))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_FOUR;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_FIVE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_FIVE;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_FIVE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_FIVE;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_FIVE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_FIVE;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_SIX))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_SIX;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_SIX))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_SIX;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_SIX))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_SIX;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_SEVEN))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_SEVEN;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_SEVEN))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_SEVEN;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_SEVEN))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_SEVEN;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_EIGHT))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_EIGHT;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_EIGHT))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_EIGHT;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_EIGHT))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_EIGHT;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_NINE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_NINE;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_NINE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_NINE;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_NINE))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_NINE;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_ZERO))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_ZERO;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_ZERO))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_ZERO;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ZERO))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ZERO;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_Q))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_Q;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_Q))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_Q;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_Q))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_Q;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_R))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_R;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_R))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_R;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_R))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_R;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_T))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_T;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_T))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_T;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_T))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_T;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_Y))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_Y;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_Y))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_Y;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_Y))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_Y;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_U))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_U;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_U))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_U;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_U))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_U;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_O))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_O;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_O))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_O;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_O))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_O;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_P))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_P;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_P))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_P;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_P))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_P;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_F))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_F;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_F))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_F;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_F))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_F;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_G))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_G;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_G))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_G;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_G))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_G;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_H))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_H;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_H))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_H;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_H))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_H;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_J))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_J;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_J))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_J;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_J))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_J;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_L))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_L;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_L))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_L;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_L))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_L;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_Z))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_Z;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_Z))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_Z;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_Z))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_Z;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_X))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_X;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_X))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_X;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_X))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_X;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_C))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_C;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_C))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_C;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_C))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_C;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_V))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_V;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_V))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_V;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_V))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_V;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_B))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_B;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_B))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_B;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_B))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_B;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_N))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_N;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_N))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_N;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_N))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_N;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_M))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_M;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_M))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_M;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_M))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_M;

                    _IsQuickSlotKeyInput = false;
                }

                if (QuickSlotKetInput(en_KeyCode.KEY_CODE_ALT_CAPSLOCK))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_ALT_CAPSLOCK;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CTRL_CAPSLOCK))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CTRL_CAPSLOCK;

                    _IsQuickSlotKeyInput = false;
                }
                else if (QuickSlotKetInput(en_KeyCode.KEY_CODE_CAPSLOCK))
                {
                    NewkeyCode = en_KeyCode.KEY_CODE_CAPSLOCK;

                    _IsQuickSlotKeyInput = false;
                }

                if(_IsQuickSlotKeyInput == false)
                {
                    // 입력한 KeyCode에 연결되어 있었던 단축키 초기화
                    GameSceneUI._QuickSlotKeyUI.QuickSlotKeyFindFix(NewkeyCode);

                    _BindingKey.KeyCode = NewkeyCode;
                }

                QuickSlotKeyItemUIUpdate();
            }                        
        }        
    }
}
