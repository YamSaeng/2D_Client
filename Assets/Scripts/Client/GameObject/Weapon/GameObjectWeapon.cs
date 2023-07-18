using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 무기 관리
public class GameObjectWeapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponRenderer _WeaponRenderer;
    [SerializeField]
    public Weapon _Weapon;

    protected float _DesiredAngle;

    private void Awake()
    {
    
    }

    // 입력으로 받은 Vector2로 향하게 무기의 위치를 조정해줌
    public void AimWeapon(Vector2 Pointer)
    {        
        // Pinter로 향하는 벡터 구함        
        Vector2 Direction = (Pointer - (Vector2)transform.position).normalized;
        // 오른쪽 로컬 기준 좌표 값을 Pointer를 바라보게 해줌
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
        // 오른쪽 로컬 기준 좌표 값을 Pointer를 바라보게 해줌
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
            // 마우스 위치랑 무기 위치 사이 각도가 90도 보다 크거나 -90도 미만 일때 y축을 뒤집음
            //_WeaponRenderer.WeaponFlipSpriteP(_DesiredAngle > 90 || _DesiredAngle < -90);
            // 마우스 위치랑 무기 위치 사이 각도가 0 ~ 180도 사이 일때 머리 뒤로 숨김
            _WeaponRenderer.RenderBehindHead(_DesiredAngle < 180 && _DesiredAngle > 0);
        }
    }

    public void OnWeapon(Weapon NewWeapon)
    {
        _WeaponRenderer = NewWeapon.GetComponent<WeaponRenderer>();
        _Weapon = NewWeapon;        
    }

    public void OffWeapon()
    {
        _WeaponRenderer = null;
        _Weapon = null;
    }

    public void ChildWeaponDestory()
    {
        if(_Weapon != null)
        {
            Destroy(_Weapon.gameObject);

            OffWeapon();
        }
    }

    // GameObjectInput OnDefaultAttackPressed 이벤트와 연결
    // 가지고 있는 무기로 공격 시도
    public void Attack()
    {
        if(_Weapon != null)
        {            
            _Weapon.TryAttacking();
        }
    }

    // 가지고 있는 무기 공격 멈춤
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

    public Weapon GetWeapon()
    {
        return _Weapon;
    }
}
