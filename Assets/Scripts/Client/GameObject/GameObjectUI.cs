using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectUI : MonoBehaviour
{
    private PlayerObject _OwnerPlayer;

    UI_GameScene _GameSceneUI;

    // Start is called before the first frame update
    void Start()
    {
        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;
    }

    public void SetOwner(PlayerObject Player)
    {
        _OwnerPlayer = Player;
    }

    public void InventoryUIOnOff()
    {
        if (_GameSceneUI != null && _OwnerPlayer != null)
        {
            if(_OwnerPlayer._InventoryUI != null && _OwnerPlayer._InventoryUI.gameObject.activeSelf == false)
            {
                _GameSceneUI.AddGameSceneUIStack(_OwnerPlayer._InventoryUI);
            }
            else
            {
                _GameSceneUI.DeleteGameSceneUIStack(_OwnerPlayer._InventoryUI);
                _GameSceneUI.EmptyItemExplanation();
            }
        }
    }

    public void EquipmentUIOnOff()
    {
        if (_GameSceneUI != null && _OwnerPlayer != null)
        {
            if (_OwnerPlayer._EquipmentBoxUI != null && _OwnerPlayer._EquipmentBoxUI.gameObject.activeSelf == false)
            {
                _GameSceneUI.AddGameSceneUIStack(_OwnerPlayer._EquipmentBoxUI);
                _GameSceneUI._EquipmentBoxUI.EquipmentBoxRefreshUI();
            }
            else
            {
                _GameSceneUI.DeleteGameSceneUIStack(_OwnerPlayer._EquipmentBoxUI);                
            }
        }
    }

    public void SkillUIOnOff()
    {
        if (_GameSceneUI != null && _OwnerPlayer != null)
        {
            if (_OwnerPlayer._SkillBoxUI != null && _OwnerPlayer._SkillBoxUI.gameObject.activeSelf == false)
            {
                _GameSceneUI.AddGameSceneUIStack(_OwnerPlayer._SkillBoxUI);
                _GameSceneUI._SkillBoxUI.RefreshSkillBoxUI();
            }
            else
            {
                _GameSceneUI.DeleteGameSceneUIStack(_OwnerPlayer._SkillBoxUI);
                _GameSceneUI.EmptySkillExplanation();
            }
        }
    }
}
