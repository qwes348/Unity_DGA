using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //스피드 조정 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;
    [SerializeField]
    private float crouchSpeed;

    [SerializeField]
    private float jumpForce;

    //상태변수
    private bool isWalk = false;
    private bool isRun = false;
    private bool isGround = true;
    private bool isCrouch = false;

    // 움직임 체크 변수
    private Vector3 latsPos;

    //앉았을 때 얼마나 앉을지 결정하는 변수
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private CapsuleCollider CapsuleCollider;

    //민감도
    [SerializeField]
    private float lookSensitivity;
    
    //카메라 한계
    [SerializeField]
    private float cameraRotation_limit;
    private float currentCameraRotationX = 0;

    //필요한 컴포넌트
    [SerializeField]
    private Camera theCamera;
    [SerializeField]
    private Camera weapon_Camera;

    private Rigidbody myRigid;
    
    private GunCtrl theGunController;

    private Crosshair theCrosshair;
    private StatusController theStatusController;


    // Start is called before the first frame update
    void Start()
    {
        CapsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        theGunController = FindObjectOfType<GunCtrl>();
        theCrosshair = FindObjectOfType<Crosshair>();
        theStatusController = FindObjectOfType<StatusController>();
        
        // 초기화
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;

    }

    // Update is called once per frame
    void Update()
    {
        TryCrouch();
        IsGround();
        TryJump();
        TryRun();
        Move();    
        MoveCheck();
        if(!Inventory.inventoryActivated)
        {
            CameraRotation();    
            CharacterRotation();
        }
       
    }

    // 앉기시도
    private void TryCrouch()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    // 앉기
    private void Crouch()
    {
        isCrouch = !isCrouch;
        theCrosshair.CrouchingAnimation(isCrouch);

        if(isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    //부드러운 앉기
    IEnumerator CrouchCoroutine()
    {
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;
        
        while(_posY != applyCrouchPosY)
        {
            count ++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            if(count>15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0f);
        
    }

    // 지면확인
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, CapsuleCollider.bounds.extents.y + 0.2f);
        theCrosshair.JumpingAnimation(!isGround);
    }

    //점프시도
    private void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGround && theStatusController.GetCurrentSp() > 0)
        {
            jump();
        }
    }

    // 점프
    private void jump()
    {
        // 앉은상태에서 점프시 앉은상태 해제
        if(isCrouch)
            Crouch();
        theStatusController.DecreaseStamina(100);
        myRigid.velocity = transform.up * jumpForce;
    }

    // 달리기 시도
    private void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift) && theStatusController.GetCurrentSp() > 0)
        {
            Running();
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || theStatusController.GetCurrentSp() <= 0)
        {
            RunningCancel();
        }
    }

    // 달리기 실행
    private void Running()
    {
        // 앉은상태에서 달릴시 앉기 해제
        if(isCrouch)
            Crouch();

        theGunController.CancelFineSight();

        isRun = true;
        theCrosshair.RunningAnimation(isRun);
        theStatusController.DecreaseStamina(10);
        applySpeed = runSpeed;
    }

    // 달리기취소
    private void RunningCancel()
    {
        isRun = false;
        theCrosshair.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }
    
    // 움직임 실행
    private void Move()
    {
        float _moveDirx = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirx;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void MoveCheck()
    {
        if(!isRun && !isCrouch && isGround)
        {
            if(Vector3.Distance(latsPos, transform.position) >= 0.01f)
                isWalk = true;
            else
                isWalk = false;

            theCrosshair.WalkingAnimation(isWalk);
            latsPos = transform.position;
        }
    }

    private void CameraRotation()
    {
        //카메라 상하 회전
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotation_limit, cameraRotation_limit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        weapon_Camera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }


    private void CharacterRotation()
    {
        //캐릭터 좌우 회전
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}
