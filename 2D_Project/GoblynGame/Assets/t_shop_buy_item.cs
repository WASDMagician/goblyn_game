using UnityEngine;
using System.Collections;

public class t_shop_buy_item : MonoBehaviour {

	public void Buy_Item(){
		t_shop_item_controller item = t_ui_shop_items_panel_controller.shop_panel_controller.Get_Item_To_Buy ();

		if(null == item){
			t_ui_warning_box.warning_box.Set_Warning_Message ("No item selected");
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
		}
		else if (item.Get_ID () == t_player_controller.player_controller.Get_Weapon_ID ()) {
			t_ui_warning_box.warning_box.Set_Warning_Message ("You already own this item.");
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
		}
		else if(t_player_controller.player_controller.Get_Gold () >= item.Get_Cost ()){
			t_player_controller.player_controller.Load_Weapon (item.Get_ID ());
			t_player_controller.player_controller.Set_Gold (t_player_controller.player_controller.Get_Gold () - item.Get_Cost ());
			t_ui_shop_visibility_controller.shop_visibility_controller.Disable_Shop ();
		}
		else{
			t_ui_warning_box.warning_box.Set_Warning_Message ("You don't have enough money.");
			t_ui_warning_box.warning_box.Enable_Warning_Text_Object ();
		}
	}
}
