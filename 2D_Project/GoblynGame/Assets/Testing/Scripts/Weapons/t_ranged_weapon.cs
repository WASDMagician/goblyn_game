using UnityEngine;
using System.Collections;

public class t_ranged_weapon : t_weapon {

	[SerializeField]
	protected GameObject projectile_prefab;
	private GameObject projectile;

	// Use this for initialization
	void Start () {
		Initialise ();
	}

	protected override void Initialise ()
	{
		base.Initialise ();
	}
	
	protected override IEnumerator Attack_Start (){
		yield return new WaitForSeconds (weapon_animation_ignore_start_time);
		Launch_Projectile ();
		StartCoroutine (Attack_End ());
	}

	void Launch_Projectile(){
		projectile = Instantiate (projectile_prefab, own_controller.transform.GetChild (0).position, Quaternion.identity) as GameObject;
		Physics2D.IgnoreCollision (projectile.GetComponent <Collider2D>(), own_controller.gameObject.GetComponent <Collider2D>());
		t_projectile projectile_script = projectile.GetComponent <t_projectile> ();
		projectile_script.Launch (own_controller.gameObject.transform.localScale.x, weapon_damage_amount);
	}
}
