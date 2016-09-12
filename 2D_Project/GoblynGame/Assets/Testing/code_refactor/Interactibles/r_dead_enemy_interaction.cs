using UnityEngine;
using System.Collections;

public class r_dead_enemy_interaction : r_interactible {
	[SerializeField]
	t_enemy_controller enemy_controller;

	void Start(){
		enemy_controller = this.GetComponent <t_enemy_controller> ();
	}

	public override void Interact ()
	{
		if(null == enemy_controller){
			enemy_controller = this.GetComponent <t_enemy_controller> ();
		}
		if(null != enemy_controller){
			enemy_controller.Loot ();
		}
	}
}
