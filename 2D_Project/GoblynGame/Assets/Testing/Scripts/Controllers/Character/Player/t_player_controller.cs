using UnityEngine;
using System.Collections;

public class t_player_controller : t_character_controller {

	public static t_player_controller player_controller;

	//weapon values and components
	private GameObject weapon_position;
	public GameObject player_weapon_object;
	public t_weapon player_weapon_script;

	[SerializeField]
	public GameObject player_armor_object;
	public t_armor player_armor_script;


	protected t_enemy_controller dead_enemy;

	void Start(){
		if(null == player_controller){
			player_controller = this;
		}
		else if(this != player_controller){
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);
		Initialise (weapon_id, armor_id);
	}

	void Update(){
		Handle_User_Input ();
	}

	void Handle_User_Input(){

		//not liking this use of player states but can't think of another way to do it
		if (Input.GetKeyDown (KeyCode.Escape) && (t_player_states.Is_Free_Moving () || t_player_states.Is_In_Menu ())) {
		}

		if (t_player_states.Is_Free_Moving ()) {
			if (Input.GetKeyDown (KeyCode.A)) {
				if (null != player_weapon_script) {
					player_weapon_script.Attack ();
				}
			}

			if (Input.GetKeyDown (KeyCode.D)) {
				Load_Armor (11);
			}
		}

		if(true == t_player_states.Is_Free_Moving () || true == t_player_states.Is_Interacting_With_Corpse () ){
			if(Input.GetKeyDown (KeyCode.E)){
				if(null != dead_enemy){
					dead_enemy.Loot ();
				}
			}
		}
		
	}

	public void Initialise(int _weapon_id, int _armor_id){
		weapon_position = this.transform.GetChild (0).gameObject;
		Load_Weapon (_weapon_id);
		Load_Armor (_armor_id);
		StartCoroutine (Initialise_UI ());
	}

	IEnumerator Initialise_UI(){
		while (t_ui_player_updater.player_updater == null){
			yield return new WaitForSeconds (0.25f);
		}
		Update_UI ();
	}

	public void Load_Weapon(int _weapon_id){
		//remove old weapon
		Remove_Weapon ();

		weapon_id = _weapon_id;
		//create new weapon
		player_weapon_object = Instantiate((GameObject)Resources.Load (item_codes.game_items [_weapon_id]));
		player_weapon_object.transform.parent = weapon_position.transform;
		player_weapon_object.transform.localPosition = weapon_position.transform.InverseTransformPoint (weapon_position.transform.position);
		player_weapon_script = player_weapon_object.GetComponent <t_weapon> ();

	}

	public void Remove_Weapon(){
		t_weapon[] weapons = GetComponentsInChildren <t_weapon> ();
		for(int i = 0; i < weapons.Length; i++){
			Destroy (weapons[i].gameObject);
		}
	}

	public void Load_Armor(int _armor_id){
		//remove old armor
		Remove_Armor ();

		armor_id = _armor_id;
		//create new armor
		player_armor_object = Instantiate ((GameObject)Resources.Load (item_codes.game_items [_armor_id]));
		player_armor_object.transform.parent = this.transform;
		player_armor_object.transform.localScale = this.transform.InverseTransformPoint (this.transform.position);
		player_armor_script = player_armor_object.GetComponent <t_armor> ();

		Update_UI ();
	}

	public void Remove_Armor(){
		t_armor[] armors = GetComponentsInChildren <t_armor> ();
		for(int i = 0; i < armors.Length; i++){
			Destroy (armors[i].gameObject);
		}
	}

	void Update_UI(){
		t_ui_player_updater.player_updater.Set_UI_Health (Get_Max_Health (), Get_Health ());
		t_ui_player_updater.player_updater.Set_UI_Teeth (Get_Teeth ());
		t_ui_player_updater.player_updater.Set_UI_Gold(Get_Gold ());
		t_ui_player_updater.player_updater.Set_UI_Shield (player_armor_script.Get_Armor_Defense_Amount ());
	}

	public override void Kill ()
	{
		//run player death animation
		Destroy (this.gameObject);
	}	

	void OnTriggerEnter2D(Collider2D _col){
		if(null != _col.gameObject.GetComponent <t_enemy_controller>()){
			dead_enemy = _col.gameObject.GetComponent <t_enemy_controller> ();
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(null != _col.gameObject.GetComponent <t_enemy_controller>()){
			dead_enemy = null;
		}
	}
}
