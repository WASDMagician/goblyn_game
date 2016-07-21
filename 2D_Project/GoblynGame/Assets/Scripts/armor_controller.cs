using UnityEngine;
using System.Collections;

public class armor_controller : MonoBehaviour {

	public enum armor_type {goblin, peasant, knight, wizard, };

	int[] armor_damage_reduction;

	public armor_type current_armor;

	public int damage_resistance; //higher is better
	public int hiddenness;

	// Use this for initialization
	void Start () {
		armor_damage_reduction = new int[] { 0, 1, 2, 3};
		Calculate_Damage_Resistance ();
	}

	public void Set_Armor(armor_type _armor){
		current_armor = _armor;
		Calculate_Damage_Resistance ();
	}

	public armor_type Get_Armor(){
		return current_armor;
	}

	void Calculate_Damage_Resistance(){
		damage_resistance = armor_damage_reduction [(int)current_armor];
	}

	void Calculate_Hiddenness(){
		hiddenness = (int)current_armor;
	}
}
