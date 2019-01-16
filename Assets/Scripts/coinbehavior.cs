using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinbehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//this.GetComponent<Rigidbody> ().AddTorque (Vector3.up, ForceMode.Force);
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate (Vector3.up);
		if (!righthand.hold_coin) {
			//this.GetComponent<Rigidbody> ().isKinematic = true;
			//transform.Rotate (Vector3.up);
		}
		/*
		if(righthand.hold_coin  == false && this.GetComponent<Rigidbody> ().isKinematic){
			this.GetComponent<Rigidbody> ().isKinematic = false;
		}*/
	}


}
