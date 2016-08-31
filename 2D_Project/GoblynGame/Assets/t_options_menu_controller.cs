using UnityEngine;
using System.Collections;

public class t_options_menu_controller : MonoBehaviour {

	[SerializeField]
	private GameObject panel_object;

	public void Toggle_Options_Menu(){
		if (false == panel_object.activeInHierarchy) {
			panel_object.SetActive (true);
		}
		else{
			panel_object.SetActive (false);
		}
	}
}
