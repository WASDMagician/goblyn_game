using UnityEngine;
using System.Collections;

public class armor_controller : MonoBehaviour {

	public enum head_armors {none, human, leather, steel};
	public enum body_armors {none, human, leather, steel};
	public enum legs_armors {none, human, leather, steel};

	int[] head_damage_reduction;
	int[] body_damage_reduction;
	int[] leg_damage_reduction;

	int[] armor_visibility_modifier; //assumes each armor set has same amount and are worth same

	public head_armors head_armor;
	public body_armors body_armor;
	public legs_armors leg_armor;

	public int hiddenness; //higher is better
	public int damage_resistance; //higher is better

	// Use this for initialization
	void Start () {
		head_damage_reduction = new int[] { 0, 1, 2, 3};
		body_damage_reduction = new int[] { 0, 1, 2, 3 };
		leg_damage_reduction = new int[] { 0, 1, 2, 3 };
		armor_visibility_modifier = new int[] {-3, 1, 2, 3};
		Calculate_Skin_Visibility ();
		Calculate_Damage_Resistance ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Set_Head_Armor(head_armors _head_armor){
		head_armor = _head_armor;
		Calculate_Skin_Visibility ();
		Calculate_Damage_Resistance ();
	}

	void Set_Body_armors(body_armors _body_armor){
		body_armor = _body_armor;
		Calculate_Skin_Visibility ();
		Calculate_Damage_Resistance ();
	}

	void Set_Leg_armors(legs_armors _leg_armor){
		leg_armor = _leg_armor;
		Calculate_Skin_Visibility ();
		Calculate_Damage_Resistance ();
	}

	void Calculate_Skin_Visibility(){
		hiddenness += (armor_visibility_modifier [(int)head_armor] + armor_visibility_modifier[(int)body_armor] + armor_visibility_modifier[(int)leg_armor]);
	}

	void Calculate_Damage_Resistance(){
		damage_resistance = (int)head_armor + (int)body_armor + (int)leg_armor;
	}
}
