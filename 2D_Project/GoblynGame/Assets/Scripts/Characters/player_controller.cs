using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class player_controller : character_controller {

	//temp color list
	Color[] armor_colors;

	public static player_controller player_object;

	public List<enemy_controller> dead_soldiers;
	GameObject menu_item;

	int max_health = 100; //bad

	void Awake(){
		DontDestroyOnLoad (this.gameObject);
		if(null == player_object){
			player_object = this;
		}
		else if(this != player_object){
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		armor_colors = new Color[] { Color.white, Color.yellow, Color.red, Color.blue };
		movement = GetComponent <character_movement> ();
		animator = GetComponent <Animator> ();
		weapon = GetComponentInChildren <weapon_controller> ();
		dead_soldiers = new List<enemy_controller> ();
		t_ui_player_updater.player_updater.Set_Hud_Values (max_health, health, 1, gold, teeth);
		armor = GetComponentInChildren <armor_controller> ();
	}

	void OnLevelWasLoaded(){
		armor_colors = new Color[] { Color.white, Color.yellow, Color.red, Color.blue };
		movement = GetComponent <character_movement> ();
		animator = GetComponent <Animator> ();
		weapon = GetComponentInChildren <weapon_controller> ();
		dead_soldiers = new List<enemy_controller> ();
		t_ui_player_updater.player_updater.Set_Hud_Values (max_health, health, 1, gold, teeth);
		armor = GetComponentInChildren <armor_controller> ();
	}
	
	// Update is called once per frame
	void Update () {
		Handle_User_Input ();
	}

	void Handle_User_Input(){
		if(Input.GetKey (KeyCode.LeftArrow)){
			movement.Set_Direction (-1);
		}
		else if(Input.GetKey (KeyCode.RightArrow)){
			movement.Set_Direction (1);
		}
		else{
			movement.Set_Direction (0);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			movement.Jump ();
		}

		if(Input.GetKeyDown (KeyCode.A)){
			StartCoroutine (weapon.Start_Attack ());
		}

		if(Input.GetKeyUp (KeyCode.E)){
			for(int i = 0; i < dead_soldiers.Count; i++){
				enemy_controller soldier = dead_soldiers [i];
				if(soldier.Get_Gold () > 0){
					Add_Gold (soldier.Get_Gold ());
					soldier.Remove_Gold (soldier.Get_Gold ());
					t_ui_player_updater.player_updater.Set_Hud_Values (max_health, health, 1, gold, teeth);
				}
				else if(soldier.Get_Teeth () > 0){
					Add_Teeth (soldier.Get_Teeth ());
					soldier.Remove_Teeth (soldier.Get_Teeth ());
					t_ui_player_updater.player_updater.Set_Hud_Values (max_health, health, 1, gold, teeth);
				}
				else{
					armor.Set_Armor (soldier.Get_Armor ());
					GetComponent <SpriteRenderer>().color = armor_colors[(int)soldier.Get_Armor ()];
				}
			}
		}
	}

	public void Set_Weapon(weapon_controller.weapons _weapon){
		weapon.Set_Weapon (_weapon);
	}

	public override void Remove_Gold (int amount)
	{
		base.Remove_Gold (amount);
		t_ui_player_updater.player_updater.Set_Hud_Values (max_health, health, 1, gold, teeth);
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(_col.gameObject.layer == LayerMask.NameToLayer ("Enemy")){
			enemy_controller soldier = _col.GetComponent <enemy_controller>();
			if(soldier.alive == false){
				dead_soldiers.Add (soldier);
			}
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(_col.gameObject.layer == LayerMask.NameToLayer ("Enemy")){
			dead_soldiers.Remove (_col.GetComponent <enemy_controller>());
		}
	}

}
