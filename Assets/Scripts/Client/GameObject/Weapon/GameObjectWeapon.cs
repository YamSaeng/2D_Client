using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ����
public class GameObjectWeapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponRenderer _WeaponRenderer;
    [SerializeField]
    protected Weapon _Weapon;

    protected float _DesiredAngle;

    private void Awake()
    {
        _WeaponRenderer = GetComponentInChildren<WeaponRenderer>();
        _Weapon = GetComponentInChildren<Weapon>();
    }

    // �Է����� ���� Vector2�� ���ϰ� ������ ��ġ�� ��������
    public void AimWeapon(Vector2 Pointer)
    {        
        // Pinter�� ���ϴ� ���� ����        
        Vector2 Direction = (Pointer - (Vector2)transform.position).normalized;
        // ������ ���� ���� ��ǥ ���� Pointer�� �ٶ󺸰� ����
        transform.right = Direction;        

        _DesiredAngle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                
        if (Direction.x < 0)
        {
            transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);            
        }
        else if (Direction.x > 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);            
        }

        //Vector3 AimDirection = (Vector3)Pointer - transform.position;
        //_DesiredAngle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(_DesiredAngle, Vector3.forward);        
    }

    public void S2C_AimWeapon(Vector2 Direction)
    {
        // ������ ���� ���� ��ǥ ���� Pointer�� �ٶ󺸰� ����
        transform.right = Direction;

        _DesiredAngle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        if (Direction.x < 0)
        {
            transform.localScale = new Vector3(1.0f, -1.0f, 1.0f);
        }
        else if (Direction.x > 0)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(_DesiredAngle, Vector3.forward);
    }

    protected void AdjustWeaponRendering()
    {
        if (_WeaponRenderer != null)
        {
            // ���콺 ��ġ�� ���� ��ġ ���� ������ 90�� ���� ũ�ų� -90�� �̸� �϶� y���� ������
            //_WeaponRenderer.WeaponFlipSpriteP(_DesiredAngle > 90 || _DesiredAngle < -90);
            // ���콺 ��ġ�� ���� ��ġ ���� ������ 0 ~ 180�� ���� �϶� �Ӹ� �ڷ� ����
            _WeaponRenderer.RenderBehindHead(_DesiredAngle < 180 && _DesiredAngle > 0);
        }
    }

    // GameObjectInput OnDefaultAttackPressed �̺�Ʈ�� ����
    // ������ �ִ� ����� ���� �õ�
    public void Attack()
    {
        if(_Weapon != null)
        {            
            _Weapon.TryAttacking();
        }
    }

    // ������ �ִ� ���� ���� ����
    public void StopAttack()
    {
        if(_Weapon != null)
        {
            _Weapon.StopAttacking();
        }
    }

    public void Reload()
    {
        if(_Weapon != null)
        {
            _Weapon.Reload();
        }
    }

    public void Init()
    {
        if(_Weapon != null)
        {
            _Weapon.StopAttacking();
            _Weapon.Reload();
        }
    }
}
