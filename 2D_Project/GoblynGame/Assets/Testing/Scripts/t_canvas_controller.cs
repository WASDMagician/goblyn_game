using UnityEngine;
using System.Collections;

public class t_canvas_controller : MonoBehaviour {

	public static t_canvas_controller canvas_controller;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);

		if(null == canvas_controller){
			canvas_controller = this;
		}
		else if(this != canvas_controller){
			Destroy (this.gameObject);
		}
	}
}
