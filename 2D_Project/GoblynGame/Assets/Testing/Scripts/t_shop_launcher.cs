using UnityEngine;
using System.Collections;

public class t_shop_launcher : MonoBehaviour {

	[SerializeField]
	protected GameObject[] shop_items;

	private bool entered_trigger_zone;
	
	// Update is called once per frame
	void Update () {
		if(true == entered_trigger_zone && (true == t_player_states.Is_Free_Moving () || true == t_player_states.Is_In_Shop () 
			|| t_player_states.Is_In_Dialogue ())){
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
	}

	IEnumerator Add_Items(){
		while(null == t_ui_shop_items_panel_controller.shop_panel_controller){
			yield return new WaitForSeconds (0);
		}
		for(int i = 0; i < shop_items.Length; i++){
			if (i == 0) {
				GameObject shop_item = Instantiate (shop_items [i]);
				t_ui_shop_items_panel_controller.shop_panel_controller.Add_Item (shop_item, true);
			}
			else{
				GameObject shop_item = Instantiate (shop_items [i]);
				t_ui_shop_items_panel_controller.shop_panel_controller.Add_Item (shop_item, false);
			}
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
