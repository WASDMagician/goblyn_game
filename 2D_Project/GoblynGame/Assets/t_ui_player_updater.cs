using UnityEngine;
using System.Collections;

public class t_ui_player_updater : MonoBehaviour {

	public static t_ui_player_updater player_updater;

	public t_ui_health ui_health;
	public t_ui_shield ui_shield;
	public t_ui_coins ui_coins;
	public t_ui_teeth ui_teeth;

	// Use this for initialization
	void Start () {
		if(null == player_updater){
			player_updater = this;
		}
		else if(this != player_updater){
			Destroy (gameObject);
		}
	}

	public void Set_Hud_Values(float _max_health, float _current_health, float _shield, int _coins, int _teeth){
		print ("Set hud values");
		print (_max_health + " " + _current_health + " " + _shield + " " + _coins + " " + _teeth);
		ui_health.Set_Health (_max_health, _current_health);
		ui_shield.Set_Shield (_shield);
		ui_coins.Set_Coins (_coins);
		ui_teeth.Set_Teeth (_teeth);
	}
}
