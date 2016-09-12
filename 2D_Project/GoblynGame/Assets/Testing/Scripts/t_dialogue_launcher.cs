using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class t_dialogue_launcher : MonoBehaviour {

	[SerializeField]
	private dialogue_box dialogue;
	[SerializeField]
	private bool require_key_input;
	private bool key_input_given;


	// Use this for initialization
	void Start () {
		GetComponent <BoxCollider2D>().isTrigger = true;
	}

	public void Launch_Dialogue(){
		if(null != dialogue){
			t_ui_dialogue_controller.dialogue_controller.Enable_Dialogue (dialogue);
		}
		else{
			print ("No (scriptable object) dialogue assigned to this script or it cannot be accessed.");
		}
	}
}
