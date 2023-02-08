using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CropBar : MonoBehaviour
{
    [SerializeField]
    Transform _CropBar = null;

    public void SetCropBar(float Ratio)
    {
        float CropGrowthRatio = Mathf.Clamp(Ratio, 0, 1);
        _CropBar.localScale = new Vector3(CropGrowthRatio, 1, 1);
    }
}
