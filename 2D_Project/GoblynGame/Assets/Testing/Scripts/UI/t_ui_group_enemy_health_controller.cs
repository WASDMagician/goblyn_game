using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class t_ui_group_enemy_health_controller : MonoBehaviour {

	public static t_ui_group_enemy_health_controller enemy_health_group_controller;

	// Use this for initialization
	void Start () {
		if(null == enemy_health_group_controller){
			enemy_health_group_controller = this;
		}
		else if(this != enemy_health_group_controller){
			Destroy (this.gameObject);
		}
	}
}
