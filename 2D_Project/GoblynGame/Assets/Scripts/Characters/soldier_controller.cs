using UnityEngine;
using System.Collections;

public class soldier_controller : character_controller {

	GameObject player_object;
	public float player_chase_range;
	public float character_attack_range;
	public float attack_delay;

	public float attack_level; //score at which the AI will notice that the player is disguised
	bool alive;
	

	// Use this for initialization
	void Start () {
		alive = true;
		player_object = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (alive == true) {
			if (attack_level >= player_object.GetComponent <armor_controller> ().hiddenness) { //drop this into the if statement below
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
	}

	IEnumerator Attack_Player(){
		yield return new WaitForSeconds (attack_delay);
		StartCoroutine (weapon.Start_Attack ());
	}
}
