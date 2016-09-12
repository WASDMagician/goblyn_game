using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class t_ui_on_click_reroute : MonoBehaviour {

	private Button button;

	// Use this for initialization
	void Start () {
		Get_Button ();
	}

	void Get_Button(){
		button = GetComponent <Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		Handle_User_Input ();
	}

	void Handle_User_Input(){
		if(Input.GetKeyDown (KeyCode.E)){
			if(null == button){
				Get_Button ();
			}
			else if(null != button){
				if(EventSystem.current.currentSelectedGameObject.GetComponent <Button>() == button){
					PointerEventData pointer = new PointerEventData (EventSystem.current);
					ExecuteEvents.Execute (button.gameObject, pointer, ExecuteEvents.submitHandler);
				}
			}
		}
	}

	void Click_Button(){
		
	}
}
