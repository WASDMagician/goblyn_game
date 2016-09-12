using UnityEngine;
using System.Collections;

public class t_press_any_button : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Handle_User_Input ();
	}

	void Handle_User_Input(){
		if(Input.anyKeyDown){
			t_main_menu_controller.main_menu_controller.Activate ();
			this.gameObject.SetActive (false);
		}
	}
}
