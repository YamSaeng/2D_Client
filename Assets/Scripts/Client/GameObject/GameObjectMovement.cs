using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class GameObjectMovement : MonoBehaviour
{
    [field: SerializeField]
    public CreatureObject _OwnerObject { get; set; }

    protected Rigidbody2D _Rigidbody2D;    

    protected float _Speed;

    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }

    // GameObject의 움직임을 멈춰주는 이벤트
    // GameObjectMovement - MovementStop()과 연결
    [field: SerializeField]
    public UnityEvent OnMovementStop { get; set; }

    private void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();    
    }

    public void SetOwner(CreatureObject OwnerObject)
    {
        _OwnerObject = OwnerObject; 
    }

    // GameObjectInput의 OnMoveKeyPresse에 연결
    // 캐릭터가 진행하고자 하는 방향 값을 받는다.
    public void MoveGameObject(Vector2 MovementDirection)
    {
        if(_OwnerObject != null && _OwnerObject._IsChattingFocus == false)
        {
            _Speed = _OwnerObject._GameObjectInfo.ObjectStatInfo.Speed;

            // 방향값이 달라지면 보내줘야함
            if (_OwnerObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.MOVING 
                && MovementDirection.magnitude > 0)
            {
                UI_SkillCastingBar SkillCastingBar = _OwnerObject.transform.Find("UI_SkillCastingBar").GetComponent<UI_SkillCastingBar>();
                if (SkillCastingBar != null)
                {
                    if (SkillCastingBar.isActiveAndEnabled == true)
                    {
                        CMessage SkillCastingCancelPacket = Packet.MakePacket.ReqMakeSkillCastingCancelPacket();
                        Managers.NetworkManager.GameServerSend(SkillCastingCancelPacket);
                    }
                }

                _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton = MovementDirection;

                CMessage ReqMovePacket = Packet.MakePacket.ReqMakeMovePacket(
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton.x,
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton.y,                    
                    _OwnerObject.transform.position.x,
                    _OwnerObject.transform.position.y,
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.State);
                Managers.NetworkManager.GameServerSend(ReqMovePacket);
            }  
            else if(_OwnerObject._GameObjectInfo.ObjectPositionInfo.State == en_CreatureState.MOVING
                && _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton != MovementDirection && MovementDirection.magnitude > 0)
            {
                _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton = MovementDirection;

                CMessage ReqMovePacket = Packet.MakePacket.ReqMakeMovePacket(
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton.x,
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton.y,
                    _OwnerObject.transform.position.x,
                    _OwnerObject.transform.position.y,
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.State);
                Managers.NetworkManager.GameServerSend(ReqMovePacket);
            }
            else if(_OwnerObject._GameObjectInfo.ObjectPositionInfo.State == en_CreatureState.MOVING
                && MovementDirection.magnitude == 0)
            {
                _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton = Vector2.zero;

                _OwnerObject._GameObjectInfo.ObjectPositionInfo.State = en_CreatureState.IDLE;

                CMessage ReqMoveStopPacket = Packet.MakePacket.ReqMakeMoveStopPacket(
                    _OwnerObject.transform.position.x,
                    _OwnerObject.transform.position.y,
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.State);
                Managers.NetworkManager.GameServerSend(ReqMoveStopPacket);
            }            
        }
    }    

    public void MoveOtherGameObject(Vector2 MovementDirection)
    {
        if(_OwnerObject != null)
        {
            _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton = MovementDirection;
        }        
    }

    private void FixedUpdate()
    {                 
        if(_OwnerObject != null)
        {
            OnVelocityChange?.Invoke(_OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton.normalized.magnitude);

            if (_Rigidbody2D != null)
            {
                //_Rigidbody2D.velocity = _Speed * _MovementDirection.normalized * Time.deltaTime;
            }
        }        
    }

    private void Update()
    {        
        if(_OwnerObject != null)
        {            
            _OwnerObject.transform.position += (Vector3)(_OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton.normalized * _OwnerObject._GameObjectInfo.ObjectStatInfo.Speed * Time.deltaTime);
        }                
    }

    public void MovementStop()
    {
        _OwnerObject._GameObjectInfo.ObjectPositionInfo.MoveDireciton = Vector2.zero;
    }
}
