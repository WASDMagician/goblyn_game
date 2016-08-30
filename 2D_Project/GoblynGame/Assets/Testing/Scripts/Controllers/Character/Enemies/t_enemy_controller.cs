using UnityEngine;
using System.Collections;

public class t_enemy_controller : t_character_controller{

	t_enemy_health_bar health_bar;

	// Use this for initialization
	void Start () {
		health_bar = GetComponent <t_enemy_health_bar> ();
	}
	
	// Update is called once per frame
	void Update () {
		health_bar.Update_Health_Bar ();
	}

	public override void Damage (int _damage_amount)
	{
		base.Damage (_damage_amount);
		health_bar.Update_Health_Bar ();
	}

	public override void Kill ()
	{
		//play death animation
		Destroy (this.gameObject);
	}
}
