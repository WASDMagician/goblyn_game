using UnityEngine;
using System.Collections;

public class t_enemy_controller : t_character_controller{

	protected t_enemy_health_bar health_bar;

	protected GameObject enemy_armor_object;
	protected t_armor enemy_armor_script;

	protected GameObject enemy_weapon_object;
	protected t_weapon enemy_weapon_script;

	protected t_enemy_movement_script enemy_movement_script;

	[SerializeField]
	float enemy_attack_range;

	private bool gold_looted = false;
	private bool teeth_looted = false;
	private bool weapon_looted = false;
	private bool armor_looted = false;

	// Use this for initialization
	void Start () {
		health_bar = GetComponent <t_enemy_health_bar> ();
		enemy_armor_object = this.transform.GetChild (2).gameObject;
		enemy_armor_script = enemy_armor_object.GetComponent <t_armor> ();

		enemy_weapon_script = GetComponentInChildren <t_weapon> ();
		enemy_weapon_object = enemy_weapon_script.gameObject;

		enemy_movement_script = GetComponent <t_enemy_movement_script> ();
	}
	
	// Update is called once per frame
	void Update () {
		health_bar.Update_Health_Bar (); //updates health bar *position* rename it.
		if (true == is_alive) {
			if (true == Player_In_Attack_Distance ()) { //refactor to not happen each frame?
				print ("ATTACK");
				enemy_weapon_script.Attack ();
			}
		}
	}

	bool Player_In_Attack_Distance(){
		if (r_player_controller.character_controller != null) {
			if (Vector3.Distance (enemy_weapon_object.transform.position, r_player_controller.character_controller.transform.position) <
			  enemy_weapon_script.Get_Weapon_Range ()) {
				return true;
			}
		}
		return false;
	}

	public override void Damage (int _damage_amount)
	{
		if (null != enemy_armor_script) {
			base.Damage (_damage_amount - enemy_armor_script.Get_Armor_Defense_Amount ());
		}
		else{
			base.Damage (_damage_amount);
		}

		health_bar.Update_Health_Bar ();
	}

	public override void Kill ()
	{
		//play death animation
		Set_Alive (false);
		GetComponent <t_enemy_movement_script>().Stop_Movement ();
		GetComponent <Collider2D>().isTrigger = true;
		GetComponent <Rigidbody2D>().isKinematic = true;
		this.transform.Rotate (new Vector3(0, 0, -90));
		//Destroy (this.gameObject);
	}

	public void Loot(){
		if(false == gold_looted){
			r_player_controller.character_controller.Add_Gold (Get_Gold ());
			string message = "Looted " + Get_Gold ().ToString () + " gold.\nNext item: " + Get_Teeth ().ToString () + " teeth.";
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
			t_ui_warning_box.warning_box.Set_Warning_Message (message);
			gold_looted = true;

		}
		else if(false == teeth_looted){
			r_player_controller.character_controller.Add_Teeth (Get_Teeth ());
			string message = "Looted " + Get_Teeth ().ToString () + " teeth.\nNext item: " + item_codes.game_item_names[weapon_id];
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
			t_ui_warning_box.warning_box.Set_Warning_Message (message);
			teeth_looted = true;
		}
		else if(false == weapon_looted){
			r_player_controller.character_controller.Set_Weapon (weapon_id);
			string message = "Looted: " + item_codes.game_item_names [weapon_id] + ".\nNext item: " + item_codes.game_item_names [armor_id];
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
			t_ui_warning_box.warning_box.Set_Warning_Message (message);
			weapon_looted = true;
		}
		else if(false == armor_looted){
			r_player_controller.character_controller.Set_Armor (armor_id);
			string message = "Looted: " + item_codes.game_item_names [armor_id] + ".\nNo more items to loot.";
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
			t_ui_warning_box.warning_box.Set_Warning_Message ("Looted armor");
			armor_looted = true;
		}
		else{
			//nothing left to loot
		}
	}
}
