using UnityEngine;
using System.Collections;

public class r_menu_load_save_controller : MonoBehaviour {
	
	public void Load(){
		if (load_save_game.load_save_controller != null) {
			load_save_game.load_save_controller.Load ();
		}
	}

	public void Save(){
		if(load_save_game.load_save_controller != null){
			load_save_game.load_save_controller.Save ();
		}
	}
}
