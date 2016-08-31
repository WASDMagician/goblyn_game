using UnityEngine;
using System.Collections;

public class t_enemy_controller : t_character_controller{

	protected t_enemy_health_bar health_bar;

	protected GameObject enemy_armor_object;
	protected t_armor enemy_armor_script;

	protected GameObject enemy_weapon_object;
	protected t_weapon enemy_weapon_script;

	[SerializeField]
	float enemy_attack_range;

	// Use this for initialization
	void Start () {
		health_bar = GetComponent <t_enemy_health_bar> ();
		enemy_armor_object = this.transform.GetChild (2).gameObject;
		enemy_armor_script = enemy_armor_object.GetComponent <t_armor> ();

		enemy_weapon_script = GetComponentInChildren <t_weapon> ();
		enemy_weapon_object = enemy_weapon_script.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		health_bar.Update_Health_Bar (); //updates health bar *position* rename it.
		if(true == Player_In_Attack_Distance ()){ //refactor to not happen each frame?
			enemy_weapon_script.Attack ();
		}
	}

	bool Player_In_Attack_Distance(){
		if (t_player_controller.player_controller != null) {
			if (Vector3.Distance (enemy_weapon_object.transform.position, t_player_controller.player_controller.transform.position) <
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
		Destroy (this.gameObject);
	}
}
