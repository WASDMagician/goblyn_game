using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class t_main_menu_controller : MonoBehaviour {

	public static t_main_menu_controller main_menu_controller;

	// Use this for initialization
	void Start () {
		if(null == main_menu_controller){
			main_menu_controller = this;
		}
		else if(this != main_menu_controller){
			Destroy (this.gameObject);
		}
	}

	public void Activate(){
		this.transform.GetChild (0).gameObject.SetActive (true);
		EventSystem.current.SetSelectedGameObject (this.transform.GetChild (0).transform.GetChild (0).gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
