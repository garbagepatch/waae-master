using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
[RequireComponent(typeof (Rigidbody))]
public class Motor : MonoBehaviour
{
    Animator anim;
    private Transform _fpvPos = null;
    private Transform _originalCam = null;
    private Vector3 _velocity = Vector3.zero;
    private Rigidbody rb;
    private Vector3 _rotation = Vector3.zero;
    private float _camrotation = 0f;
    private Vector3 thruster = Vector3.zero;
    [SerializeField]
    private float cameraLimit = 85f;
    private float currentCameraRotation = 0f;
    [SerializeField]
    private Camera cam;
    AimIK aim;
    FullBodyBipedIK ik;
    float cameraPosition = 0f;
    [SerializeField]
    Transform RaycastStart;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        anim = GetComponent<Animator>();
        
        aim.Disable();
    }
    public void Swap(Transform fpvPos)
    {
        _fpvPos = fpvPos;

    }
    public void SwapBack(Transform originalCam)
    {
        _originalCam = originalCam;
    }
    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
        if (Input.GetButton("Sprint"))
        {
            anim.SetFloat("Speed", 3f);
        } else if (!Input.GetButton("Sprint"))
        {
          
            return;
        }
    }
      public void Rotate(Vector3 rotation)
    {
        _rotation = rotation;
    }
    public void RotateCamera(float camrotation)
    {
        _camrotation = camrotation;
    }
    public void ApplyThruster(Vector3 localThruster)
    {
        thruster = localThruster;
    }
    private void Update()
    {
    }
    private void LateUpdate()
    {
       
    }
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        MoveCamera();
    }
    void PerformMovement()
    {
        if(_velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + _velocity * Time.deltaTime);
            if(thruster != Vector3.zero)
            {
                rb.AddForce(thruster * Time.deltaTime, ForceMode.Acceleration); 
            }
        }
    }
    void MoveCamera()
    {
        if (cam.transform.position == _originalCam.position)
        {
            cam.transform.TransformPoint(_fpvPos.position); }

        else if (cam.transform.position == _fpvPos.position)
        {
            cam.transform.TransformPoint(_originalCam.position);
        }

    }
void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(_rotation));
        if(cam != null)
        {

           
            currentCameraRotation -= _camrotation;
            currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraLimit, cameraLimit);
            cam.transform.localEulerAngles = new Vector3(currentCameraRotation, 0, 0);
          
        }

    }
}
