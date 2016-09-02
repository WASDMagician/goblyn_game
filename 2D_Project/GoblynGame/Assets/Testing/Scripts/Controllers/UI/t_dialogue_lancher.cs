using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class t_dialogue_lancher : MonoBehaviour {

	public dialogue_box dialogue_text_object;

	private bool trigger_active = false;
	private bool dialogue_box_active = false;
	public bool require_key_input = false;
	public bool deactivate_on_keypress = false;
	
	// Update is called once per frame
	void Update () {
		if((true == require_key_input && true == trigger_active && true == t_player_states.Is_Free_Moving ()) ||
			true == t_player_states.Is_In_Shop ()){
			Handle_User_Input ();
		}
	}

	void Handle_User_Input(){
		if(Input.GetKeyDown (KeyCode.E)){
			if (true == deactivate_on_keypress && true == dialogue_box_active) {
				Deactivate_Dialogue_Box ();
			}
			Launch_Dialogue_box ();
		}

		if (true == t_player_states.Is_In_Shop () ||
			true == t_player_states.Is_In_Dialogue ()) {
			if (Input.GetKey (KeyCode.Escape)) {
				Deactivate_Dialogue_Box ();
			}
		}
	}

	void Launch_Dialogue_box(){
		dialogue_box_active = true;
		t_player_states.Set_In_Dialogue ();
		t_dialogue_box_controller.dialogue_box_controller.Activate ();
		t_dialogue_box_controller.dialogue_box_controller.Setup_Dialogue_Box (dialogue_text_object);
	}

	void Deactivate_Dialogue_Box(){
		dialogue_box_active = false;
		t_player_states.Set_Free_Moving ();
		t_dialogue_box_controller.dialogue_box_controller.Exit ();
	}

	void OnDestroy(){
		Deactivate_Dialogue_Box ();
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(null != _col.GetComponent<t_player_controller>()){
			trigger_active = true;
			if (false == require_key_input) {
				Launch_Dialogue_box ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(null != _col.GetComponent <t_player_controller>()){
			trigger_active = false;
			Deactivate_Dialogue_Box ();
		}
	}
}
