using UnityEngine;
using System.Collections;

public class t_shop_launcher : MonoBehaviour {

	[SerializeField]
	protected GameObject[] shop_items;
	[SerializeField]
	t_ui_shop_controller shop_controller;

	IEnumerator Add_Items(){
		while(null == shop_controller){
			yield return new WaitForSeconds (0);
		}
		print ("Add items");
		for(int i = 0; i < shop_items.Length; i++){
			if (i == 0) {
				GameObject shop_item = Instantiate (shop_items [i]);
				shop_controller.Add_Item (shop_item, true);
			}
			else{
				GameObject shop_item = Instantiate (shop_items [i]);
				shop_controller.Add_Item (shop_item, false);
			}
		}
	}

	public void Launch_Shop(){
		shop_controller.Enable_Shop ();
		StartCoroutine (Add_Items ());
	}

	public void Disable_Shop(){
		shop_controller.Disable_Shop ();
	}
}
