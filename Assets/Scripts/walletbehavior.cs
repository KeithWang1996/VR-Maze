using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class walletbehavior : MonoBehaviour {
	public static int money;
    public AudioClip ding;
	bool iscollide;
	float trans;
	public Text text;
    AudioSource source;
    // Use this for initialization
    void Start () {
		money = 0;///
		iscollide = false;
		trans = 0.0f;
        source = GetComponent<AudioSource>();
        source.volume = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
		if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) == 0)
			iscollide = false;
        if (money < 10 && money >= 0)
        {
            text.text = "0" + money;
        }
        else if (money >= 10)
        {
            text.text = "" + money;
        }
        else if(money < 0){
            text.text = "" + money;
            lefthand.game_over = true;
        }
	}
	/*
	void OnCollisionEnter (Collision col) {
		GameObject coin = col.gameObject;
		if (coin.tag == "coin") {
			Destroy (coin);
			money++;
			print (money);
			if (money < 10) {
				text.text = "0" + money;
			} else {
				text.text = "" + money;
			}
		}
	}
    */

	void OnTriggerEnter (Collider col) {
		if (iscollide)
			return;
		iscollide = true;
		GameObject coin = col.gameObject;
		if (coin != null && coin.tag == "coin") {
			coin.transform.SetParent (null);
			Destroy (coin);
            source.PlayOneShot(ding);
			//iscollide = false;
			money++;
			//print (money);
		}
	}
		
}
