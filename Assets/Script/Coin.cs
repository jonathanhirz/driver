using UnityEngine;

public class Coin : MonoBehaviour {

    public AudioClip pickupCoin;
	
	void Update () {
        transform.Rotate(0, 10 * Time.deltaTime, 0);
	}

    void OnTriggerEnter(Collider coll) {
        if(coll.gameObject.tag == "car") {
            AudioSource.PlayClipAtPoint(pickupCoin, transform.position);
            Destroy(gameObject);
        }
    }
}
