using UnityEngine;
using System.Collections;

//totally copied from andy moore: http://captain-andy.com/post/91110372105/hovercars-in-unity-make-this-game

public class Thruster : MonoBehaviour {

    public float minThrusterStrength;
    public float maxThrusterStrength;
    public float thrusterDistance;
    public Transform[] thrusters;
    public LayerMask floatLayerMask;
    
    private Rigidbody rb;
    private float thrusterStrength;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate() {
        RaycastHit hit;
        foreach(Transform thruster in thrusters) {
            Vector3 downwardForce;
            float distancePercentage;

            if(Physics.Raycast(thruster.position, thruster.up * -1, out hit, thrusterDistance, floatLayerMask)) {
                // the thruster is within thrusterDisance from the ground. how far away?
                distancePercentage = 1-(hit.distance/thrusterDistance);

                // calculate how much force to push
                downwardForce = transform.up * thrusterStrength * distancePercentage;
                // correct the force for the mass of the car and deltaTime
                downwardForce = downwardForce * Time.deltaTime * rb.mass;

                // apply the force where the thruster is
                rb.AddForceAtPosition(downwardForce, thruster.position);

                Debug.DrawRay(thruster.position, thruster.up * -1, Color.red);
            }
        }
    }

    void Update() {
        if(Input.GetKey(KeyCode.F) || Input.GetButton("Flatten")) {
            thrusterStrength = minThrusterStrength;
        } else {
            thrusterStrength = maxThrusterStrength;
        }
    }
}
