using UnityEngine;

public class Driver : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody rb;
    // private Vector3 lookVector = new Vector3(0.001f, 0, 0);


    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {

        var movementH = Input.GetAxis("Horizontal");
        var movementV = Input.GetAxis("Vertical");

        var movement = new Vector3(movementH, 0, movementV);
        rb.AddForce(movement * speed);

        // Vector3 lookVector = transform.position + (transform.forward * 5);

        // transform.LookAt(lookVector);

    }
}
