using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerControler : MonoBehaviour
{
 [SerializeField]
    private float speed = 5f;

    [SerializeField]
private float lookSensitivity = 3f;

    private PlayerMotor motor;

    void Start(){
        motor = GetComponent<PlayerMotor>();


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

    }
}
