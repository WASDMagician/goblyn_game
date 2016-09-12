using UnityEngine;
using System.Collections;

public class r_menu_controller : MonoBehaviour {

	public static r_menu_controller menu_controller;
	private bool is_active;

	// Use this for initialization
	void Start () {
		if(null == menu_controller){
			menu_controller = this;
		}
		else if(this != menu_controller){
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(true == is_active){
			Handle_User_Input ();
		}
	}

	void Handle_User_Input(){
		if(Input.GetKeyDown (r_input_keys.menu)){
			StartCoroutine(Deactivate (0.1f)); //delay to prevent opening and closing menu immediately
		}
	}

	public void Activate(){
		if(null != r_player_controller.character_controller && 
			r_player_controller.character_controller.state != r_player_controller.states.menu){
			r_player_controller.character_controller.Switch_States (r_player_controller.states.menu);
		}
		is_active = true;
		this.transform.GetChild (0).gameObject.SetActive (true);
	}

	public IEnumerator Deactivate(float _delay){
		yield return new WaitForSeconds (_delay);
		is_active = false;
		this.transform.GetChild (0).gameObject.SetActive (false);
		r_player_controller.character_controller.Switch_States (r_player_controller.character_controller.last_state);
	}
}
