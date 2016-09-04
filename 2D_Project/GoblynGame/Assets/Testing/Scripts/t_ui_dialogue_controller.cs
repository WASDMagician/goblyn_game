using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class t_ui_dialogue_controller : MonoBehaviour {

	public static t_ui_dialogue_controller dialogue_controller;

	private dialogue_box dialogue;
	[SerializeField]
	private GameObject button_prefab;
	[SerializeField]
	private t_ui_dialogue_set_text dialogue_text;
	[SerializeField]
	private t_ui_dialogue_set_buttons dialogue_buttons;

	private List<GameObject> button_objects;

	private int current_page = 0;

	void Start(){
		if(null == dialogue_controller){
			dialogue_controller = this;
		}
		else if(this != dialogue_controller){
			Destroy (this.gameObject);
		}

		button_objects = new List<GameObject> ();
	}

	public void Enable_Dialogue(dialogue_box _dialogue){
		t_player_states.Set_In_Dialogue ();
		this.transform.GetChild (0).gameObject.SetActive (true);
		Setup_Dialogue (_dialogue);
	}

	public void Disable_Dialogue(){
		current_page = 0;
		Erase_Buttons ();
		t_player_states.Set_Free_Moving ();
		this.transform.GetChild (0).gameObject.SetActive (false);
	}
	
	public void Setup_Dialogue(dialogue_box _dialogue){
		dialogue = _dialogue;
		Setup_Text ();
		Setup_Buttons ();
	}

	void Setup_Text(){
		dialogue_text.Set_Text (dialogue.pages[current_page].page_text);
	}

	void Setup_Buttons(){
		Erase_Buttons ();

		int num_buttons = dialogue.pages [current_page].option_buttons.Length;
		for(int b = 0; b < num_buttons; b++){
			GameObject button = Instantiate (button_prefab);
			t_ui_shop_button button_script = button.GetComponent <t_ui_shop_button> ();
			button_script.Set_Button_Text (dialogue.pages[current_page].option_buttons[b].button_text);
			button_script.Set_Button_Number (b);
			button.transform.parent = dialogue_buttons.gameObject.transform;
			button_objects.Add (button);
		}

		EventSystem.current.SetSelectedGameObject (button_objects[0]);

	}

	void Go_To_Page(int _page){
		current_page = _page;
		Setup_Text ();
		Setup_Buttons ();
	}

	public void Handle_Button_Click(int _button_number){
		int page_index = dialogue.pages [current_page].option_buttons [_button_number].go_to_page;
		Carry_Out_Button_Action (_button_number);
		if(-1 == page_index){
			Disable_Dialogue ();
		}
		else if(page_index > -1){
			Go_To_Page (page_index);
		}
	}

	void Erase_Buttons(){
		if (null != button_objects) {
			for (int i = 0; i < button_objects.Count; i++) {
				Destroy (button_objects [i]);
			}
			button_objects.Clear ();
		}
	}

	void Carry_Out_Button_Action(int _button_number){
		int action_value = dialogue.pages [current_page].option_buttons [_button_number].action_value;
		dialogue_box.dialogue_page.dialogue_options.actions action = dialogue.pages [current_page].option_buttons [_button_number].action;
		if(action == dialogue_box.dialogue_page.dialogue_options.actions.modify_gold){
			t_player_controller.player_controller.Set_Gold (t_player_controller.player_controller.Get_Gold () + action_value);
		}
		else if(action == dialogue_box.dialogue_page.dialogue_options.actions.modify_health){
			t_player_controller.player_controller.Set_Health (t_player_controller.player_controller.Get_Health () + action_value);
		}
		else if(action == dialogue_box.dialogue_page.dialogue_options.actions.modify_teeth){
			t_player_controller.player_controller.Set_Teeth (t_player_controller.player_controller.Get_Teeth () + action_value);
		}
		else if(action == dialogue_box.dialogue_page.dialogue_options.actions.give_item){
			
		}
	}
}
