using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class r_player_controller : t_character_controller {

	public static r_player_controller character_controller;
	public enum states {idle, climbing, dialogue, shopping, menu};
	public states state = states.idle;
	public states last_state = states.idle;

	private t_bezier_spline climbing_spline;

	private t_weapon weapon_script;
	private t_armor armor_script;

	private t_player_movement movement;

	private List<GameObject> interactable_objects;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		//check that only one player exists
		if(null == character_controller){
			character_controller = this;
		}
		else if(this != character_controller){
			Destroy (character_controller.gameObject);
			character_controller = this;
		}

		//list of objects to interact with
		interactable_objects = new List<GameObject> ();
		//reference to movement script
		movement = GetComponent <t_player_movement> ();
		Set_Weapon (weapon_id);
	}
	
	// Update is called once per frame
	void Update () {
		Handle_User_Input ();
	}

	void Handle_User_Input(){
		if (Input.anyKey) {
			//choose Input to handle based on current player state
			switch (state) {
			case states.idle:
				Idle_Input ();
				break;
			case states.climbing:
				Climbing_Input ();
				break;
			case states.dialogue:
				Dialogue_Input ();
				break;
			case states.shopping:
				Shopping_Input ();
				break;
			case states.menu:
				Menu_Input ();
				break;
			default:
				Idle_Input ();
				break;
			}
		}
		else{
			movement.Stop ();
		}
	}

	void Idle_Input(){
		if(Input.GetKey (r_input_keys.move_left)){
			movement.Move_Left ();
		}
		else if(Input.GetKey (r_input_keys.move_right)){
			movement.Move_Right ();
		}
		else{
			movement.Stop ();
		}
		if(Input.GetKeyDown (r_input_keys.jump)){
			movement.Player_Jump ();
		}

		if(Input.GetKeyDown (r_input_keys.menu)){
			//movement.Stop (); //set velocity to zero
			Switch_States (states.menu);
			r_menu_controller.menu_controller.Activate ();
		}

		if(Input.GetKeyDown (r_input_keys.attack)){
			weapon_script.Attack ();
		}

		if(Input.GetKeyDown (r_input_keys.interact)){
			for(int i = 0; i < interactable_objects.Count; i++){
				GameObject interactible_object = interactable_objects [i];
				switch(interactible_object.GetComponent <r_interactible>().interactible_type){
				case r_interactible.interactible_types.npc:
					Switch_States (states.dialogue);
					interactible_object.GetComponent <r_npc_interaction>().Interact ();
					break;
				case r_interactible.interactible_types.dead_enemy:
					//does not switch states as a single action takes place
					interactible_object.GetComponent <r_dead_enemy_interaction> ().Interact ();
					break;
				case r_interactible.interactible_types.shop:
					Switch_States (states.shopping);
					interactible_object.GetComponent <r_shop_interaction> ().Interact ();
					break;
				case r_interactible.interactible_types.pickup:
					break;
				}
			}
		}

		if(null != climbing_spline){
			if(Input.GetKey (r_input_keys.climb_up)){
				r_spline_walking_utility.Set (climbing_spline, movement.character_rigidbody, 2f, 0.1f);
				Switch_States (states.climbing);

			}
			else if(Input.GetKey (r_input_keys.climb_down)){
				r_spline_walking_utility.Set (climbing_spline, movement.character_rigidbody, 2f, 0.9f);
				Switch_States (states.climbing);

			}
		}
	}

	void Climbing_Input(){
		/*
		 * Need to modify climing setup so that non-vertical climing works better
		 * Currently player snaps to position of spline when starting climb regardless
		 * of where they were when they initiated as long as they are within the climb
		 * bounding box.
		 * To fix: have collision with spline be a necessity and modify climbing logic so that
		 * leaving the climb bounding does not cause you to fall (setting the rigidbody non-kinematic)
		 * if you are still moving up or down the spline.
		*/
		if(climbing_spline != null){
			if(Input.GetKey (r_input_keys.climb_up)){
				r_spline_walking_utility.Move_Up_Bezier_Spline ();
			}
			else if(Input.GetKey (r_input_keys.climb_down)){
				r_spline_walking_utility.Move_Down_Bezier_Spline ();
			}
			else if(Input.GetKey (r_input_keys.move_left)){
				if(true == movement.Player_Is_Grounded ()){
					Switch_States (states.idle);
				}
			}
			else if(Input.GetKey (r_input_keys.move_right)){
				if(true == movement.Player_Is_Grounded ()){
					Switch_States (states.idle);
				}
			}
		}
	}

	void Dialogue_Input(){
		//dialogue box handles input
	}

	void Shopping_Input(){
		//shop interacits input
		//maybe shouldn't, not sure yet
	}

	void Menu_Input(){
		if(Input.GetKeyDown(r_input_keys.menu)){
			Switch_States (states.idle);
		}
	}

	public void Switch_States(states desired_state){
		if(states.dialogue == desired_state || states.menu == desired_state || states.shopping == desired_state){
			Time.timeScale = 0;
			last_state = state;
			state = desired_state;
		}
		else if(states.idle == desired_state){
			Time.timeScale = 1;
			last_state = state;
			state = desired_state;
			movement.character_rigidbody.isKinematic = false;
		}
		else if(states.climbing == desired_state){
			Time.timeScale = 1;
			last_state = state;
			state = desired_state;
			movement.character_rigidbody.isKinematic = true;
		}
	}

	public IEnumerator Delay_Switch_States(states _desired_state, float _delay){
		yield return new WaitForSeconds (_delay);
		Switch_States (_desired_state);
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(null != _col.GetComponent<r_shop_interaction>()){
			interactable_objects.Add (_col.gameObject);
		}
		else if(null != _col.GetComponent <r_npc_interaction>()){
			interactable_objects.Add (_col.gameObject);
		}
		else if(null != _col.GetComponent <r_dead_enemy_interaction>()){
			interactable_objects.Add (_col.gameObject);
		}

		if(_col.gameObject.layer == LayerMask.NameToLayer ("Climbable")){
			print ("Enter spline");
			climbing_spline = _col.gameObject.GetComponent <t_bezier_spline> ();
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(null != _col.GetComponent <r_interactible>()){
			interactable_objects.Remove (_col.gameObject);
		}
		else if(null != _col.GetComponent <r_npc_interaction>()){
			interactable_objects.Remove (_col.gameObject);
		}
		else if(null != _col.GetComponent <r_dead_enemy_interaction>()){
			interactable_objects.Remove (_col.gameObject);
		}

		if(_col.gameObject.layer == LayerMask.NameToLayer ("Climbable")){
			r_spline_walking_utility.Reset ();
			climbing_spline = null;
			Switch_States (states.idle);
		}
	}

	/*
	 * The following code is for getting and setting weapon, armor, gold, teeth, health and shield.
	 * This should have no impact on gameplay logic.
	*/

	public void Set_Weapon(int _weapon_id){
		weapon_id = _weapon_id;
		Remove_Weapon ();

		GameObject weapon = (GameObject)Instantiate (Resources.Load (item_codes.game_items [_weapon_id])) as GameObject;
		weapon.transform.parent = this.transform;
		weapon.transform.position = this.transform.position;
		weapon_script = weapon.GetComponent <t_weapon> ();
	}

	void Remove_Weapon(){
		if(null != this.GetComponentInChildren <t_weapon>()){
			Destroy (this.GetComponentInChildren <t_weapon> ().gameObject);
		}
	}

	public void Set_Armor(int _armor_id){
		armor_id = _armor_id;
	}

	void Remove_Armor(){
		
	}

	public override void Set_Gold(int _gold){
		gold = _gold;
		t_ui_player_updater.player_updater.Set_UI_Gold (_gold);
	}

	public override void Add_Gold(int _gold){
		gold += _gold;
		t_ui_player_updater.player_updater.Set_UI_Gold (gold);
	}

	public override void Set_Health(int _health){
		print ("UPdate health");
		health = _health;
		t_ui_player_updater.player_updater.Set_UI_Health (max_health, health);
	}

	public override void Add_Health(int _health){
		max_health += _health;
		t_ui_player_updater.player_updater.Set_UI_Health (max_health, max_health);
	}

	public override void Set_Teeth(int _teeth){
		teeth = _teeth;
		t_ui_player_updater.player_updater.Set_UI_Teeth (teeth);
	}

	public override void Add_Teeth(int _teeth){
		teeth += _teeth;
		t_ui_player_updater.player_updater.Set_UI_Teeth (teeth);
	}

	public void Update_UI(){
		t_ui_player_updater.player_updater.Set_UI_Gold (Get_Gold ());
		t_ui_player_updater.player_updater.Set_UI_Health (Get_Max_Health (), Get_Health ());
		t_ui_player_updater.player_updater.Set_UI_Shield (1);
		t_ui_player_updater.player_updater.Set_UI_Teeth (Get_Teeth ());

	}
}
