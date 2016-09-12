using UnityEngine;
using System.Collections;

public class t_character_movement : MonoBehaviour {

	//components
	public Rigidbody2D character_rigidbody;
	private SpriteRenderer character_sprite;

	//setable player values
	[SerializeField]
	protected float character_speed;
	[SerializeField]
	protected float character_jump_speed;

	//private player variables
	protected int character_direction = 1;
	protected int character_last_direction = 1;

	//raycast values
	[SerializeField]
	protected LayerMask jumpable_layer;
	protected float cast_length;
	protected float player_width;
	protected Vector2 cast_offset;

	public virtual void Initialise(){
		character_rigidbody = GetComponent <Rigidbody2D> ();

		player_width = GetComponent <BoxCollider2D> ().bounds.extents.x;

		//the following may not be required for enemies
		//player uses them to cast down to check for collision
		cast_length = GetComponent <BoxCollider2D> ().bounds.extents.y / 4; //4 is magic number, not sure why
		cast_offset = new Vector2 (player_width, 0); //used to cast on left and right of player
	}

	/// <summary>
	/// Compares signs (+/-) of sprite to decide whether it should be flipped to match direction
	/// </summary>
	protected void Sprite_Direction_Check(){
		if(Mathf.Sign (this.transform.localScale.x) != Mathf.Sign (character_last_direction)){
			Vector3 scale = this.transform.localScale;
			scale.x *= -1;
			this.transform.localScale = scale;
		}
	}
		
	protected void Move(){
		Vector2 velocity = character_rigidbody.velocity;
		velocity.x = character_direction * character_speed;
		character_rigidbody.velocity = velocity;
	}
		
	protected void Jump(){
		character_rigidbody.AddForce (Vector2.up * character_jump_speed, ForceMode2D.Impulse);
	}

	/// <summary>
	/// Cast down on left, center and right of player to check for ground collision
	/// Makes use of private function to do actual casting.
	/// </summary>
	/// <returns><c>true</c>, if is grounded was playered, <c>false</c> otherwise.</returns>
	public bool Player_Is_Grounded(){
		if (Cast_Down (Vector2.zero)){
			return true;
		}
		else if(Cast_Down (-cast_offset)){
			return true;
		}
		else if(Cast_Down (cast_offset)){
			return true;
		}
		else{
			return false;
		}
	}

	/// <summary>
	/// Casting implementation for Player_Is_Grounded
	/// </summary>
	/// <returns><c>true</c>, if down was cast, <c>false</c> otherwise.</returns>
	/// <param name="cast_offset">Cast offset.</param>
	private bool Cast_Down(Vector2 cast_offset){
		RaycastHit2D hit = Physics2D.Raycast (this.transform.position + (Vector3)cast_offset, -this.transform.up, cast_length, jumpable_layer);
		if(hit){
			return true;
		}
		return false;
	}
}
