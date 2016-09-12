using UnityEngine;
using System.Collections;

public class t_player_movement : t_character_movement {

	// Use this for initialization
	void Start () {
		Initialise ();
	}

	public void Move_Left(){
		character_direction = -1;
		character_last_direction = -1;
		Sprite_Direction_Check ();
		Move ();
	}

	public void Move_Right(){
		character_direction = 1;
		character_last_direction = 1;
		Sprite_Direction_Check ();
		Move ();
	}

	public void Stop(){
		Vector2 current_velocity = character_rigidbody.velocity;
		current_velocity.x = 0;
		//character_rigidbody.velocity = Vector2.zero;
		character_rigidbody.velocity = current_velocity;
		character_direction = 0;
	}

	public void Player_Jump(){
		if(Player_Is_Grounded ()){
			Jump ();
		}
	}
}
