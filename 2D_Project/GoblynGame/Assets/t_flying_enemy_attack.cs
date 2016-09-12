using UnityEngine;
using System.Collections;

public class t_flying_enemy_attack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Damage(){
		print ("Damage");
		r_player_controller.character_controller.Damage (10);
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(_col.gameObject == r_player_controller.character_controller.gameObject){
			Damage ();
		}
	}
}
