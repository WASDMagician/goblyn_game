using UnityEngine;
using System.Collections;

public class t_set_item_for_purchase : MonoBehaviour {

	private t_ui_shop_controller shop_controller;

	void Start(){
		shop_controller = GetComponentInParent <t_ui_shop_controller> ();
	}

	public void Set_Item_For_Purchase(){
		shop_controller.Set_Item_To_Buy (GetComponent <t_shop_item_controller>());
	}
}
