using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour{
    [SerializeField]
    private float forwardSpeed;
    [SerializeField]
    private float turnSpeed;
    private Rigidbody rb;
    private Vector3 directionalDistance;
    [SerializeField]
    private GameObject leftFrontWheel;
    [SerializeField]
    private GameObject rightFrontWheel;
    [SerializeField]
    private GameObject leftBackWheel;
    [SerializeField]
    private GameObject rightBackWheel;

    private SpawnPointManager _spawnPointManager;

    private GameObject[] cars;

    private float rotationAlongXForAllWheels = 0;
    public void Awake(){
      _spawnPointManager = FindObjectOfType<SpawnPointManager>();
   }
    private void Start(){
        rb = this.GetComponent<Rigidbody>();
    }

    /*private void Update(){
        float forwardInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        MoveCar(forwardInput,turnInput);
    }*/

    public void MoveCar(float forwardAmount,float turnAmount){
        if(rb != null){
            rb.velocity = this.transform.TransformDirection(new Vector3(0f,0f,forwardSpeed)*forwardAmount);
        }
        if(Input.GetKey(KeyCode.D)){ // turnAmount > 0.5
            RotateWheels(Mathf.Clamp(turnSpeed*0.3f,5f,40f));
            //Debug.Log(Mathf.Clamp(turnSpeed*0.3f,5f,40f));
        }
        else if(Input.GetKey(KeyCode.A)){ // turnAmount < -0.5
            RotateWheels(Mathf.Clamp(-turnSpeed*0.3f,-40f,-5f));
            //Debug.Log(Mathf.Clamp(-turnSpeed*0.3f,-40f,-5f));
        }
        else{
            RotateWheels(0f);
        }
        if(forwardAmount > 0.3 || forwardAmount < -0.3){
            this.transform.Rotate(0f,turnSpeed*turnAmount*Time.deltaTime*Mathf.Sign(forwardAmount),0f);
        }
    }

    private void RotateWheels(float yRotation){
        Vector3 rotationOfCar = this.transform.localEulerAngles;
        rotationAlongXForAllWheels += (rb.velocity.magnitude*forwardSpeed*Time.deltaTime*5f) % 360;
        leftFrontWheel.transform.rotation = Quaternion.Euler(rotationAlongXForAllWheels,rotationOfCar.y + yRotation,rotationOfCar.z);
        rightFrontWheel.transform.rotation = Quaternion.Euler(rotationAlongXForAllWheels,rotationOfCar.y + yRotation,rotationOfCar.z);
        leftBackWheel.transform.rotation = Quaternion.Euler(rotationAlongXForAllWheels,rotationOfCar.y,rotationOfCar.z);
        rightBackWheel.transform.rotation = Quaternion.Euler(rotationAlongXForAllWheels,rotationOfCar.y,rotationOfCar.z);
    }

    public void Respawn(){
      Vector3 pos = _spawnPointManager.SelectRandomSpawnpoint();
      transform.rotation = Quaternion.Euler(0f,0f,0f);
      transform.localPosition = pos;
   }

}
