using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ����
public class Weapon : MonoBehaviour
{
    // ����ü�� �߻��ϴ� ���⿡ ����� ��ġ��
    [SerializeField]
    protected GameObject _Muzzle = null;

    // ź��
    [SerializeField]
    protected int _Ammo = 0;

    // ź�� �ִ밪
    [SerializeField]
    protected int _MaxAmmo = 0;

    // ź���� �ִ밪���� Ȯ��
    public bool AmmoFull { get => _Ammo >= _MaxAmmo; }

    // ���� ���� ����
    protected bool _IsAttacking = false;

    // ����� ����
    [SerializeField]
    protected bool _IsReload = false;

    [SerializeField]
    protected en_WeaponType _WeaponType;

    // ���� �̺�Ʈ
    [field: SerializeField]
    public UnityEvent OnAttack { get; set; }

    // ź���� ���� ���¿��� ���� �̺�Ʈ
    [field: SerializeField]
    public UnityEvent OnAttackNoAmmo { get; set; }

    // ź�� ������ ����Ǵ� �̺�Ʈ
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
