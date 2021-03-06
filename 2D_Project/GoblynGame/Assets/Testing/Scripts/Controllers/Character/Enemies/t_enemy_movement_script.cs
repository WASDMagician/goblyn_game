﻿using UnityEngine;
using System.Collections;

public class t_enemy_movement_script : t_character_movement {

	[SerializeField]
	protected float enemy_chase_distance;
	[SerializeField]
	protected float patrol_distance_from_start;

	t_enemy_controller enemy_controller;

	private Vector3 left_limit;
	private Vector3 right_limit;

	private int movement_direction = 1;

	// Use this for initialization
	void Start () {
		Initialise ();
		left_limit = this.transform.position;
		left_limit.x -= patrol_distance_from_start;

		right_limit = this.transform.position;
		right_limit.x += patrol_distance_from_start;
		enemy_controller = GetComponent <t_enemy_controller> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (true == enemy_controller.Is_Alive ()) {
			Movement_Decider ();
			Move ();
		}
	}

	void Movement_Decider(){
		if(null != r_player_controller.character_controller){
			float player_distance = Vector3.Distance (this.transform.position, r_player_controller.character_controller.transform.position);
			if(player_distance < enemy_chase_distance){
				Chase_Player ();
			}
			else{
				Patrol ();
			}
		}
		else{
			Patrol ();
		}
	}

	void Chase_Player(){
		character_direction = Get_Player_Direction ();
		character_last_direction = character_direction;
		Sprite_Direction_Check ();
	}

	void Patrol(){
		if (movement_direction == -1) {
			if (this.transform.position.x < left_limit.x) {
				movement_direction = 1;
				character_direction = 1;
				character_last_direction = character_direction;
				Sprite_Direction_Check ();
			} 
		} else if (movement_direction == 1) {
			if (this.transform.position.x > right_limit.x) {
				movement_direction = -1;
				character_direction = -1;
				character_last_direction = character_direction;
				Sprite_Direction_Check ();
			} 
		}
	}

	public void Stop_Movement(){
		character_rigidbody.velocity = Vector2.zero;
	}


	int Get_Player_Direction(){
		if(r_player_controller.character_controller.transform.position.x < this.transform.position.x){
			return -1;
		}
		else{
			return 1;
		}
	}

	bool Can_See_Player(){
		return !Physics2D.Linecast ((Vector2)this.transform.position, (Vector2)r_player_controller.character_controller.transform.position);
	}
}
