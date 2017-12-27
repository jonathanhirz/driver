using UnityEngine;

public class CameraSetup : MonoBehaviour {

    private Vector3 offset;

    void Awake() {
        // offset = transform.position;
        offset = new Vector3(0, 15, -22);
        transform.rotation = Quaternion.Euler(30, 0, 0);
        transform.position = transform.parent.transform.position + offset;
    }
}
