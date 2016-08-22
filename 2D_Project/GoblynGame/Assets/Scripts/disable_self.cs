using UnityEngine;
using System.Collections;

public class disable_self : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Handle_User_Input ();
	}

	void Handle_User_Input(){
		if(Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.E)){
			this.gameObject.SetActive (false);
		}
	}
}
