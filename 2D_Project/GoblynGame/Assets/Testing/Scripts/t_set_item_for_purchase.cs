using UnityEngine;
using System.Collections;

public class t_set_item_for_purchase : MonoBehaviour {

	public void Set_Item_For_Purchase(){
		t_ui_shop_items_panel_controller.shop_panel_controller.Set_Item_To_Buy (GetComponent <t_shop_item_controller>());
	}
}
