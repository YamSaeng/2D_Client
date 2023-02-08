using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    GameObject _CameraManagerRoot;
    private const float CAMERA_MOVE_SPEED = 6.0f;
    private Camera MainCamera;
    PlayerObject MyPlayer;

    CinemachineVirtualCamera _CinemachineCamera;

    void Start()
    {
        
    }

    void Update()
    {
        CameraMove();
    }    

    public void Init()
    {
        if(_CameraManagerRoot == null)
        {
            _CameraManagerRoot = new GameObject { name = "@CameraManager" };
            _CameraManagerRoot.AddComponent<CinemachineVirtualCamera>();
            _CinemachineCamera = _CameraManagerRoot.GetComponent<CinemachineVirtualCamera>();
            _CinemachineCamera.AddCinemachineComponent<CinemachineFramingTransposer>();

            UnityEngine.Object.DontDestroyOnLoad(_CameraManagerRoot);
        }        
    }    

    public void CameraSetTarget(GameObject TargetObject)
    {
        _CinemachineCamera.Follow = TargetObject.transform;   
        _CinemachineCamera.LookAt = TargetObject.transform;

        _CinemachineCamera.m_Lens.NearClipPlane = 0;        
        
    }

    public void CameraMove()
    {
        //if (GameObject.FindGameObjectWithTag("MyPlayer") != null)
        //{
        //    Vector3 CameraFollowPosition = GameObject.FindGameObjectWithTag("MyPlayer").transform.position;
        //    CameraFollowPosition.z = transform.position.z;

        //    // 따라가야할 방향 구하기
        //    Vector3 CameraMoveDir = (CameraFollowPosition - transform.position).normalized;
        //    // 따라가야할 거리 구하기
        //    float Distance = Vector3.Distance(CameraFollowPosition, transform.position);

        //    // 거리가 0.1f 보다 크다면
        //    if (Distance > 0.1f)
        //    {
        //        // 현재 카메라 위치에서 따라가야할 방향 * 이동해야할 거리 * 카메라 움직임 속도 * deletaTime 만큼 구해서
        //        // 새로운 카메라 위치로 삼는다.
        //        transform.position = transform.position + (CameraMoveDir * Distance * CAMERA_MOVE_SPEED * Time.deltaTime);
        //    }
        //}
    }

    public static void CameraShake(float ShakePower)
    {
        float RandomY = Random.Range(-1.5f, 1.5f);

        Vector3 RandomMovement = new Vector3(0, RandomY).normalized * ShakePower;
        Camera.main.transform.position = Camera.main.transform.position + RandomMovement;
    }
}
