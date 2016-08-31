using UnityEngine;
using System.Collections;

public class t_ui_shop_visibility_controller : MonoBehaviour {

	public static t_ui_shop_visibility_controller shop_visibility_controller;

	public bool shop_active;

	void Start(){
		if(null == shop_visibility_controller){
			shop_visibility_controller = this;
		}
		else if(shop_visibility_controller != this){
			Destroy (this.gameObject);
		}
	}

	public void Enable_Shop(){
		this.transform.GetChild (0).gameObject.SetActive (true);
		shop_active = true;
	}

	public void Disable_Shop(){
		this.transform.GetChild (0).gameObject.SetActive (false);
		shop_active = false;
	}
}
