using UnityEngine;
using System.Collections;

public class t_player_controller : t_character_controller {

	public static t_player_controller player_controller;

	//weapon values and components
	[SerializeField]
	private int weapon_id;
	private GameObject weapon_position;
	public GameObject player_weapon_object;
	public t_weapon player_weapon_script;

	//armor values and components
	[SerializeField]
	private int armor_id = 11;
	public GameObject player_armor_object;
	public t_armor player_armor_script;

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
		if(Input.GetKeyDown (KeyCode.Escape)){
			t_pause_menu_controller.pause_controller.Toggle_Pause ();
		}

		if(Input.GetKeyDown (KeyCode.A)){
			if(null != player_weapon_script){
				player_weapon_script.Attack ();
			}
		}

		if(Input.GetKeyDown (KeyCode.D)){
			Load_Armor (11);
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
		Update_All_UI_Elements ();
	}

	public void Load_Weapon(int _weapon_id){
		//remove old weapon
		Remove_Weapon ();

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

		//create new armor
		player_armor_object = Instantiate ((GameObject)Resources.Load (item_codes.game_items [_armor_id]));
		player_armor_object.transform.parent = this.transform;
		player_armor_object.transform.localScale = this.transform.InverseTransformPoint (this.transform.position);
		player_armor_script = player_armor_object.GetComponent <t_armor> ();

		Update_UI_Defense ();
	}

	public void Remove_Armor(){
		t_armor[] armors = GetComponentsInChildren <t_armor> ();
		for(int i = 0; i < armors.Length; i++){
			Destroy (armors[i].gameObject);
		}
	}
		
	public int Get_Weapon_ID(){
		return weapon_id;
	}

	public int Get_Armor_ID(){
		return armor_id;
	}

	public void Update_All_UI_Elements(){
		Update_UI_Health ();
		Update_UI_Gold ();
		Update_UI_Teeth ();
		Update_UI_Defense ();
	}

	void Update_UI_Health(){
		try{
			t_ui_player_updater.player_updater.Set_UI_Health (Get_Max_Health (), Get_Health ());
		}
		catch(System.Exception e){
			Debug.Log (e.ToString ());
		}

		Update_UI_Defense ();
	}

	void Update_UI_Defense(){
		try{
			t_ui_player_updater.player_updater.Set_UI_Shield (player_armor_script.Get_Armor_Defense_Amount ());
		}
		catch(System.Exception e){
			Debug.Log (e.ToString ());
		}
	}

	void Update_UI_Gold(){
		try{
			t_ui_player_updater.player_updater.Set_UI_Gold(Get_Gold ());
		}
		catch(System.Exception e){
			Debug.Log (e.ToString ());
		}
	}

	void Update_UI_Teeth(){
		try{
			t_ui_player_updater.player_updater.Set_UI_Teeth (Get_Teeth ());
		}
		catch(System.Exception e){
			Debug.Log (e.ToString ());
		}
	}
	

	//override character_controller functions
	public override void Set_Gold (int _gold)
	{
		base.Set_Gold (_gold);
		Update_UI_Gold ();
	}

	public override void Set_Health (int _health)
	{
		base.Set_Health (_health);
		Update_UI_Health ();
	}

	public override void Set_Teeth (int _teeth)
	{
		base.Set_Teeth (_teeth);
		Update_UI_Teeth ();
	}

	public override void Damage (int _damage_amount)
	{
		base.Damage (_damage_amount);
		Update_UI_Health (); //may be unnecesarry with call to set_health
	}

	public override void Kill ()
	{
		//run player death animation
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(_col.GetComponent<t_pickup>() != null){
			_col.GetComponent <t_pickup> ().Set_Collided_With_Player (true);
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(_col.GetComponent <t_pickup>() != null){
			_col.GetComponent <t_pickup>().Set_Collided_With_Player (false);
		}
	}
	
}
