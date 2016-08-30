using UnityEngine;
using System.Collections;

public class t_weapon_pickup : t_pickup {

	[SerializeField]
	int weapon_id;

	public override void Pickup_Item ()
	{
		if (false == requires_key_input || true == key_input_given || true == collided_with_player) {
			t_player_controller.player_controller.Load_Weapon (weapon_id);
			Destroy (this.gameObject);
		}
	}
}
