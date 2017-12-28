using UnityEngine;

public class CarHead : MonoBehaviour {

    [SerializeField]
    private Transform car;
    [SerializeField]
    private Vector2 pitchMinMax = new Vector2(-30, 50);
    [SerializeField]
    private float rotationSmoothTime = 0.2f;
    [SerializeField]
    private float rotateSpeed = 1.5f;
    [SerializeField]
    private bool inverted = false;

    private float yaw;
    private float pitch;
    private Vector3 currentRotation;
    private Vector3 rotationSmoothVelocity;
    
	void Awake () {
        transform.position = car.position;   
	}
	
	void LateUpdate () {
        transform.position = car.position;

        // inverted up/down look axis setting
        int invertedInt = inverted ? 1 : -1;
        yaw -= Input.GetAxis("RightHorizontal") * rotateSpeed;
        pitch += Input.GetAxis("RightVertical") * rotateSpeed * invertedInt;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector2(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;
	}
}
