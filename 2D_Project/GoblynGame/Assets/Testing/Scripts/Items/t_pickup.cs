using UnityEngine;
using System.Collections;

public abstract class t_pickup : MonoBehaviour {
	[SerializeField]
	protected bool requires_key_input = false;
	protected bool key_input_given = false;
	protected bool collided_with_player = false;

	public void Set_Collided_With_Player(bool _collided){
		collided_with_player = _collided;
	}

	void Update(){
		if((false == requires_key_input && true == collided_with_player) || true == key_input_given){
			Pickup_Item ();
		}
		else{
			Handle_User_Input ();
		}
	}

	void Handle_User_Input(){
		if(Input.GetKeyDown (KeyCode.E)){
			key_input_given = true;
		}
	}

	public abstract void Pickup_Item ();


}
