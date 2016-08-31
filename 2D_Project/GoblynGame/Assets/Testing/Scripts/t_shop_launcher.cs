using UnityEngine;
using System.Collections;

public class t_shop_launcher : MonoBehaviour {

	[SerializeField]
	protected GameObject[] shop_items;
	[SerializeField]
	protected bool requires_key_input;

	private bool entered_trigger_zone;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(true == entered_trigger_zone){
			Handle_User_Input ();
		}
	}

	void Handle_User_Input(){
		
		if(Input.GetKeyDown (KeyCode.E)){
			if (false == t_ui_shop_visibility_controller.shop_visibility_controller.shop_active) {
				Launch_Shop ();
				StartCoroutine (Add_Items ());
			}
		}
		if(Input.GetKeyDown (KeyCode.Escape)){
			if(true == t_ui_shop_visibility_controller.shop_visibility_controller.shop_active){
				Close_Shop ();
			}
		}
	}

	IEnumerator Add_Items(){
		while(null == t_ui_shop_items_panel_controller.shop_panel_controller){
			yield return new WaitForSeconds (0);
		}
		for(int i = 0; i < shop_items.Length; i++){
			print ("ADDING");
			GameObject shop_item = Instantiate (shop_items [i]);
			t_ui_shop_items_panel_controller.shop_panel_controller.Add_Item (shop_item);
		}
	}

	void Launch_Shop(){
		t_ui_shop_visibility_controller.shop_visibility_controller.Enable_Shop ();
	}

	void Close_Shop(){
		t_ui_shop_visibility_controller.shop_visibility_controller.Disable_Shop ();
	}

	void OnTriggerEnter2D(Collider2D _col){
		if (null != _col.GetComponent <t_player_controller> ()) {
			entered_trigger_zone = true;
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if (null != _col.GetComponent <t_player_controller> ()) {
			entered_trigger_zone = true;
		}
	}


}
