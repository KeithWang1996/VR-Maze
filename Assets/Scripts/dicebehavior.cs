using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dicebehavior : MonoBehaviour {
	int counter;
	public static int result;
	//bool getresult;
	public static int delay_counter = 250;
	bool unlocked;
    public AudioClip drop;
    AudioSource source;
    // Use this for initialization
    void Start () {
		counter = 3;
		this.GetComponent<Rigidbody> ().AddTorque (Vector3.up, ForceMode.Impulse);
		unlocked = false;
        source = GetComponent<AudioSource>();
        source.volume = 0.1f;
		//getresult = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (righthand.hold) {
			this.GetComponent<Rigidbody> ().useGravity = true;
		}
		//print ("up is " + Vector3.Normalize(transform.up));
		//print ("right is " + Vector3.Normalize(transform.right));
		if (counter <= 0) {
			if (Vector3.Distance(this.transform.up,Vector3.up) < 0.01) {
				result = 6;
			}
			else if (Vector3.Distance(-this.transform.up,Vector3.up) < 0.01) {
				result = 1;
			}
			else if (Vector3.Distance(this.transform.right,Vector3.up) < 0.01) {
				result = 2;
			}
			else if (Vector3.Distance(-this.transform.right,Vector3.up) < 0.01) {
				result = 5;
			}
			else if (Vector3.Distance(this.transform.forward,Vector3.up) < 0.01) {
				result = 4;
			}
			else {
				result = 3;
			}
			if (delay_counter > 0) {
				delay_counter--;
			}
		}

		if (delay_counter == 0) {
			//print (result);
			if (!unlocked) {
				unlocked = true;
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		if (counter > 0 && righthand.hold) {
            source.PlayOneShot(drop);
			Vector3 dir = Vector3.Normalize(collision.gameObject.transform.up);
			this.GetComponent<Rigidbody> ().AddForce (dir/3.0f, ForceMode.Impulse);
			counter--;
		}
	}
}
