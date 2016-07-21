using UnityEngine;
using System.Collections;

public class weapon_drop_controller : MonoBehaviour {

	bool is_active;
	weapon_controller weapon;
	player_controller player;
	BoxCollider2D collider;
	Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
		weapon = GetComponent <weapon_controller> ();
		collider = GetComponent <BoxCollider2D> ();
		rigidbody = GetComponent <Rigidbody2D> ();
		Resize_Bounds ();
	}

	void Resize_Bounds(){
		collider.size = new Vector2 (collider.size.x * 2, collider.size.y * 2);
	}

	void Update(){
		if(is_active){
			Handle_User_Input ();
		}
	}

	void Handle_User_Input(){
		if(Input.GetKeyUp (KeyCode.E)){
			if(weapon != null){
				player.Set_Weapon (weapon.Get_Weapon ());
				GetComponent <textbox_manager>().Deactivate ();
				Destroy (this.gameObject);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D _col){
		if(_col.gameObject.layer == LayerMask.NameToLayer ("Ground")){
			rigidbody.isKinematic = true;
			collider.isTrigger = true;
		}
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(_col.CompareTag ("Player")){
			is_active = true;
			player = _col.GetComponent <player_controller> ();
		}
	}
}
