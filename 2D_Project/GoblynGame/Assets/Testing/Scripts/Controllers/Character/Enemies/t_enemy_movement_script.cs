using UnityEngine;
using System.Collections;

public class t_enemy_movement_script : t_character_movement {

	[SerializeField]
	protected float enemy_chase_distance;

	// Use this for initialization
	void Start () {
		Initialise ();
	}
	
	// Update is called once per frame
	void Update () {
		Movement_Decider ();
		Move ();
	}

	void Movement_Decider(){
		float player_distance = Vector3.Distance (this.transform.position, t_player_controller.player_controller.transform.position);
		if(player_distance < enemy_chase_distance){
			character_direction = Get_Player_Direction ();
			character_last_direction = character_direction;
			Sprite_Direction_Check ();
		}
		else{
			character_direction = 0;
		}
	}



	int Get_Player_Direction(){
		if(t_player_controller.player_controller.transform.position.x < this.transform.position.x){
			return -1;
		}
		else{
			return 1;
		}
	}

	bool Can_See_Player(){
		return !Physics2D.Linecast ((Vector2)this.transform.position, (Vector2)t_player_controller.player_controller.transform.position);
	}
}
