using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class righthand : MonoBehaviour {
	public GameObject ring;
	//public GameObject dice;
	public Text screen;
	int switch_counter;
	int rotate_counter;
	int digit1;
	int digit2;
	bool digit1_select;
	bool digit2_select;
	public static bool number_select;
	bool indexhold;
	public static bool hold;
	public static bool hold_coin;
	int temp;
    int temp2;
    public static int number;
    public AudioClip kada;
    public AudioClip Do;
    public AudioClip Re;
    public AudioClip Mi;
    AudioSource source;
	// Use this for initialization
	void Start () {
		switch_counter = 30;
		rotate_counter = 0;
		digit1 = 0;
		digit2 = 0;
		digit1_select = false;
		digit2_select = false;
		indexhold = false;
		temp = 0;
		hold = false;
		hold_coin = false;
		number_select = false;
        source = GetComponent<AudioSource>();
        source.volume = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 stick = OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
		if (stick.x > 0.8 && rotate_counter == 0) {
			switch_counter -= 1;
			if (switch_counter <= 0) {
                source.PlayOneShot(kada);
				//transform.parent.parent.parent.Rotate (new Vector3 (0, 90, 0));
				rotate_counter = 36;
				switch_counter = 30;
				digit1--;
			   	digit2--;
			}

		} else if (stick.x < -0.8 && rotate_counter == 0) {
			switch_counter -= 1;
			if (switch_counter <= 0) {
                source.PlayOneShot(kada);
                //transform.parent.parent.parent.Rotate (new Vector3 (0, -90, 0));
                rotate_counter = -36;
				switch_counter = 30;
				digit1++;
				digit2++;

			}

		} else {
			switch_counter = 30;
		}

		if (rotate_counter > 0) {
			ring.transform.Rotate (new Vector3 (0, 0, -3));
			rotate_counter-=3;

		}
		if (rotate_counter < 0) {
			ring.transform.Rotate (new Vector3 (0, 0, 3));
			rotate_counter+=3;
		}
		if (digit1 == -1) {
			digit1 = 9;
		}
		if (digit1 == 10) {
			digit1 = 0;
		}
		if (digit2 == -1) {
			digit2 = 9;
		}
		if (digit2 == 10) {
			digit2 = 0;
		}
		if (OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 1) {
			
			if(!indexhold){
				if (digit1_select == false) {
					temp = digit1;
					screen.text = temp + " ";
					digit1_select = true;
                    source.PlayOneShot(Do);
				} else if (digit2_select == false) {
                    temp2 = digit2;
					screen.text = temp + "" + temp2;
					digit2_select = true;

                    source.PlayOneShot(Re);
                } else {
					number = (10 * temp) + temp2;
					number_select = true;
                    source.PlayOneShot(Mi);
                    //print (number);
                }
			}
			indexhold = true;
		}
		if (OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) == 0) {
			indexhold = false;
		}
		if (OVRInput.GetDown (OVRInput.Button.PrimaryThumbstickUp, OVRInput.Controller.RTouch)) {
			number = 0;
			screen.text = "";
			digit1_select = false;
			digit2_select = false;
			print (number);
		}
		if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) < 1) {
			hold_coin = false;

		}

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "dice" && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0) {
			col.gameObject.GetComponent<Collider> ().isTrigger = false;
			hold = true;
		}
		if (col.gameObject.tag == "coin" && OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) > 0) {
			hold_coin = true;
			print("grab coin");
		}
	}
}
