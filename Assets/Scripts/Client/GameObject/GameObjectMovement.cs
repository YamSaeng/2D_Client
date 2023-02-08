using ServerCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class GameObjectMovement : MonoBehaviour
{
    [field: SerializeField]
    public PlayerObject _OwnerController { get; set; }

    protected Rigidbody2D _Rigidbody2D;

    protected Vector2 _MovementDirection;

    protected float _Speed;

    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }

    private void Awake()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();    
    }

    public void SetOwner(PlayerObject baseController)
    {
        _OwnerController = baseController; 
    }

    // GameObjectInput�� OnMoveKeyPresse�� ����
    // ĳ���Ͱ� �����ϰ��� �ϴ� ���� ���� �޴´�.
    public void MoveGameObject(Vector2 MovementDirection)
    {
        if(_OwnerController != null)
        {
            _Speed = _OwnerController._GameObjectInfo.ObjectStatInfo.Speed;

            // ���Ⱚ�� �޶����� ���������
            if (_OwnerController._GameObjectInfo.ObjectPositionInfo.State != en_CreatureState.MOVING 
                && MovementDirection.magnitude > 0)
            {                
                _OwnerController._GameObjectInfo.ObjectPositionInfo.State = en_CreatureState.MOVING;

                _MovementDirection = MovementDirection;

                CMessage ReqMovePacket = Packet.MakePacket.ReqMakeMovePacket(
                    _MovementDirection.x,
                    _MovementDirection.y,                    
                    _OwnerController.transform.position.x,
                    _OwnerController.transform.position.y,
                    _OwnerController._GameObjectInfo.ObjectPositionInfo.State);
                Managers.NetworkManager.GameServerSend(ReqMovePacket);
            }  
            else if(_OwnerController._GameObjectInfo.ObjectPositionInfo.State == en_CreatureState.MOVING
                && _MovementDirection != MovementDirection && MovementDirection.magnitude > 0)
            {
                _MovementDirection = MovementDirection;

                CMessage ReqMovePacket = Packet.MakePacket.ReqMakeMovePacket(
                    _MovementDirection.x,
                    _MovementDirection.y,
                    _OwnerController.transform.position.x,
                    _OwnerController.transform.position.y,
                    _OwnerController._GameObjectInfo.ObjectPositionInfo.State);
                Managers.NetworkManager.GameServerSend(ReqMovePacket);
            }
            else if(_OwnerController._GameObjectInfo.ObjectPositionInfo.State == en_CreatureState.MOVING
                && MovementDirection.magnitude == 0)
            {
                _MovementDirection = MovementDirection;

                _OwnerController._GameObjectInfo.ObjectPositionInfo.State = en_CreatureState.IDLE;

                CMessage ReqMoveStopPacket = Packet.MakePacket.ReqMakeMoveStopPacket( _OwnerController.transform.position.x, _OwnerController.transform.position.y, _OwnerController._GameObjectInfo.ObjectPositionInfo.State );
                Managers.NetworkManager.GameServerSend(ReqMoveStopPacket);
            }            
        }
    }    

    private void FixedUpdate()
    {        
        OnVelocityChange?.Invoke(_MovementDirection.normalized.magnitude);

        if (_Rigidbody2D != null)
        {     
            _OwnerController.transform.position += (Vector3)(_MovementDirection.normalized * _Speed * Time.deltaTime);

            //_Rigidbody2D.velocity = _Speed * _MovementDirection.normalized * Time.deltaTime;
        }        
    }    
}
