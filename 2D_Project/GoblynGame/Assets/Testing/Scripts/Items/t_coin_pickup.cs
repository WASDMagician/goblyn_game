using UnityEngine;
using System.Collections;

public class t_coin_pickup : t_pickup {

	[SerializeField]
	int coin_value;

	public override void Pickup_Item ()
	{
		if (false == requires_key_input || true == key_input_given || true == collided_with_player) {
			t_player_controller.player_controller.Set_Gold (t_player_controller.player_controller.Get_Gold () + coin_value);
			Destroy (this.gameObject);
		}
	}
}
