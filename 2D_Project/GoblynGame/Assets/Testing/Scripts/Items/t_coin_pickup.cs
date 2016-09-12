﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class t_coin_pickup: t_pickup {

	[SerializeField]
	private int coin_value;

	public override void Pickup_Item ()
	{
		if ((false == requires_key_input) || (true == requires_key_input && true == key_input_given)) {
			r_player_controller.character_controller.Add_Gold (coin_value);
			Destroy_Pickup ();
		}
	}

	void Destroy_Pickup(){
		Destroy (this.gameObject);
	}
}