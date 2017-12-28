using UnityEngine;

public class Ball : MonoBehaviour {

	[SerializeField]
    private AudioClip ballHit;

    void OnCollisionEnter(Collision coll) {
        if(coll.relativeVelocity.magnitude >= 5) {
            AudioSource.PlayClipAtPoint(ballHit, transform.position);
        }
    }
}
