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

    public void ActiveChoiceUI(bool Active)
    {        
        LeftChoiceSprite.SetActive(Active);
        RightChoiceSprite.SetActive(Active);
    }
}
