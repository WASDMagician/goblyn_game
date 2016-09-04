using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class t_ui_shop_items_panel_controller : MonoBehaviour {

	public static t_ui_shop_items_panel_controller shop_panel_controller;
	public List<GameObject> shop_items;
	private t_shop_item_controller item_to_buy;

	[SerializeField]
	float vertical_button_margin;

	// Use this for initialization
	void Start () {
		if(null == shop_panel_controller){
			shop_panel_controller = this;
		}
		else if(shop_panel_controller != this){
			Destroy (this.gameObject);
		}
		shop_items = new List<GameObject> ();
	}

	void OnDisable(){
		for(int i = 0; i < shop_items.Count; i++){
			Destroy (shop_items[i]);
		}
		shop_items.Clear ();
	}

	public void Add_Item(GameObject _shop_item, bool _is_active_item){
		if(null != _shop_item.GetComponent <t_shop_item_controller>()){
			shop_items.Add (_shop_item);
			_shop_item.transform.parent = this.transform;
			if(_is_active_item){
				Set_Active (_shop_item);
			}
		}
		else{
			Debug.Log ("Passed gameobject did not have t_shop_item_controller component attached.");
		}
	}

	public void Set_Active(GameObject _active_button){
		t_event_system_controller.event_controller.Set_First_Selected (_active_button);
	}

	public void Set_Item_To_Buy(t_shop_item_controller _item){
		item_to_buy = _item;
	}

	public t_shop_item_controller Get_Item_To_Buy(){
		if(null != item_to_buy){
			return item_to_buy;
		}
		else{
			return null;
		}
	}

}
