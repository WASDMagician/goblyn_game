using UnityEngine;
using System.Collections;

public class t_shop_controller : MonoBehaviour {

	public weapon_object weapon_one;
	public weapon_object weapon_two;
	public weapon_object weapon_three;

	public t_item_controller item_one;
	public t_item_controller item_two;
	public t_item_controller item_three;

	void Start(){
		Set_Item_Details (item_one, weapon_one);
		Set_Item_Details (item_two, weapon_two);
		Set_Item_Details (item_three, weapon_three);
	}

	public void Set_Weapons(weapon_object _weapon_one, weapon_object _weapon_two, weapon_object _weapon_three){
		weapon_one = _weapon_one;
		weapon_two = _weapon_two;
		weapon_three = _weapon_three;

		Set_Item_Details (item_one, weapon_one);
		Set_Item_Details (item_two, weapon_two);
		Set_Item_Details (item_three, weapon_three);
	}

	void Set_Item_Details(t_item_controller _item, weapon_object _object){
		_item.Set_Details (_object.sprite_image, _object.name, _object.damage, _object.cost);
	}
}
