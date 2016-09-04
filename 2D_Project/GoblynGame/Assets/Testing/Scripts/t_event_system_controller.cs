using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class t_event_system_controller : MonoBehaviour {

	public static t_event_system_controller event_controller;
	private EventSystem event_system;

	// Use this for initialization
	void Start () {
		if(null == event_controller){
			event_controller = this;
		}
		else if(this != event_controller){
			Destroy (this.gameObject);
		}
		event_system = GetComponent <EventSystem> ();
	}
	
	public void Set_First_Selected(GameObject _button){
		if (null == _button.GetComponent <Button> ()) {
			print ("Gameobject passed to Set_First_Selected is not a button");
		} else {
			event_system.SetSelectedGameObject (_button);
		}
	}
}
