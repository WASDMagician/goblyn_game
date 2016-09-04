using UnityEngine;
using System.Collections;

public class t_armor : MonoBehaviour, IDamageable<int> {

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

	/// <summary>
	/// How much other goblins will be alarmed by player appearance.
	/// 0 means you are dressed as a goblin and they will accept you.
	/// Each increment above that increases goblin hostility while
	/// decreasing the hostility of NPC's and enemies who will mistake
	/// you for their own, their hostility is set on a per NPC-type
	/// basis.
	/// 0 is
	/// </summary>
	/// <returns>The gobline visibility.</returns>
	public int Get_Goblin_Alarm_Rating(){
		return goblin_alarm_rating;
	}

	protected virtual void Disable(){
		//play disable animation
		t_player_controller.player_controller.Load_Armor (11);
	}
}
