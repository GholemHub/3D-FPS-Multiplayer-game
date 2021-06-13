using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{    

        [SerializeField]
        private Camera cam;
        private Vector3 velocity = Vector3.zero;
        private Vector3 rotation = Vector3.zero; 
        private Vector3 cameraRotation = Vector3.zero;
        private Vector3 thrusterForce = Vector3.zero;
        


    private Rigidbody rigidBody;

    void Start(){
        rigidBody = GetComponent<Rigidbody>();
    }

//gets a movoment vector
    public void Move(Vector3 _velocity){
        velocity = _velocity;
    }

//gets a roatation vector
     public void Rotate(Vector3 _rotation){
        rotation = _rotation;
    }

    //gets a CameraRoatation vector
     public void RotateCamera(Vector3 _cameraRotation){
        cameraRotation = _cameraRotation;
    }

//get vector for our thrusters
    public void ApplyThruster(Vector3 _thrusterFirce){
        thrusterForce = _thrusterFirce;
    }

     

//run every physics iteration
    void FixedUpdate(){
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable
    void PerformMovement(){
        if(velocity != Vector3.zero){
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        }

        if(thrusterForce != Vector3.zero){
            rigidBody.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

//Perform Rotation
    void PerformRotation(){
        rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(rotation));
        if(cam != null){
            cam.transform.Rotate(-cameraRotation);
        }
    }


 }
