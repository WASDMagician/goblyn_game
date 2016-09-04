using UnityEngine;
using System.Collections;

public class t_player_movement : t_character_movement {

	// Use this for initialization
	void Start () {
		Initialise ();
	}
	
	// Update is called once per frame
	void Update () {
		Handle_User_Input ();
		Move ();
	}

	public void Handle_User_Input ()
	{
		if (t_player_states.Is_Free_Moving ()) {
			if (Input.GetKey (KeyCode.LeftArrow)) {
				character_direction = -1;
				character_last_direction = -1;
				Sprite_Direction_Check ();
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				character_direction = 1;
				character_last_direction = 1;
				Sprite_Direction_Check ();
			} else {
				character_direction = 0;
			}

			if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space)) {
				if (Player_Is_Grounded ()) {
					Jump ();
				}
			}
		}
		else{
			character_direction = 0;
		}
	}
}
