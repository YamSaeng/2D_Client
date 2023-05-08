using ServerCore;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CropController : CreatureObject
{
    SpriteRenderer _CropImage;
    TextMeshPro _CropStepText;
    UI_CropBar _CropBar;

    public override void Init()
    {
        base.Init();

        _HPBarUI = transform.Find("UI_HPBar").gameObject.GetComponent<UI_HPBar>();
        _NameUI = transform.Find("UI_Name").gameObject.GetComponent<UI_Name>();
        _CropImage = transform.Find("CropImage").GetComponent<SpriteRenderer>();

        _CropStepText = transform.Find("CropStepText").gameObject.GetComponent<TextMeshPro>();

        _CropBar = transform.Find("UI_CropBar").gameObject.GetComponent<UI_CropBar>();

        _SpellBar = null;
        _GatheringBar = null;

        _NameUI.Init(_GameObjectInfo.ObjectName);

        UpdateHPBar();

        _HPBarUI.gameObject.SetActive(false);

        CropImageChange(_GameObjectInfo.ObjectCropStep);

        _CropStepText.text = "성장 중";

        _CropBar.SetCropBar(0.1f);

        _NameUI.gameObject.SetActive(false);
        _CropBar.gameObject.SetActive(false);
        _CropStepText.gameObject.SetActive(false);
    }

    public void CropImageChange(byte CropGrowthStep)
    {
        CropGrowthStep -= 1;

        switch (_GameObjectInfo.ObjectType)
        {
            case en_GameObjectType.OBJECT_CROP_POTATO:
                _CropImage.sprite = Managers.Sprite._PotatoGrowthSprite[CropGrowthStep];
                break;
            case en_GameObjectType.OBJECT_CROP_CORN:
                _CropImage.sprite = Managers.Sprite._CornGrowthSprite[CropGrowthStep];
                break;
        }

        _GameObjectInfo.ObjectCropStep = CropGrowthStep;

        if(_GameObjectInfo.ObjectCropStep + 1 == _GameObjectInfo.ObjectCropMaxStep)
        {
            _CropStepText.text = "수확 가능";
        }
    }

    public void SetCropBar(float Ratio)
    {
        _CropBar.SetCropBar(Ratio);
    }

    float Timer;

    public void Update()
    {
        Timer += Time.deltaTime;

        if (Timer > 1.0f)
        {
            Timer = 0;

            if (_GameObjectInfo.ObjectCropStep != _GameObjectInfo.ObjectCropMaxStep)
            {
                CMessage ReqPlantGrowthPacket = Packet.MakePacket.ReqMakePlantGrowthPacket(Managers.NetworkManager._AccountId,
                Managers.NetworkManager._PlayerDBId,
                _GameObjectInfo.ObjectId,
                _GameObjectInfo.ObjectType);
                Managers.NetworkManager.GameServerSend(ReqPlantGrowthPacket);
            }
        }
    }

    private void OnMouseEnter()
    {
        _NameUI.gameObject.SetActive(true);
        _CropBar.gameObject.SetActive(true);
        _CropStepText.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        _NameUI.gameObject.SetActive(false);
        _CropBar.gameObject.SetActive(false);
        _CropStepText.gameObject.SetActive(false);
    }
}
