using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class t_projectile : MonoBehaviour {

	[SerializeField]
	protected float projectile_speed;
	protected Rigidbody2D projectile_rigidbody;
	protected int projectile_damage;
	protected SpriteRenderer sprite_renderer;

	// Use this for initialization
	void Start () {
		projectile_rigidbody = GetComponent <Rigidbody2D> ();
		sprite_renderer = GetComponent <SpriteRenderer> ();
	}

	void Update(){
		Destroy_Off_Screen ();
	}

	void Destroy_Off_Screen(){
		Vector2 projectile_position_in_camera = Camera.main.WorldToViewportPoint (this.transform.position);
		if (projectile_position_in_camera.x > 1 || projectile_position_in_camera.x < 0 ||
		   projectile_position_in_camera.y > 1 || projectile_position_in_camera.y < 0) {
			Destroy_Projectile ();
		}
	}

	public void Launch(float _x_direction, int _damage){
		int x_direction = 0;

		if(_x_direction < 0){
			x_direction = -1;
		}
		else {
			x_direction = 1;
		}

		Vector3 direction_scale = this.transform.localScale;
		direction_scale.x = x_direction;
		this.transform.localScale = direction_scale;

		projectile_damage = _damage;
		if(null == projectile_rigidbody){
			projectile_rigidbody = GetComponent <Rigidbody2D> ();
		}
		if (null != projectile_rigidbody) {
			projectile_rigidbody.velocity = new Vector2 (projectile_speed * x_direction, 0);
		}
	}

	void Damage_Character(t_character_controller _character){
		_character.Set_Health (_character.Get_Health () - projectile_damage);
		Destroy_Projectile ();
	}

	protected virtual void Destroy_Projectile(){
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(null != _col.gameObject.GetComponent <t_character_controller>()){
			Damage_Character (_col.GetComponent <t_character_controller>());
		}
	}
}
