using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ¹«±â ·»´õ¸µ
[RequireComponent(typeof(SpriteRenderer))]
public class WeaponRenderer : MonoBehaviour
{
    [SerializeField]
    protected int _PlayerSoringOrder = 0;

    private SpriteRenderer _WeaponSprite;

    private void Awake()
    {
        _WeaponSprite = GetComponent<SpriteRenderer>();
    }

    public void WeaponFlipSpriteP(bool IsFlip)
    {      
        _WeaponSprite.flipY = IsFlip;
    }

    public void RenderBehindHead(bool IsBehindHead)
    {
        if(IsBehindHead == true)
        {
            _WeaponSprite.sortingOrder = _PlayerSoringOrder - 1;
        }
        else
        {
            _WeaponSprite.sortingOrder = _PlayerSoringOrder + 1;
        }
    }
}
