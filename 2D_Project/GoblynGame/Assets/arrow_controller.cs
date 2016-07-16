using UnityEngine;
using System.Collections;
using System;

public class arrow_controller : MonoBehaviour {

	public bool active;
	public float arrow_speed;
	public int damage;
	public Rigidbody2D rigidbody;
	public Vector2 vel;
	int direction;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent <Rigidbody2D> ();
	}

	void Update(){
			if(active){
			Vector2 velocity = rigidbody.velocity;
			velocity = new Vector2 (arrow_speed * direction, 0);
			vel = velocity;
			rigidbody.velocity = velocity;
		}
	}

	public void Set_Active(bool _active, int _direction){
		active = _active;
		direction = _direction;
	}

	void OnCollisionEnter2D(Collision2D _col){
		print ("Collided");
		if (_col.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			_col.gameObject.GetComponent <character_controller> ().Remove_Health (damage);
		}
		Destroy (this.gameObject);
	}
}
