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

    protected Vector2 _MovementDirection;

    protected float _Speed;

    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }

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
        if(_OwnerObject != null)
        {
            _Speed = _OwnerObject._GameObjectInfo.ObjectStatInfo.Speed;

            // 방향값이 달라지면 보내줘야함
            if (_OwnerObject._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.MOVING 
                && MovementDirection.magnitude > 0)
            {                
                _OwnerObject._GameObjectInfo.ObjectPositionInfo.State = en_CreatureState.MOVING;

                _MovementDirection = MovementDirection;

                CMessage ReqMovePacket = Packet.MakePacket.ReqMakeMovePacket(
                    _MovementDirection.x,
                    _MovementDirection.y,                    
                    _OwnerObject.transform.position.x,
                    _OwnerObject.transform.position.y,
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.State);
                Managers.NetworkManager.GameServerSend(ReqMovePacket);
            }  
            else if(_OwnerObject._GameObjectInfo.ObjectPositionInfo.State == en_CreatureState.MOVING
                && _MovementDirection != MovementDirection && MovementDirection.magnitude > 0)
            {
                _MovementDirection = MovementDirection;

                CMessage ReqMovePacket = Packet.MakePacket.ReqMakeMovePacket(
                    _MovementDirection.x,
                    _MovementDirection.y,
                    _OwnerObject.transform.position.x,
                    _OwnerObject.transform.position.y,
                    _OwnerObject._GameObjectInfo.ObjectPositionInfo.State);
                Managers.NetworkManager.GameServerSend(ReqMovePacket);
            }
            else if(_OwnerObject._GameObjectInfo.ObjectPositionInfo.State == en_CreatureState.MOVING
                && MovementDirection.magnitude == 0)
            {
                _MovementDirection = MovementDirection;

                _OwnerObject._GameObjectInfo.ObjectPositionInfo.State = en_CreatureState.IDLE;

                CMessage ReqMoveStopPacket = Packet.MakePacket.ReqMakeMoveStopPacket( _OwnerObject.transform.position.x, _OwnerObject.transform.position.y, _OwnerObject._GameObjectInfo.ObjectPositionInfo.State );
                Managers.NetworkManager.GameServerSend(ReqMoveStopPacket);
            }            
        }
    }    

    public void MoveOtherGameObject(Vector2 MovementDirection)
    {
        _MovementDirection = MovementDirection;
    }

    private void FixedUpdate()
    {        
        OnVelocityChange?.Invoke(_MovementDirection.normalized.magnitude);

        if (_Rigidbody2D != null)
        {    
            //_Rigidbody2D.velocity = _Speed * _MovementDirection.normalized * Time.deltaTime;
        }        
    }

    private void Update()
    {
        _OwnerObject.transform.position += (Vector3)(_MovementDirection.normalized * _OwnerObject._GameObjectInfo.ObjectStatInfo.Speed * Time.deltaTime);
    }
}
