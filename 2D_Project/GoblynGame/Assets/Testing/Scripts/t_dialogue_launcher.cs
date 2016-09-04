using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class t_dialogue_launcher : MonoBehaviour {

	[SerializeField]
	private dialogue_box dialogue;
	[SerializeField]
	private bool require_key_input;
	private bool key_input_given;
	private bool collided_with_player;


	// Use this for initialization
	void Start () {
		GetComponent <BoxCollider2D>().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (true == collided_with_player && true == require_key_input) {
			Handle_User_Input ();
		}
		else if(true == collided_with_player && false == require_key_input){
			Launch_Dialogue ();
		}
	}

	void Handle_User_Input(){
		if (t_player_states.Is_Free_Moving ()) {
			if (Input.GetKeyDown (KeyCode.E)) {
				Launch_Dialogue ();
			}
		}
	}

	void Launch_Dialogue(){
		if(null != dialogue){
			t_ui_dialogue_controller.dialogue_controller.Enable_Dialogue (dialogue);
		}
		else{
			print ("No (scriptable object) dialogue assigned to this script or it cannot be accessed.");
		}
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(t_player_controller.player_controller.gameObject == _col.gameObject){
			collided_with_player = true;
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(t_player_controller.player_controller.gameObject == _col.gameObject){
			collided_with_player = false;
		}
	}
}
