using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lefthand : MonoBehaviour {
	// Use this for initialization
	LineRenderer line;
	public Material l_material;
	public Transform my_pos;
	public GameObject arrow;
	public GameObject mark;
	public GameObject maze;
	public GameObject dice;
	public GameObject coin;
	public GameObject mask;
	public GameObject selected;
	public Text room10_ins;
	public Text room19_ins;
	public Text room40_ins;
	public Text room49_ins;
	public Text room49_money;
    public Text room1_ins;
	public Slider slider;
	public Light light;
    public GameObject compass;
    public GameObject skull;
    public AudioClip spooky;
    public AudioClip xiu;
    public AudioClip crush;
    public AudioClip explosion;
    public AudioClip applause;
    AudioSource source;
	GameObject target;
	GameObject prev_target;
	GameObject facingwall;
	GameObject red_block;
	GameObject ball;
	GameObject reward;
	GameObject thedice;
    GameObject wall1;
    GameObject wall2;
    GameObject wall3;
    GameObject wall4;
    public GameObject dead_screen;
    Vector3 new_position;
	Vector3 old_position;
	float distance;
	int switch_counter;
	int rotate_counter;
	int tele_counter;
	int guess;
	int choice;
	int choice2;
	int sum;
	int cpu_money;
	int final_flag;
    int wall_counter;
	List<int> choices;
	int reward_num;
	int mask_reward;
	float ins_counter;
	float mask_counter;
	int stages;
	bool room10_triggered;
	bool room10_passed;
	bool room19_triggered;
	bool room19_passed;
	bool room40_triggered;
	bool room40_passed;
	bool room49_triggered;
	bool room49_passed;
	bool ghost_triggered;
    bool room18_triggered;
    bool room18_passed;
    bool room36_triggered;
    bool room36_passed;
    bool room1_triggered;
    bool room1_passed;
    bool room43_triggered;
    bool room43_passed;
    bool room33_triggered;
    bool room33_passed;
    bool room41_triggered;
    bool room41_passed;
    bool has_select;
	bool can_attack;
	bool attack_mode;
    bool turnormove;
    bool wall_pushing;
    bool reload = false;
    public static bool reset;
    public static bool game_over;
	public static int room_index;
	int ghost_index;
	public static bool abletomove;
    Queue<GameObject> arrows;
    int num_arr;
	Vector3 init_pos;
	Vector3 change;
	int move_counter;
	void Start () {
		line = gameObject.AddComponent<LineRenderer>();
        if (line != null)
        {
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;
            line.positionCount = 2;
            line.startColor = Color.black;
            line.startColor = Color.black;
            line.material = l_material;
        }
        num_arr = 0;
        arrows = new Queue<GameObject>();
		distance = 0;
		new_position = transform.parent.parent.parent.position;
		old_position = new_position;
        source = this.GetComponent<AudioSource>();
        source.volume = 0.03f;
		switch_counter = 30;
		tele_counter = 100;
		rotate_counter = 0;
		room_index = 1;
		
		ins_counter = 0;
		mask_counter = 0;
		stages = 0;
		guess = 0;
		choice = 0;
		choice2 = 0;
		choices = new List<int>{0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
		sum = 0;
		room10_triggered = false;
		room10_passed = false;
		room19_triggered = false;
		room19_passed = false;
		room40_passed = false;
		room40_triggered = false;
		ghost_triggered = false;
        room18_triggered = false;
        room18_passed = false;
        room36_triggered = false;
        room36_passed = false;
        room1_passed = false;
        room43_triggered = false;
        room43_passed = false;
        room33_triggered = false;
        room33_passed = false;
        room41_triggered = false;
        room41_passed = false;
        has_select = false;
		can_attack = false;
		attack_mode = false;
		init_pos = new Vector3(8.775406f, 43.0f, -43.59833f);
		ghost_index = 1;
		move_counter = 0;
		cpu_money = 15;
		final_flag = 0;
		slider.maxValue = 1.0f;
		slider.minValue = 0.0f;
		slider.value = 0.5f;
		light.intensity = 0.5f;
        turnormove = true;
        mask_reward = 5;
        game_over = false;
        wall_pushing = false;
        wall_counter = 1300;
        dead_screen.SetActive(false);
        dicebehavior.delay_counter = 250;
       //Instantiate(coin, transform.parent.parent.parent.position + 0.5f * transform.parent.parent.parent.forward, compass.transform.rotation);
        room1_ins.text = "Welcome to my maze";
        ins_counter = 6;
        //PlayerPrefs.SetInt("reload", 0);
        if (PlayerPrefs.GetInt("reload") == 1)
        {
            room1_triggered = false;
            abletomove = true;
            
        }
        else
        {
            room1_triggered = true;
            abletomove = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		room49_money.text = "My coins: " + cpu_money;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Vector3 facing = compass.transform.forward;
		RaycastHit hit;
		RaycastHit hit2;

		line.SetPosition(0, transform.position);
		line.SetPosition(1, transform.position + fwd * 800);
		Vector2 stick = OVRInput.Get (OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        if (game_over && !wall_pushing) {
            
            if (Physics.Raycast(compass.transform.position, compass.transform.forward, out hit2, 100)) {
                wall1 = hit2.collider.gameObject;
            }
            if (Physics.Raycast(compass.transform.position, -compass.transform.forward, out hit2, 100))
            {
                wall2 = hit2.collider.gameObject;
            }
            if (Physics.Raycast(compass.transform.position, compass.transform.right, out hit2, 100))
            {
                wall3 = hit2.collider.gameObject;
            }
            if (Physics.Raycast(compass.transform.position, -compass.transform.right, out hit2, 100))
            {
                wall4 = hit2.collider.gameObject;
            }
            if (wall1 != null && wall2 != null && wall3 != null && wall4 != null) {
                wall_pushing = true;
            }
        }
        if (wall_pushing && wall_counter > 0) {
            wall1.transform.Translate(wall1.transform.up / 300.0f, Space.World);
            wall2.transform.Translate(wall2.transform.up / 300.0f, Space.World);
            wall3.transform.Translate(wall3.transform.up / 300.0f, Space.World);
            wall4.transform.Translate(wall4.transform.up / 300.0f, Space.World);
            wall_counter--;
        }
        if (wall_counter == 255) {
            dead_screen.SetActive(true);
            source.PlayOneShot(crush);
        }
        if (dead_screen.active) {
            dead_screen.GetComponent<Renderer>().material.color = new Color(154f/255f, 0f, 0f, (255f-wall_counter)/255f);
        }
        if (wall_counter <= 0) {
            Instantiate(skull, transform.parent.parent.parent.position + 0.5f * transform.parent.parent.parent.forward,skull.transform.rotation);
            PlayerPrefs.SetInt("reload", 1);
            SceneManager.LoadScene("Rooms");
            Start();
        }
		if (OVRInput.Get (OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch)) {
			slider.gameObject.SetActive (true);
			slider.enabled = true;
			if (stick.x > 0.8) {
				if (slider.value < 1) {
					slider.value += 0.01f;
					light.intensity += 0.01f;
				}
			} else if (stick.x < -0.8 && rotate_counter == 0) {
				if (slider.value > 0) {
					slider.value -= 0.01f;
					light.intensity -= 0.01f;
				}
			}
		}
	    else {
			slider.gameObject.SetActive (false);
			slider.enabled = false;
			if (stick.x > 0.8 && rotate_counter == 0 && turnormove) {
				switch_counter -= 1;
				if (switch_counter <= 0) {
					//transform.parent.parent.parent.Rotate (new Vector3 (0, 90, 0));
					rotate_counter = 90;
					switch_counter = 30;
                    turnormove = false;
				}

			} else if (stick.x < -0.8 && rotate_counter == 0 && turnormove) {
				switch_counter -= 1;
				if (switch_counter <= 0) {
					//transform.parent.parent.parent.Rotate (new Vector3 (0, -90, 0));
					rotate_counter = -90;
					switch_counter = 30;
                    turnormove = false;
                }
			} else {
                turnormove = true;
                switch_counter = 30;
			}

			if (rotate_counter > 0) {
				transform.parent.parent.parent.Rotate (new Vector3 (0, 1, 0));
				mask.transform.Rotate (new Vector3 (0, 1, 0));
				arrow.transform.Rotate (new Vector3 (0, 1, 0));
				rotate_counter--;
			}
			if (rotate_counter < 0) {
				transform.parent.parent.parent.Rotate (new Vector3 (0, -1, 0));
				mask.transform.Rotate (new Vector3 (0, -1, 0));
				arrow.transform.Rotate (new Vector3 (0, -1, 0));
				rotate_counter++;
			}
            if (rotate_counter == 0) {
                turnormove = true;
            }
		}

		if (mask_reward > 0) {
			if (reward == null) {
				reward = Instantiate (coin, transform.parent.parent.parent.position + 0.5f * transform.parent.parent.parent.forward, compass.transform.rotation);
                //print(reward.transform.position);
				mask_reward--;
				//print (mask_reward);
			}
		}
		if (Physics.Raycast (transform.position, fwd, out hit, 100)) {
            if(line != null)
			    line.SetPosition (1, hit.point);
			target = hit.collider.gameObject;
			if (distance > 0.02f)
			{
				transform.parent.parent.parent.position += Vector3.Normalize(new_position - old_position) * 0.02f;
				distance -= 0.02f;
			}
			if (target.tag == "Gd") {
				if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) == 1) {
					new_position = hit.point;
					old_position = transform.parent.parent.parent.position;
					new_position.y = 43.0f;
					distance = Vector3.Distance (new_position, old_position);
				}
			}
			if (prev_target != target && red_block != null) {
				Destroy (red_block);
				has_select = false;
			}
			if (target.tag == "block") {
				//print (target.name);
				if (!has_select) {
					red_block = Instantiate (selected, target.transform.position, target.transform.rotation);
					red_block.transform.SetParent (maze.transform);
					has_select = true;
				}
				if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && can_attack) {
					//print (target.name + " is attacked");
					int attack_index = int.Parse(target.name);
					//print (attack_index + " " +ghost_index);
                    source.PlayOneShot(explosion);
					if (attack_index == ghost_index) {
						ghost_index = 100;
						//print ("destroyed");
						ghost_triggered = false;
						mask.transform.position = Vector3.zero;
						mask_reward = 10;
					}
                    if (attack_index == room_index) {
                        game_over = true;
                    }
					can_attack = false;
				}
			}
			if (Physics.Raycast (compass.transform.position, facing, out hit2, 100)) {
				facingwall = hit2.collider.gameObject;
			}

			if (target.tag == "door" && target == facingwall && abletomove && !game_over) {
				if (OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) == 1 && turnormove) {
					tele_counter--;
                    turnormove = false;
					if (tele_counter <= 0) {
						change = target.transform.position + 5 * Vector3.Normalize (facing);
						move_counter = 60;
                        source.PlayOneShot(xiu);
                        if (reward != null)
                        {
                           // print("???");
                            Destroy(reward);
                            reward_num = 0;
                            mask_reward = 0;
                        }
                        if (facing == Vector3.forward) {
							room_index += 1;
						} else if (facing == Vector3.back) {
							room_index -= 1;
						} else if (facing == Vector3.left) {
							room_index += 7;
						} else if (facing == Vector3.right) {
							room_index -= 7;
						}
                        if (room_index == 43 && !room43_passed) {
                            room43_triggered = true;
                        }
                        if (room_index == 41 && !room41_passed)
                        {
                            room41_triggered = true;
                        }
                        if (room_index == 33 && !room33_passed)
                        {
                            room33_triggered = true;
                        }
                        if (room_index == 36 && !room36_passed)
                        {
                            room36_triggered = true;
                        }
                        if (room_index == 18 && !room18_passed) {
                            room18_triggered = true;
                        }
						if (room_index == 10 && !room10_passed) {
							abletomove = false;
							room10_triggered = true;
							room10_ins.text = "Welcome to the room of dice";
							ins_counter = 4;
							stages = 0;
						}
						if (room_index == 19 && !room19_passed) {
							abletomove = false;
							room19_triggered = true;
							room19_ins.text = "";
							ins_counter = 1;
							stages = 0;
						}
						if (room_index == 40 && !room40_passed) {
							abletomove = false;
							room40_triggered = true;
							room40_ins.text = "";
							ins_counter = 1;
							stages = 0;
						}
						if (room_index == 49 && !room49_passed) {
							
							abletomove = false;
							room49_triggered = true;
							room49_ins.text = "Impressive, you made it to the last room.";
							ins_counter = 5;
							stages = 0;
						}
						if (ghost_triggered) {
							int x = Random.Range (0, 7);
							int y = Random.Range (0, 7);
							//int x = 0;
							//int y = 1;
							ghost_index = x * 7 + y * 1 + 1;
							while (ghost_index == 10 || ghost_index == 19 || ghost_index == 40) {
								x = Random.Range (0, 7);
								y = Random.Range (0, 7);
								ghost_index = x * 7 + y * 1 + 1;
							}
							//print (x + " " + y);
							Vector3 tele_pos = init_pos;
							tele_pos.x -= x * 10.0f;
							tele_pos.z += y * 10.0f;
							mask.transform.position = tele_pos;
                            if (room_index == ghost_index)
                            {
                                mask_counter = 4;
                                source.PlayOneShot(spooky);
                                walletbehavior.money = walletbehavior.money / 2;
                            }
                            else
                            {
                                if (attack_mode)
                                {
                                    can_attack = true;
                                }
                            }
                        }
						//print (room_index + " " + ghost_index);
						
						change.y = 43.0f;
						change = change - transform.parent.parent.parent.position;
                        //transform.parent.parent.parent.position = change;
                        GameObject temp_arrow = Instantiate(mark, arrow.transform.position, arrow.transform.rotation);
                        temp_arrow.transform.SetParent (maze.transform);
                        if (arrows.Count == 6) {
                            Destroy(arrows.Dequeue());
                        }
                        arrows.Enqueue(temp_arrow);
						arrow.transform.position += (-arrow.transform.forward) * 0.05f;
						tele_counter = 100;
					}
				} else {
					tele_counter = 100;
				}
			} else {
				if (target.tag != "door") {
					//print (target.name);
				}
				else if (!abletomove) {
					//print (room_index);
				}
				else if (target != facingwall) {
					//print ("not facing door! " + target.name);
				}
			}
			prev_target = target;
		}
		if (move_counter > 0) {
			transform.parent.parent.parent.Translate (change / 60.0f, Space.World);
			move_counter--;
        }
        if (move_counter <= 0) {
            turnormove = true;
            if (room18_triggered && !room18_passed)
            {
                mask_reward = 3;
                room18_passed = true;
            }
            else if (room33_triggered && !room33_passed)
            {
                mask_reward = 3;
                room33_passed = true;
            }
            else if (room43_triggered && !room43_passed)
            {
                mask_reward = 3;
                room43_passed = true;
            }
            else if (room41_triggered && !room41_passed)
            {
                mask_reward = 3;
                room41_passed = true;
            }
            
        }
		if (mask_counter > 0) {
			mask.transform.Translate (Vector3.Normalize(-mask.transform.up + mask.transform.right)/15.0f, Space.World);
			mask_counter -= Time.deltaTime;
		}
        
        if (room1_triggered) {
            ins_counter -= Time.deltaTime;
            if (stages == 0) {
                if (ins_counter <= 0) {
                    room1_ins.text = "Now I'll give you a little tutorial.";
                    stages++;
                    ins_counter = 5;
                }
            }
            if (stages == 1)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Look right, you should see your map and wallet";
                    stages++;
                    ins_counter = 5;
                }
            }
            if (stages == 2)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "They will move with you, and you can use your right hand to change their position relative to you";
                    stages++;
                    ins_counter = 8;
                }
            }
            if (stages == 3)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Grab the coin in front of you and touch your wallet with coin to store coins";
                    stages++;
                    ins_counter = 6;
                }
            }
            if (stages == 4)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Coins are your life, if you have less than 0 coins, that is game over.";
                    stages++;
                    ins_counter = 6;
                }
            }
            if (stages == 5)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Now look at your righthand, there should be a ring.";
                    stages++;
                    ins_counter =5;
                }
            }
            if (stages == 6)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "It can be used to input 2-digit numbers, it will come handy.";
                    stages++;
                    ins_counter = 5;
                }
            }
            if (stages == 7)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Push righthand stick left and right to turn the ring, and hold right index trigger to select digits";
                    stages++;
                    ins_counter = 5;
                }
            }
            if (stages == 8)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Once your select both digits, hold trigger again to input number. Push stick forward to clear";
                    stages++;
                    ins_counter = 5;
                }
            }
            if (stages == 9)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Try input 14";
                    stages++;
                    righthand.number_select = false;
                }
            }
            if (stages == 10)
            {
                if (righthand.number_select)
                {
                    if (righthand.number == 14)
                    {
                        room1_ins.text = "That is correct.";
                        stages++;
                        ins_counter = 3;
                    }
                    else {
                        room1_ins.text = "That is not 14, push stick forward to clear.";
                    }
                }
            }

            if (stages == 11)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Push your lefthand stick to turn left or right";
                    stages++;
                    ins_counter = 6;
                }
            }
            if (stages == 12)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Black walls are doors to next room.";
                    stages++;
                    ins_counter = 5;
                }
            }
            if (stages == 13)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Use ray from you lefthand to select black doors, then hold left index trigger to go to next room";
                    stages++;
                    ins_counter = 6;
                }
            }
            if (stages == 14)
            {
                if (ins_counter <= 0)
                {
                    room1_ins.text = "Now, good luck";
                    room1_passed = true;
                    room1_triggered = false;
                    abletomove = true;
                }
            }
        }
		if (room49_triggered) {
			ins_counter -= Time.deltaTime;
			if (stages == 0) {
				if (ins_counter <= 0) {
					room49_ins.text = "Now you will have to finish one last game";
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 1) {
				if (ins_counter <= 0) {
					room49_ins.text = "The rule might be a little complex, so pay attention";
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 2) {
				if (ins_counter <= 0) {
					room49_ins.text = "I have 15 coins. And you have " + walletbehavior.money + " coins";

					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 3) {
				if (ins_counter <= 0) {
					room49_ins.text = "The coins you have been collected will give you advantages here";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 4) {
				if (ins_counter <= 0) {
					room49_ins.text = "Each turn, you have to decide how much you wanna pay for right to attack";
					stages++;
					ins_counter = 6;
				}
			}
			if (stages == 5) {
				if (ins_counter <= 0) {
					room49_ins.text = "You can choose from 0 coins to 5 coins, same for me";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 6) {
				if (ins_counter <= 0) {
					room49_ins.text = "Both will pay the price, but only the one paid more became attacker for this turn";
					stages++;
					ins_counter = 7;
				}
			}
			if (stages == 7) {
				if (ins_counter <= 0) {
					room49_ins.text = "Then you will have to throw a dice, the attacker will take the result amount of coins from the other";
					stages++;
					ins_counter = 8;
				}
			}
			if (stages == 8) {
				if (ins_counter <= 0) {
					room49_ins.text = "If we pay the same amount of coins, we call it a tie for this turn, no one get or lose any coin";
					stages++;
					ins_counter = 8;
				}
			}
			if (stages == 9) {
				if (ins_counter <= 0) {
					room49_ins.text = "If one of us have less than 0 coins, the other wins";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 10) {
				if (ins_counter <= 0) {
					room49_ins.text = "If we both end up with no coins, well, I'll just let you win";
					stages++;
					ins_counter = 6;
				}
			}
			if (stages == 11) {
				if (thedice != null) {
					Destroy (thedice);
					righthand.hold = false;
					dicebehavior.delay_counter = 250;
				}
				if (ins_counter <= 0) {
					room49_ins.text = "Now, decide how much you want to pay for this turn";
					stages++;
					righthand.number_select = false;
				}
			}
			if (stages == 12) {
				if (righthand.number_select) {
					if (righthand.number > 5 || righthand.number > walletbehavior.money) {
						room49_ins.text = "You choose " + righthand.number + " coins. Too much. Reinput please"; 
						righthand.number_select = false;
					} else {
						int cpu_choose = 100;
						while(cpu_choose > cpu_money){
							cpu_choose = Random.Range (0, 6);
						}
						cpu_money -= cpu_choose;
						walletbehavior.money -= righthand.number;
						if (cpu_choose > righthand.number) {
							room49_ins.text = "You pay " + righthand.number + " coins, and I pay " + cpu_choose + " coins. I win.";
							final_flag = 0;
							stages++;
							ins_counter = 6;
						} else if (cpu_choose < righthand.number) {
							room49_ins.text = "You pay " + righthand.number + " coins, and I pay " + cpu_choose + " coins. You win.";
							stages++;
							ins_counter = 6;
							final_flag = 1;
						} else {
							room49_ins.text = "we both pay " + righthand.number + " coins.";
							if (righthand.number == 0) {
								stages = 15;
							} else {
								stages = 11;
							}
							ins_counter = 4;
						}
					}
				}
			}
			if (stages == 13) {
				if (ins_counter <= 0) {
					room49_ins.text = "Throw the dice";
					stages++;
					thedice = Instantiate (dice, transform.parent.parent.parent.position + 0.5f * transform.parent.parent.parent.forward, dice.transform.rotation);
				}
			}
			if (stages == 14) {
				if (dicebehavior.delay_counter == 0) {
					//print ("dice throwed");
					room49_ins.text = "Dice get " + dicebehavior.result;
					ins_counter = 3;
					if (final_flag == 0) {
						cpu_money += dicebehavior.result;
						walletbehavior.money -= dicebehavior.result;
					} else if (final_flag == 1) {
						cpu_money -= dicebehavior.result;
						walletbehavior.money += dicebehavior.result;
					}
					if (cpu_money < 0) {
						stages = 15;
						ins_counter = 3;
					} else if (walletbehavior.money < 0) {
						stages = 16;
						ins_counter = 3;
					} else {
						stages = 11;
						ins_counter = 3;
					}
				}

			}
			if (stages == 15) {
				if (ins_counter <= 0) {
					room49_ins.text = "Congratulations! That is the end of it.";
                    source.PlayOneShot(applause);
                    stages++;
                    ins_counter = 4;
				}
			}
            if (stages == 16)
            {
                if (ins_counter <= 0)
                {
                    room49_ins.text = "To be continued";
                    abletomove = true;
                    room40_triggered = false;
                    room40_passed = true;
                }
            }
           

        }
		if (room40_triggered) {
			ins_counter -= Time.deltaTime;
			if (stages == 0) {
				if (ins_counter <= 0) {
					room40_ins.text = "Welcome to the room of duel";
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 1) {
				if (ins_counter <= 0) {
					room40_ins.text = "You should see your opponent now";
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 2) {
				if (ins_counter <= 0) {
					room40_ins.text = "Now give me a number between 0 to 10";
					stages++;
					righthand.number_select = false;
				}
			}
			if (stages == 3) {
				if (righthand.number_select) {
					room40_ins.text = "You select " + righthand.number;
					choice = righthand.number;
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 4) {
				if (ins_counter <= 0) {
					if (choice > 10) {
						room40_ins.text = "Too large, repick please";
						stages--;
						righthand.number_select = false;
					} 
					else {
						choice2 = Random.Range (0,11);
						for (int i = 0; i < choices.Count; i++) {
							if (choices [i] > choice2 + 10 || choices[i] < choice2) {
								choices.RemoveAt (i);
								i--;
							}
						}
						sum = choice + choice2;
						room40_ins.text = "now both of you have given me a number";
						stages++;
						ins_counter = 4;
					}
				}
			}
			if (stages == 5) {
				if (ins_counter <= 0) {
					room40_ins.text = "Now you have to guess the sum of your numbers, one at a time";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 6) {
				if (ins_counter <= 0) {
					room40_ins.text = "The first one find out the sum wins";
					reward_num = 6;
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 7) {
				if (ins_counter <= 0) {
					room40_ins.text = "Start from you, player, input a number please";
					stages++;
					righthand.number_select = false;
				}
			}
			if (stages == 8) {
				if (righthand.number_select) {
					if (righthand.number < choice || righthand.number > choice + 10) {
						room40_ins.text = "You select " + righthand.number + ". That is impossible, your wasted your chance. Now your opponent";
						stages++;
					} else if (righthand.number == sum) {
						room40_ins.text = "You select " + righthand.number + ". That is the sum";
						stages = 10;//
					} else {
						room40_ins.text = "You select " + righthand.number + ". Nice try, but wrong. Now your opponent";
						int selection = righthand.number;
						stages++;
						if (selection < 10) {
							for (int i = 0; i < choices.Count; i++) {
								if (choices [i] >= choice2 + selection || choices[i] == selection) {
									choices.RemoveAt (i);
									i--;
								}
							}
						} else if (selection > 10) {
							for (int i = 0; i < choices.Count; i++) {
								if (choices [i] <= selection + choice2 - 10 || choices[i] == selection) {
									choices.RemoveAt (i);
									i--;
								}
							}
						}
					}
					ins_counter = 8;
				}
			}
			if (stages == 9) {
				if (ins_counter <= 0) {
					int index = choices.Count / 2;
					int selection2 = choices[index];
					//print (choices.Count + "  == " + index);
					if (selection2 == sum) {
						room40_ins.text = "Your opponent select " + selection2 + ". That is the sum";
						stages = 11;//
						ins_counter = 5;
					} else {
						room40_ins.text = "Your opponent select " + selection2 + ". Wrong, your turn again";
						choices.Remove (selection2);
						stages = 8;
						righthand.number_select = false;
					}
				}
			}
			if (stages == 10) {
				if (ins_counter <= 1) {
					room40_ins.text = "You win! Here is your reward.";
					if (reward_num > 0) {
						if (reward == null) {
							reward = Instantiate (coin, transform.parent.parent.parent.position + 0.5f * transform.parent.parent.parent.forward, compass.transform.rotation);
							reward_num--;
							//print (reward_num);
						}
					} else {
						stages = 12;
					}
				}
			}
			if (stages == 11) {
				if (ins_counter <= 0) {
					room40_ins.text = "You lose! I'll take away 6 coins from you";
					walletbehavior.money -= 6;
					stages++;
				}
			}
			if(stages == 12){
				abletomove = true;
				room40_triggered = false;
				room40_passed = true;
			}
		}
		if (room19_triggered) {
			ins_counter -= Time.deltaTime;
			if (stages == 0) {
				if (ins_counter <= 0) {
					ins_counter = 4;
					stages++;
				}
			}
			if (stages == 1) {
				mask.transform.Translate (Vector3.Normalize(-mask.transform.up + mask.transform.right)/15.0f, Space.World);
                source.PlayOneShot(spooky);
				if (ins_counter <= 0) {
					ins_counter = 0;
					stages++;
				}
			}
			if (stages == 2) {
				if (ins_counter <= 0) {
					room19_ins.text = "Wow, suprise!";
					stages++;
					ins_counter = 3;
				}
			}
			if (stages == 3) {
				if (ins_counter <= 0) {
					room19_ins.text = "Unfortunately, you just woke up the ghost of the maze";
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 4) {
				if (ins_counter <= 0) {
					room19_ins.text = "From now on, each time you go to a new room, you might see it is waiting for you";
					stages++;
					ins_counter = 7;
				}
			}
			if (stages == 5) {
				if (ins_counter <= 0) {
					room19_ins.text = "Nevermind, it won't kill you.";
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 6) {
				if (ins_counter <= 0) {
					room19_ins.text = "But each time it sees you, it will take half of your coins away";
					stages++;
					ins_counter = 6;
				}
			}
			if (stages == 7) {
				if (ins_counter <= 0) {
					room19_ins.text = "Well, you still have a change to defeat it";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 8) {
				if (ins_counter <= 0) {
					room19_ins.text = "Each time you go to a new room, you get a chance for attacking one of rooms";
					stages++;
					ins_counter = 8;
				}
			}
			if (stages == 9) {
				if (ins_counter <= 0) {
					room19_ins.text = "Choose the room on map using your left hand.";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 10) {
				if (ins_counter <= 0) {
					room19_ins.text = "If your attack the room the ghost is in, it will be destroyed and you'll get reward";
					stages++;
					ins_counter = 8;
				}
			}
			if (stages == 11) {
				if (ins_counter <= 0) {
					room19_ins.text = "One last thing, never attack the room you are in";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 12) {
				if (ins_counter <= 0) {
					room19_ins.text = "Now leave. Good luck";
					abletomove = true;
					room19_triggered = false;
					room19_passed = true;
					ghost_triggered = true;
					attack_mode = true;
				}
			}
		}
		if (room10_triggered) {
			ins_counter -= Time.deltaTime;
			if (stages == 0) {
				if (ins_counter <= 0) {
					room10_ins.text = "Later you have to guess the result of dice";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 1) {
				if (ins_counter <= 0) {
					room10_ins.text = "You'll be rewarded if result of dice is larger than or equal to your guess";
					stages++;
					ins_counter = 8;
				}
			}
			if (stages == 2) {
				if (ins_counter <= 0) {
					room10_ins.text = "The greater you guess, the greater the rewards.";
					stages++;
					ins_counter = 5;
				}
			}
			if (stages == 3) {
				if (ins_counter <= 0) {
					room10_ins.text = "But be careful, if result of dice is smaller, you'll be punished";
					stages++;
					ins_counter = 8;
				}
			}
			if (stages == 4) {
				if (ins_counter <= 0) {
					room10_ins.text = "Now make your guess using your number ring";
					stages++;
					righthand.number_select = false;
				}
			}
			if (stages == 5) {
				if (righthand.number_select) {
					room10_ins.text = "You select " + righthand.number;
					guess = righthand.number;
					stages++;
					ins_counter = 4;
				}
			}
			if (stages == 6) {
				if (ins_counter <= 0) {
					if (guess > 6) {
						room10_ins.text = "Too large, repick please";
						stages--;
						righthand.number_select = false;
					} else if(guess == 0){
						room10_ins.text = "Cannot be 0, repick please";
						stages--;
						righthand.number_select = false;
					}
					else {
						reward_num = guess;
						room10_ins.text = "now pick up and throw the dice using your right hand";
						stages++;
						thedice = Instantiate (dice, transform.parent.parent.parent.position + 0.5f * transform.parent.parent.parent.forward, dice.transform.rotation);
					}
				}
			}
			if (stages == 7) {
				if (dicebehavior.delay_counter == 0) {
					if (dicebehavior.result >= guess) {
						room10_ins.text = "Dice is " + dicebehavior.result + ", and you guessed " + guess + ". Here are your rewards";
						if (reward_num > 0) {
							if (reward == null) {
								reward = Instantiate (coin, transform.parent.parent.parent.position + 0.5f * transform.parent.parent.parent.forward, compass.transform.rotation);
								reward_num--;
								//print (reward_num);
							}
						} else {
							stages++;
							ins_counter = 6;
						}
					} else {
						room10_ins.text = "Dice is " + dicebehavior.result + ", and you guessed " + guess + ". I'll take away two coin from you";
						walletbehavior.money -= 2;
						stages++;
						ins_counter = 6;
					}
				}
			}
			if (stages == 8) {
				if (ins_counter <= 0) {
					room10_ins.text = "Feel free to leave now";
					room10_triggered = false;
					room10_passed = true;
					abletomove = true;
				}
			}

		}	
	}
}
