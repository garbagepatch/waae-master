using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent (typeof(ConfigurableJoint))]
[RequireComponent (typeof (Motor))]
public class PlayerController : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float sensitivity = 3f;
  
    [SerializeField]
    private float thrusterForce = 1000f;
   /* [Header("Spring Settings:")]
    [SerializeField]
    private JointDriveMode jointMode;
    [SerializeField]
    private float jointSpring=20f;
    [SerializeField]
    private float jointMaxForce = 70f;


    ConfigurableJoint joint;*/
    private Motor motor;
    private void Start()
    {
        motor = GetComponent<Motor>();
      //  joint = GetComponent<ConfigurableJoint>();
     //   SetJointSettings(jointSpring);
        anim = GetComponentInChildren<Animator>();

    }
    private void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        
        Vector3 Horizontal = transform.right * xMove;
        Vector3 Vertical = transform.forward * zMove;
        anim.SetFloat("TurningSpeed", xMove);
        Vector3 velocity = (Horizontal + Vertical).normalized * speed;
        anim.SetFloat("Speed", zMove);
        

        motor.Move(velocity);

        float yRot = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRot , 0)*sensitivity;
        anim.SetFloat("Angle", rotation.magnitude);
        motor.Rotate(rotation);


        float xRot = Input.GetAxisRaw("Mouse Y");
        float camrotation = xRot* sensitivity;
        motor.RotateCamera(camrotation);
       
        
        Vector3 localThruster = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            localThruster = Vector3.up * thrusterForce;
            //       SetJointSettings(0f);

            //    } else { SetJointSettings(jointSpring); }
        }   motor.ApplyThruster(localThruster);

    }
  //   private void SetJointSettings(float _jointSpring)
 //   {
   //     joint.yDrive = new JointDrive { mode = jointMode, positionSpring = _jointSpring, maximumForce = jointMaxForce };
 //   }
}
