using UnityEngine;
using System.Collections;

public class temp_shop_test : MonoBehaviour {
	public GameObject[] shop_objects;
	// Use this for initialization
	void Start () {
		StartCoroutine (Add_Items ());
	}

	IEnumerator Add_Items(){
		while (null == t_ui_shop_items_panel_controller.shop_panel_controller) {
			print ("Waiting");
			yield return new WaitForSeconds (0);
		}
		for (int i = 0; i < shop_objects.Length; i++) {
			GameObject shop_object = Instantiate (shop_objects [i]);
			t_ui_shop_items_panel_controller.shop_panel_controller.Add_Item (shop_object);
		}
	}
}
