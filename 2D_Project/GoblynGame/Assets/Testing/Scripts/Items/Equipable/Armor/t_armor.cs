using UnityEngine;
using System.Collections;

public class t_armor : MonoBehaviour{

	[SerializeField]
	protected int armor_defense_amount;
	[SerializeField]
	protected int armor_health;
	[SerializeField]
	protected int goblin_alarm_rating;


	public virtual void Set_Health(int _amount){
		armor_health = _amount;
		Health_Check (); //not happy with this
	}

	public virtual int Get_Health(){
		return armor_health;
	}

	public virtual int Get_Armor_Defense_Amount(){
		return armor_defense_amount;
	}

	public virtual void Damage(int _amount){
		Set_Health (Get_Health () - _amount);
	}

	public virtual void Health_Check(){
		if(Get_Health () <= 0){
			Disable ();
		}
	}
		
	public int Get_Goblin_Alarm_Rating(){
		return goblin_alarm_rating;
	}

	protected virtual void Disable(){
		//play disable animation
		t_player_controller.player_controller.Load_Armor (11);
	}
}
