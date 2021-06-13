using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
 [RequireComponent(typeof(ConfigurableJoint))]
public class PlayerControler : MonoBehaviour
{
   
 [SerializeField]
    private float speed = 5f;
    [SerializeField]
private float lookSensitivity = 3f;

    [SerializeField]
    private float thrysterFource = 1000f;
    [Header("Spring settings:")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20;
    [SerializeField]
    private float jointMaxForce = 40f;
    
    private PlayerMotor motor;
    private ConfigurableJoint joint;

    void Start(){
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    void Update(){
        //Calculate movoment velocity as a 3d vector

        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        
        // Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotarion as a 3d Vector: (turning around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity; 


/////////////////////

        //Apply rotation
        motor.Rotate(_rotation);

         //Calculate Camera rotarion as a 3d Vector: (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity; 

        //Apply camera rotation
        motor.RotateCamera(_cameraRotation);

        Vector3 _thrusterForce = Vector3.zero;
        //Apply the truster force
        if(Input.GetButton("Jump")){
            _thrusterForce = Vector3.up * thrysterFource;
            SetJointSettings(0f);
        }else{
            SetJointSettings(jointSpring);
        }

        motor.ApplyThruster(_thrusterForce);
    }

private void SetJointSettings(float _jointSpring){
    joint.yDrive = new JointDrive{ 
        mode = jointMode, 
        positionSpring = jointSpring, 
        maximumForce = jointMaxForce
    };
}

}
