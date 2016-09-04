using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_weapon_details : MonoBehaviour {
	[SerializeField]
	protected Sprite weapon_sprite;
	[SerializeField]
	protected string weapon_name;
	[SerializeField]
	protected int weapon_damage;
	[SerializeField]
	protected int weapon_cost;

	public Sprite Get_Weapon_Sprite(){
		return weapon_sprite;
	}

	public string Get_Weapon_Name(){
		return weapon_name;
	}

	public int Get_Weapon_Damage(){
		return weapon_damage;
	}

	public int Get_Weapon_Cost(){
		return weapon_cost;
	}
}
