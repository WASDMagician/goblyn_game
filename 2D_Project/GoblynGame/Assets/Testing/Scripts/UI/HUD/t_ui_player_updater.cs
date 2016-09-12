using UnityEngine;
using System.Collections;

public class t_ui_player_updater : MonoBehaviour {

	public static t_ui_player_updater player_updater;

	[SerializeField]
	private t_ui_health ui_health;
	[SerializeField]
	private t_ui_shield ui_shield;
	[SerializeField]
	private t_ui_gold ui_gold;
	[SerializeField]
	private t_ui_teeth ui_teeth;

	// Use this for initialization
	void Start () {
		if(null == player_updater){
			player_updater = this;
		}
		else if(this != player_updater){
			Destroy (gameObject);
		}
	}

	public void Update_UI(float _max_health, float _current_health, int _shield, int _gold, int _teeth){
		Set_UI_Health (_max_health, _current_health);
		Set_UI_Shield (_shield);
		Set_UI_Gold (_gold);
		Set_UI_Teeth (_teeth);
	}

	public void Set_UI_Health(float _max_health, float _current_health){
		ui_health.Set_Health (_max_health, _current_health);
	}

	public void Set_UI_Shield(int _shield){
		ui_shield.Set_Shield (_shield);
	}

	public void Set_UI_Gold(int _gold){
		ui_gold.Set_Gold (_gold);
	}

	public void Set_UI_Teeth(int _teeth){
		ui_teeth.Set_Teeth (_teeth);	
	}
}
