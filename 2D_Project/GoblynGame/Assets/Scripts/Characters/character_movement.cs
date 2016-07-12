using UnityEngine;
using System.Collections;

public class character_movement : MonoBehaviour {
	Rigidbody2D rigidbody;
	BoxCollider2D collider;

	float current_x_speed;
	public float max_speed;
	public float acceleration;
	public float deceleration;

	public float jump_speed;

	int direction;
	int look_direction; //not quite happy having 2 directions but... yeah.

	public LayerMask ground_layer;

	void Start(){
		rigidbody = GetComponent <Rigidbody2D> ();
		collider = GetComponent <BoxCollider2D> ();
	}

	void Update(){
		Move ();
	}

	public void Set_Direction(int _direction){
		direction = _direction;
		Flip_And_Position_Sprite (_direction);
	}

	void Flip_And_Position_Sprite(int _direction){
		if (_direction != 0 && _direction != look_direction) {

			look_direction = _direction;
			Vector3 new_scale = this.transform.localScale;

			if (look_direction == -1) {
				new_scale.x = -Mathf.Abs (new_scale.x);
			}
			else{
				new_scale.x = Mathf.Abs (new_scale.x);
			}
			this.transform.localScale = new_scale;
		}
	}

	public void Jump(){
		if(Is_Grounded ()){
			Vector2 velocity = rigidbody.velocity;
			velocity.y = jump_speed;
			rigidbody.velocity = velocity;
		}
	}

	void Move(){
		Calculate_X_Speed ();
		Vector2 current_velocity = rigidbody.velocity;
		current_velocity.x = current_x_speed;
		rigidbody.velocity = current_velocity;
	}

	void Calculate_X_Speed(){
		if(direction == 1){
			current_x_speed = Mathf.Lerp (current_x_speed, max_speed, acceleration);
		}
		else if(direction == -1){
			current_x_speed = Mathf.Lerp (current_x_speed, -max_speed, acceleration);

		}
		else{
			current_x_speed = Mathf.Lerp (current_x_speed, 0, deceleration);
		}
	}

	bool Is_Grounded(){
		return(Physics2D.OverlapCircle (this.transform.position, 1f, ground_layer));
	}


}
