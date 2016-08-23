using UnityEngine;
using System.Collections;

public class t_dialogue_lancher : MonoBehaviour {

	public GameObject dialogue_ui_object;
	public dialogue_box dialogue_text_object;

	private bool trigger_active = false;
	public bool require_key_input = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(true == require_key_input && true == trigger_active){
			Handle_User_Input ();
		}
	}

	void Handle_User_Input(){
		if(Input.GetKeyDown (KeyCode.E)){
			Launch_Dialogue_box ();
		}
	}

	void Launch_Dialogue_box(){
		t_dialogue_box_controller.dialogue_box_controller.Activate ();
		t_dialogue_box_controller.dialogue_box_controller.Setup_Dialogue_Box (dialogue_text_object);
	}

	void Deactivate_Dialogue_Box(){
		t_dialogue_box_controller.dialogue_box_controller.Exit ();
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(_col.CompareTag ("Player")){
			trigger_active = true;
			if (false == require_key_input) {
				Launch_Dialogue_box ();
			}
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(_col.CompareTag ("Player")){
			trigger_active = false;
			Deactivate_Dialogue_Box ();
		}
	}
}
