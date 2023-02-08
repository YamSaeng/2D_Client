using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Name : MonoBehaviour
{
    [SerializeField]
    TextMeshPro _ObjectName;

    public GameObject LeftChoiceSprite;
    public GameObject RightChoiceSprite;

    public void Init(string ObjectName)
    {
        _ObjectName.text = ObjectName;
        LeftChoiceSprite.SetActive(false);
        RightChoiceSprite.SetActive(false);
    }

    public void Init(string ObjectName, float PositionX, float PositionY)
    {
        _ObjectName.text = ObjectName;
        LeftChoiceSprite.SetActive(false);
        RightChoiceSprite.SetActive(false);
                
        gameObject.transform.position = new Vector3(PositionX, PositionY, 0);
    }

    public void ActiveChoiceUI(bool Active)
    {        
        LeftChoiceSprite.SetActive(Active);
        RightChoiceSprite.SetActive(Active);
    }
}
