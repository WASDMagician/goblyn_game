using UnityEngine;
using System.Collections;

public class enemy_controller : character_controller {

	GameObject player_object;
	public float player_chase_range;
	public float character_attack_range;
	public float attack_delay;

	public float attack_level; //score at which the AI will notice that the player is disguised
	public bool alive;

	public GameObject looting_textbox_prefab;

	public GameObject weapon_drop_prefab;
	

	// Use this for initialization
	void Start () {
		alive = true;
		player_object = GameObject.FindGameObjectWithTag ("Player");
		weapon = GetComponentInChildren <weapon_controller> ();
		armor = GetComponentInChildren <armor_controller> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (alive == true) {
			if (attack_level > player_object.GetComponent <armor_controller> ().hiddenness) {
				float distance = Vector3.Distance (this.transform.position, player_object.transform.position);
				if (distance < player_chase_range && distance > character_attack_range) {
					Chase_Player ();
				} else if (distance < character_attack_range) { //can this be collapsed into 1?
					movement.Set_Direction (0);
					StartCoroutine (Attack_Player ());
				} else {
					movement.Set_Direction (0);
				}
			}
		}
	}

	void Chase_Player(){
		if(player_object.transform.position.x < this.transform.position.x ){
			movement.Set_Direction (-1);
		}
		else{
			movement.Set_Direction (1);
		}
	}

	public override void Die ()
	{
		alive = false;
		this.transform.Rotate (new Vector3(0, 0, 90));
		GetComponent <Collider2D>().isTrigger = true;
		GetComponent <Rigidbody2D>().velocity = new Vector2(0, 0);
		GetComponent <Rigidbody2D>().isKinematic = true;

		GameObject loot_text = Instantiate (looting_textbox_prefab, this.transform.position, Quaternion.identity) as GameObject;
		loot_text.transform.parent = this.transform;
		GameObject weapon_drop = Instantiate (weapon_drop_prefab, this.transform.position, Quaternion.identity) as GameObject;
		weapon_controller weapon_drop_weapon = weapon_drop.GetComponent <weapon_controller> ();

		weapon_drop_weapon.Set_Weapon (weapon.Get_Weapon ());
		weapon_drop.GetComponent <Rigidbody2D>().AddForce (new Vector2(-(movement.Get_direction () * 5), 5), ForceMode2D.Impulse);
	}

	public armor_controller.armor_type Get_Armor(){
		return armor.Get_Armor ();
	}

	IEnumerator Attack_Player(){
		yield return new WaitForSeconds (attack_delay);
		StartCoroutine (weapon.Start_Attack ());
	}
}
