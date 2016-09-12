using UnityEngine;
using System.Collections;

public class t_shop_buy_item : MonoBehaviour {

	private t_ui_shop_controller shop_controller;

	void Start(){
		shop_controller = GetComponentInParent<t_ui_shop_controller> ();
	}

	public void Buy_Item(){
		t_shop_item_controller item = shop_controller.Get_Item_To_Buy ();

		if(null == item){
			t_ui_warning_box.warning_box.Set_Warning_Message ("No item selected");
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
		}
		else if (item.Get_ID () == r_player_controller.character_controller.Get_Weapon_ID ()) {
			t_ui_warning_box.warning_box.Set_Warning_Message ("You already own this item.");
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
		}
		else if(r_player_controller.character_controller.Get_Gold () >= item.Get_Cost ()){
			r_player_controller.character_controller.Set_Weapon (item.Get_ID ());
			r_player_controller.character_controller.Add_Gold (-item.Get_Cost ());
			shop_controller.Disable_Shop ();
		}
		else{
			t_ui_warning_box.warning_box.Set_Warning_Message ("You don't have enough money.");
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
		}
	}
}
