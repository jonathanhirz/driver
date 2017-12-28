using UnityEngine;

public class Cube : MonoBehaviour {

	[SerializeField]
    private AudioClip blockHit;

    void OnCollisionEnter(Collision coll) {
        if(coll.relativeVelocity.magnitude >= 5) {
            AudioSource.PlayClipAtPoint(blockHit, transform.position);
        }
    }
}
