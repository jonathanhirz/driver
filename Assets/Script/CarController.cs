using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float twistSpeed;
    [SerializeField]
    private LayerMask flipLayerMask;
    [SerializeField]
    private Transform TwistThrusterL;
    [SerializeField]
    private Transform TwistThrusterR;

    private Rigidbody rb;
    private float rotationVelocity;
    private float fixingRotation;
    private float desiredAngle;
    private float desiredAngleX;
    private float desiredAngleY;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float movementH;
    private float movementV;
    private float twist;
    private float twistLeftTrigger;
    private float twistRightTrigger;
    private Vector3 movement;

    void Awake() {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void FixedUpdate() {
        // get our inputs
        movementH = Input.GetAxis("Horizontal");
        movementV = Input.GetAxis("Vertical");

        // check if we are touching the ground
        if(Physics.Raycast(transform.position, -transform.up, 2f)) {
            // we are on the ground. enable the accelerator and increase drag
            rb.drag = 1;

            // move player relatetive to camera
            var theCamera = Camera.main;
            var camForward = theCamera.transform.forward;
            var camRight = theCamera.transform.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            // Vector3 movement = new Vector3(movementH, transform.forward.y / 10, movementV) * speed;
            movement = (camForward * movementV + camRight * movementH) * speed;
            movement = new Vector3(movement.x, transform.forward.y / 10, movement.z);
            rb.AddForce(movement);

        } else {
            // we aren't on the ground, and don't want to just halt in mid-air. reduce drag
            rb.drag = 0;
        }

        // input for twisting
        // first for keys Q+E, Twist input axis
        twist = Input.GetAxis("Twist");
        if(twist < 0) {
            rb.AddForceAtPosition(-transform.up * -twist * twistSpeed, TwistThrusterL.position);
        }
        if(twist > 0) {
            rb.AddForceAtPosition(-transform.up * twist * twistSpeed, TwistThrusterR.position);
        }

        // then for Dualshock4, triggers control two twist axises
        twistLeftTrigger = Input.GetAxis("TwistLeftTrigger");
        twistLeftTrigger = (twistLeftTrigger + 1) / 2;
        if(twistLeftTrigger == 0.5f) twistLeftTrigger = 0.0f;
        twistRightTrigger = Input.GetAxis("TwistRightTrigger");
        twistRightTrigger = (twistRightTrigger + 1) / 2;
        if(twistRightTrigger == 0.5f) twistRightTrigger = 0.0f;

        rb.AddForceAtPosition(-transform.up * twistLeftTrigger * twistSpeed, TwistThrusterL.position);
        rb.AddForceAtPosition(-transform.up * twistRightTrigger * twistSpeed, TwistThrusterR.position);
    }

    void LateUpdate() {
        // "fake" rotate the car when you are turning
        if(movement.x != 0 || movement.z != 0) {
            // use the movement vector3 calculated before to figure out car rotation
            desiredAngleX = movement.x;
            desiredAngleY = -movement.z;
        }
        // figure out angle between movement, input, and camera forward
        desiredAngle = Mathf.Atan2(desiredAngleY, desiredAngleX) * Mathf.Rad2Deg + 90;
        Vector3 newRotation = transform.eulerAngles;
        newRotation.y = Mathf.SmoothDampAngle(newRotation.y, desiredAngle, ref rotationVelocity, turnSpeed);
        transform.eulerAngles = newRotation;

        // debug reset key
        if(Input.GetKeyDown(KeyCode.R)) {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            rb.velocity = Vector3.zero;
            desiredAngle = 0;
        }
    }
}
