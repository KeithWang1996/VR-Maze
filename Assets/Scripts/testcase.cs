using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcase : MonoBehaviour {
	public GameObject coin;
	Vector3 pos;
	Quaternion rot;
	// Use this for initialization
	void Start () {
		pos = coin.transform.position;
		rot = coin.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
