using UnityEngine;

public class ResetScript : MonoBehaviour {

	void OnTriggerEnter(Collider coll) {
        if(coll.gameObject.tag == "car") {
            coll.gameObject.transform.position = new Vector3(0, 5, 0);
        }
    }
}
