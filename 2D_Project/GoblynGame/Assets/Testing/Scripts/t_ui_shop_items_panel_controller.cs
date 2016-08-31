using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class t_ui_shop_items_panel_controller : MonoBehaviour {

	public static t_ui_shop_items_panel_controller shop_panel_controller;
	public List<GameObject> shop_items;

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

	public void Add_Item(GameObject _shop_item){
		print ("Add item");
		if(null != _shop_item.GetComponent <t_shop_item_controller>()){
			shop_items.Add (_shop_item);
			_shop_item.transform.parent = this.transform;
			Position_Items ();
		}
		else{
			Debug.Log ("Passed gameobject did not have t_shop_item_controller component attached.");
		}
	}

	void Position_Items(){
		for(int i = 0; i < shop_items.Count; i++){
			Vector3 position = shop_items [i].transform.position;
			//set position to top
			position.y = this.GetComponent <RectTransform> ().rect.top;
			shop_items [i].transform.localPosition = position;
		}
	}
}
