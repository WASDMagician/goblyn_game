using UnityEngine;
using System.Collections;

public class t_pause_menu_controller : MonoBehaviour {

	public static t_pause_menu_controller pause_controller;

	public static bool paused = false;

	// Use this for initialization
	void Start () {
		if(pause_controller == null){
			pause_controller = this;
		}
		else if(pause_controller != this){
			Destroy (gameObject);
		}
	}

	public void Toggle_Pause(){
		if(false == paused){
			t_player_states.Is_In_Menu ();
			Time.timeScale = 0;
			paused = true;
			t_pause_menu_controller.pause_controller.Enable_Menu ();
			t_player_states.Set_In_Menu ();
		}
		else if(true == paused){
			t_player_states.Is_Free_Moving ();
			Time.timeScale = 1;
			paused = false;
			t_pause_menu_controller.pause_controller.Disable_Menu ();
			t_player_states.Set_Free_Moving ();
		}
	}

	public void Enable_Menu(){
		t_player_states.Is_In_Menu ();
		this.transform.GetChild (0).gameObject.SetActive (true);
	}

	public void Disable_Menu(){
		t_player_states.Is_Free_Moving ();
		this.transform.GetChild (0).gameObject.SetActive (false);
	}
}
