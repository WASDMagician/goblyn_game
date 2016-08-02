using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class shop_controller : MonoBehaviour {


	[System.Serializable]
	public class Weapon_Information{
		//DO NOT CHANGE ANYTHING IN THIS UNLESS ABSOLUTELY NECESARRY
		//DOING SO WILL RESET EVERY SHOP IN THE GAME MEANING IT HAS
		//TO BE SET UP AGAIN!!!!!!!
		public Sprite weapon_image;
		public string weapon_name;
		public int weapon_damage;
		public int weapon_cost;
	}

	player_controller player_object;

	public weapon_controller.weapons weapon_one;
	public weapon_controller.weapons weapon_two;
	public weapon_controller.weapons weapon_three;

	bool in_activation_range = false;
	bool shop_active = false;

	GameObject ui_shop_item;
	GameObject ui_item_one;
	GameObject ui_item_two;
	GameObject ui_item_three;
	GameObject ui_buy_button;
	GameObject ui_cancel_button;

	GameObject ui_money_error;

	Image ui_item_one_background_image;
	Image ui_item_two_background_image;
	Image ui_item_three_background_image;
	Image ui_buy_button_background_image;
	Image ui_cancel_button_background_image;

	Image ui_item_one_image;
	Text ui_item_one_name;
	Text ui_item_one_damage;
	Text ui_item_one_cost;

	Image ui_item_two_image;
	Text ui_item_two_name;
	Text ui_item_two_damage;
	Text ui_item_two_cost;

	Image ui_item_three_image;
	Text ui_item_three_name;
	Text ui_item_three_damage;
	Text ui_item_three_cost;

	public Weapon_Information weapone_one_info;
	public Weapon_Information weapon_two_info;
	public Weapon_Information weapon_three_info;

	List<GameObject> ui_element_list;

	int current_selection = 0;
	int fixed_selection = -1;

	public Color standard_colour;
	public Color currently_selected_colour;
	public Color fixed_colour;

	int selectable_items_start;
	int selectable_items_end;

	void Awake(){
		Get_UI_Items ();
		selectable_items_start = 0;
		selectable_items_end = ui_element_list.Count;
		ui_money_error.SetActive (false);
		ui_shop_item.SetActive (false);
	}

	void Get_UI_Items(){
		ui_shop_item = GameObject.FindGameObjectWithTag ("shop_item");

		ui_item_one = GameObject.FindGameObjectWithTag ("item_one");
		ui_item_one_image = ui_item_one.transform.GetChild (0).GetComponent <Image> ();
		ui_item_one_name = ui_item_one.transform.GetChild (1).GetComponent <Text> ();
		ui_item_one_damage = ui_item_one.transform.GetChild (2).GetComponent <Text> ();
		ui_item_one_cost = ui_item_one.transform.GetChild (3).GetComponent <Text> ();

		ui_item_two = GameObject.FindGameObjectWithTag ("item_two");
		ui_item_two_image = ui_item_two.transform.GetChild (0).GetComponent <Image> ();
		ui_item_two_name = ui_item_two.transform.GetChild (1).GetComponent <Text> ();
		ui_item_two_damage = ui_item_two.transform.GetChild (2).GetComponent <Text> ();
		ui_item_two_cost = ui_item_two.transform.GetChild (3).GetComponent <Text> ();

		ui_item_three = GameObject.FindGameObjectWithTag ("item_three");
		ui_item_three_image = ui_item_three.transform.GetChild (0).GetComponent <Image> ();
		ui_item_three_name = ui_item_three.transform.GetChild (1).GetComponent <Text> ();
		ui_item_three_damage = ui_item_three.transform.GetChild (2).GetComponent <Text> ();
		ui_item_three_cost = ui_item_three.transform.GetChild (3).GetComponent <Text> ();

		ui_buy_button = GameObject.FindGameObjectWithTag ("buy_button");
		ui_cancel_button = GameObject.FindGameObjectWithTag ("cancel_button");

		ui_item_one_background_image = ui_item_one.GetComponent <Image> ();
		ui_item_two_background_image = ui_item_two.GetComponent <Image> ();
		ui_item_three_background_image = ui_item_three.GetComponent <Image> ();
		ui_buy_button_background_image = ui_buy_button.GetComponent <Image> ();
		ui_cancel_button_background_image = ui_cancel_button.GetComponent <Image> ();

		ui_money_error = GameObject.FindGameObjectWithTag ("money_error");

		ui_element_list = new List<GameObject> {ui_item_one, ui_item_two, ui_item_three, ui_buy_button, ui_cancel_button};
	}

	void Set_UI_Items_Values(){
		ui_item_one_image.sprite = weapone_one_info.weapon_image;
		ui_item_one_name.text = weapone_one_info.weapon_name;
		ui_item_one_damage.text = "Damage: " + weapone_one_info.weapon_damage.ToString ();
		ui_item_one_cost.text = "Cost: " + weapone_one_info.weapon_cost.ToString ();

		ui_item_two_image.sprite = weapon_two_info.weapon_image;
		ui_item_two_name.text = weapon_two_info.weapon_name;
		ui_item_two_damage.text = "Damage: " + weapon_two_info.weapon_damage.ToString ();
		ui_item_two_cost.text = "Cost: " + weapon_two_info.weapon_cost.ToString ();

		ui_item_three_image.sprite = weapon_three_info.weapon_image;
		ui_item_three_name.text = weapon_three_info.weapon_name;
		ui_item_three_damage.text = "Damage: " + weapon_three_info.weapon_damage.ToString ();
		ui_item_three_cost.text = "Cost: " + weapon_three_info.weapon_cost.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if(in_activation_range == true){
			Handle_User_Input ();
		}
	}

	void Handle_User_Input ()
	{
		if (shop_active == true) {
			if (ui_money_error.active == true) {
				if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.E)) {
					ui_money_error.SetActive (false);
				}
			}
			else{
				if (Input.GetKeyDown (KeyCode.DownArrow)) {
					if (current_selection + 1 < selectable_items_end) {
						current_selection++;
					} else {
						current_selection = selectable_items_start;
					}
					Reset_Colors ();
					Set_Selected_Item_Color ();
					Set_Fixed_Item_Color ();
				} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
					if (current_selection > selectable_items_start) {
						current_selection--;
					} else {
						current_selection = selectable_items_end - 1;
					}
					Reset_Colors ();
					Set_Selected_Item_Color ();
					Set_Fixed_Item_Color ();
				}

				if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.E)) {
					switch (current_selection) {
					case 0:
						print ("Select to fixed");
						fixed_selection = current_selection;
						break;
					case 1:
						print ("Select to fixed");
						fixed_selection = current_selection;
						break;
					case 2:
						print ("Select to fixed");
						fixed_selection = current_selection;
						break;
					case 3:
						Buy_Button ();
						break;
					case 4:
						Cancel_button ();
						break;
					default:
						break;
					}

					Reset_Colors ();
					Set_Selected_Item_Color ();
					Set_Fixed_Item_Color ();
				}
				if(Input.GetKeyDown (KeyCode.Escape)){
					Deactivate ();
				}
			}
		}

		else if (Input.GetKeyDown (KeyCode.E) && ui_shop_item.active == false) {
			Activate ();
		}
	}


	void Set_Selected_Item_Color(){
		if(current_selection != -1){
			switch(current_selection){
			case 0:
				ui_item_one_background_image.color = currently_selected_colour;
				break;
			case 1:
				ui_item_two_background_image.color = currently_selected_colour;
				break;
			case 2:
				ui_item_three_background_image.color = currently_selected_colour;
				break;
			case 3:
				ui_buy_button_background_image.color = currently_selected_colour;
				break;
			case 4:
				ui_cancel_button_background_image.color = currently_selected_colour;
				break;
			}
		}
	}

	void Set_Fixed_Item_Color(){
		if(fixed_selection != -1){
			switch(fixed_selection){
			case 0:
				ui_item_one_background_image.color = fixed_colour;
				break;
			case 1:
				ui_item_two_background_image.color = fixed_colour;
				break;
			case 2:
				ui_item_three_background_image.color = fixed_colour;
				break;
			}
		}
	}

	void Reset_Colors(){
		ui_item_one_background_image.color = standard_colour;
		ui_item_two_background_image.color = standard_colour;
		ui_item_three_background_image.color = standard_colour;
		ui_buy_button_background_image.color = standard_colour;
		ui_cancel_button_background_image.color = standard_colour;
	}

	public void Activate(){
		print ("Activate");
		current_selection = 0;
		fixed_selection = -1;

		Time.timeScale = 0;
		shop_active = true;
		Set_UI_Items_Values ();
		ui_money_error.SetActive (false);
		ui_shop_item.SetActive (true);

		Reset_Colors ();
		Set_Selected_Item_Color ();
	}

	public void Deactivate(){
		print ("Deactivate");
		current_selection = 0;
		fixed_selection = -1;
		Reset_Colors ();
		Time.timeScale = 1;
		shop_active = false;
		ui_money_error.SetActive (false);;
		ui_shop_item.SetActive (false);
	}
		
	void Buy_Button(){
		int gold = player_object.Get_Gold ();
		if(fixed_selection == 0){
			if (gold >= weapone_one_info.weapon_cost) {
				player_object.Remove_Gold (weapone_one_info.weapon_cost);
				player_object.Set_Weapon (weapon_one);
				Deactivate ();
			} 
			else {
				ui_money_error.SetActive (true);
			}
		}
		else if(fixed_selection == 1){
			if (gold >= weapon_two_info.weapon_cost){
				player_object.Remove_Gold (weapon_two_info.weapon_cost);
				player_object.Set_Weapon (weapon_two);
				Deactivate ();
			}
			else {
				ui_money_error.SetActive (true);
			}
		}
		else if(fixed_selection == 2){
			if(gold >= weapon_three_info.weapon_cost){
				player_object.Remove_Gold (weapon_three_info.weapon_cost);
				player_object.Set_Weapon (weapon_three);
				Deactivate ();
			}
			else {
				ui_money_error.SetActive (true);
			}
		}
	}

	void Cancel_button(){
		Deactivate ();
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(_col.CompareTag ("Player")){
			player_object = _col.GetComponent <player_controller> ();
			in_activation_range = true;
		}

	}

	void OnTriggerExit2D(Collider2D _col){
		if(_col.CompareTag ("Player")){
			player_object = null;
			in_activation_range = false;
		}

	}
}
