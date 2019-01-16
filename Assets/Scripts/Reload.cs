using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour {

    static public bool reloaded;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        reloaded = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
