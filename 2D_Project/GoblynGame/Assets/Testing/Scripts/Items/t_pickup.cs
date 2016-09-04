using UnityEngine;
using System.Collections;

public class t_pickup : MonoBehaviour {
	[SerializeField]
	protected bool requires_key_input = false;
	protected bool key_input_given = false;
	protected bool collided_with_player = false;

	void Update(){
		if(true == collided_with_player){
			Pickup_Item ();
		}
	}

	public virtual void Pickup_Item (){}

	void OnTriggerEnter2D(Collider2D _col){
		if(_col.gameObject == t_player_controller.player_controller.gameObject){
			collided_with_player = true;
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(_col.gameObject == t_player_controller.player_controller.gameObject){
			collided_with_player = false;
		}
	}


}
