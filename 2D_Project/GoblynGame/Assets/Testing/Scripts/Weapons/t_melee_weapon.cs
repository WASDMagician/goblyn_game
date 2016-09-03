using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class t_melee_weapon : t_weapon {

	protected CircleCollider2D weapon_range_collider;
	protected List<t_character_controller> enemies_in_range;

	protected float damage_start_time;
	void Start(){
		Initialise ();

	}

	protected override void Initialise ()
	{
		base.Initialise ();
		enemies_in_range = new List<t_character_controller> ();
		weapon_range_collider = GetComponent <CircleCollider2D> ();
		weapon_range_collider.radius = weapon_range;
		weapon_range_collider.isTrigger = true;
	}

	protected void Damage_Enemies(){
		for(int i = 0; i < enemies_in_range.Count; i++){
			//add a check for direction here
			//if (enemies_in_range [i] != own_controller) { //added collider ignore, check if works, remove code here if so
				if (!enemies_in_range [i].Is_Invulnerable ()) {
					enemies_in_range [i].Damage (weapon_damage_amount);
					StartCoroutine (enemies_in_range [i].Make_Invulnerable (weapon_animation_damage_time + weapon_animation_ignore_end_time)); //needs offsetting by current time
				}
			//}
		}
	}

	protected override IEnumerator Attack_Start (){
		damage_start_time = Time.time;
		while(Time.time < damage_start_time + weapon_animation_damage_time){
			Damage_Enemies ();
			yield return new WaitForSeconds (0);
		}
		StartCoroutine (Attack_End ());
	}
	

	protected virtual void OnTriggerEnter2D(Collider2D _col){
		if(_col.GetComponent <t_character_controller>() != null){
			enemies_in_range.Add (_col.GetComponent <t_character_controller> ());
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D _col){
		if(_col.GetComponent <t_character_controller>() != null){
			enemies_in_range.Remove (_col.GetComponent <t_character_controller>());
		}
	}
}
