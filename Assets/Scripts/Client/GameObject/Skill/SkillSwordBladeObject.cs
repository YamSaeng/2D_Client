using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwordBladeObject : CreatureObject
{
    public override void Init()
    {
        _GameSceneUI = Managers.UI._SceneUI as UI_GameScene;

        _LineRendererController = GetComponent<LineRendererController>();
        if (_LineRendererController != null)
        {
            _LineRendererController.SetUpOwnPlayer(this);
        }

        transform.position = new Vector3(_GameObjectInfo.ObjectPositionInfo.Position.x, _GameObjectInfo.ObjectPositionInfo.Position.y, 0);
    }

    private void GameObjectRendererUpdate()
    {
        GameObjectRenderer SkillSwordBladeRenderer = transform.Find("GameObjectRenderer").GetComponent<GameObjectRenderer>();
        if (SkillSwordBladeRenderer != null)
        {
            SkillSwordBladeRenderer.transform.right = new Vector2(_GameObjectInfo.ObjectPositionInfo.LookAtDireciton.x, _GameObjectInfo.ObjectPositionInfo.LookAtDireciton.y);
        }
    }

    private void Update()
    {
        GameObjectRendererUpdate();

        transform.position += (Vector3)(_GameObjectInfo.ObjectPositionInfo.LookAtDireciton.normalized * _GameObjectInfo.ObjectStatInfo.Speed * Time.deltaTime);        
    }
}
