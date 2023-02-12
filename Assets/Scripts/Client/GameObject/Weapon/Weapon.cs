using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 무기
public class Weapon : MonoBehaviour
{
    // 투사체를 발사하는 무기에 사용할 위치값
    [SerializeField]
    protected GameObject _Muzzle = null;

    // 탄약
    [SerializeField]
    protected int _Ammo = 0;

    // 탄약 최대값
    [SerializeField]
    protected int _MaxAmmo = 0;

    // 탄약이 최대값인지 확인
    public bool AmmoFull { get => _Ammo >= _MaxAmmo; }

    // 공격 가능 여부
    protected bool _IsAttacking = false;

    // 재공격 여부
    [SerializeField]
    protected bool _IsReload = false;

    [SerializeField]
    protected en_WeaponType _WeaponType;

    // 공격 이벤트
    [field: SerializeField]
    public UnityEvent OnAttack { get; set; }

    // 탄약이 없는 상태에서 공격 이벤트
    [field: SerializeField]
    public UnityEvent OnAttackNoAmmo { get; set; }

    // 탄약 개수가 변경되는 이벤트
    [field: SerializeField]
    public UnityEvent<int> OnAmmoChange { get; set; }

    private void Start()
    {
        _Ammo = _MaxAmmo;
        _WeaponType = en_WeaponType.WEAPON_TYPE_MELEE;
    }

    private void Update()
    {
        UseWeapon();
    }

    public void TryAttacking()
    {
        _IsAttacking = true;
    }

    public void StopAttacking()
    {
        _IsAttacking = false;
    }

    private void UseWeapon()
    {
        if(_IsAttacking == true && _IsReload == false)
        {
            switch(_WeaponType)
            {
                case en_WeaponType.WEAPON_TYPE_MELEE:
                    OnAttack?.Invoke();                    
                    break;
                case en_WeaponType.WEAPON_TYPE_RANGE:
                    if(_Ammo > 0)
                    {
                        _Ammo--;
                        OnAttack?.Invoke();
                    }
                    else
                    {
                        _IsAttacking = false;
                        OnAttackNoAmmo.Invoke();
                    }
                    break;
            }

            DefaultAttackCooltimeStart();
        }
    }

    private void DefaultAttackCooltimeStart()
    {
        StartCoroutine(DelayNextDefaultAttackCoroutine());
    }

    protected IEnumerator DelayNextDefaultAttackCoroutine()
    {
        _IsReload = true;
        yield return new WaitForSeconds(1.0f);
        _IsReload = false;
    }
}
