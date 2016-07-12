using UnityEngine;
using System.Collections;

public class trigger_text_box : MonoBehaviour {

	textbox_manager connected_manager;

	// Use this for initialization
	void Start () {
		connected_manager = GetComponent <textbox_manager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D _col){
		if (_col.gameObject.tag == "Player") {
			connected_manager.Activate ();
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if (_col.gameObject.tag == "Player") {
			connected_manager.Deactivate ();
		}
	}
}
