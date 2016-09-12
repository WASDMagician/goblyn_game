using UnityEngine;
using System.Collections;

public class t_horizontal_platform_controller : MonoBehaviour {

	t_spline_walker spline_walker;
	bool move_player_with_platform = false;

	void Start(){
		spline_walker = GetComponentInParent <t_spline_walker> ();
	}

	void Update(){
		if(true == move_player_with_platform){
			Move_Player_With_Platform ();
		}
	}

	void Move_Player_With_Platform(){
	}

	void OnCollisionEnter2D(Collision2D _col){
		if(_col.gameObject == r_player_controller.character_controller.gameObject){
			move_player_with_platform = true;
		}
	}

	void OnCollisionExit2D(Collision2D _col){
		if(_col.gameObject == r_player_controller.character_controller.gameObject){
			move_player_with_platform = false;
		}
	}
}
