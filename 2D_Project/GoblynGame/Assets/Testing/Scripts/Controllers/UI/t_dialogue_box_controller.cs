using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class t_dialogue_box_controller : MonoBehaviour {

	public static t_dialogue_box_controller dialogue_box_controller;

	public dialogue_box dialogue;

	public Text text_area;
	
	public Button button_one;
	public Text button_one_text;

	public Button button_two;
	public Text button_two_text;

	int current_page = 0;

	// Use this for initialization
	void Start () {
		if(null == dialogue_box_controller){
			dialogue_box_controller = this;
		}
		else if(this != dialogue_box_controller){
			Destroy (gameObject);
		}

		Setup_Dialogue_Box ();
	}

	void Setup_Dialogue_Box(){
		current_page = 0;
		Set_Page (current_page);
	}

	public void Setup_Dialogue_Box(dialogue_box _dialogue_box){
		dialogue = _dialogue_box;
		current_page = 0;
		Set_Page (current_page);
	}

	void Set_Page(int _page){
		if (_page == -1) {
			Exit ();
		} else {
			current_page = _page;
			text_area.text = dialogue.pages [_page].dialogue_area;
			button_one_text.text = dialogue.pages [_page].answer_one;
			button_two_text.text = dialogue.pages [_page].answer_two;
		}
	}

	public void Button_One_Press(){
		Selection_Action (dialogue.pages[current_page].answer_one_action_index);
		Set_Page (dialogue.pages[current_page].answer_one_page_index);
	}

	public void Button_Two_Press(){
		Selection_Action (dialogue.pages[current_page].answer_two_action_index);
		Set_Page (dialogue.pages[current_page].answer_two_page_index);
	}

	void Selection_Action(int _selection){
		if(_selection >= 0){
			return;
		}
		else if(game_codes.exit == _selection){
			Exit ();
		}
		else if(game_codes.give_gold == _selection){
			
			t_player_controller.player_controller.Set_Gold (t_player_controller.player_controller.Get_Gold () + dialogue.pages[current_page].gold_amount);
		}
		else if(game_codes.take_gold == _selection){
			t_player_controller.player_controller.Set_Gold (t_player_controller.player_controller.Get_Gold () - dialogue.pages[current_page].gold_amount);
		}
		else if(game_codes.give_teeth == _selection){
			t_player_controller.player_controller.Set_Teeth (t_player_controller.player_controller.Get_Teeth () + dialogue.pages[current_page].teeth_amount);
		}
		else if(game_codes.take_teeth == _selection){
			t_player_controller.player_controller.Set_Teeth (t_player_controller.player_controller.Get_Teeth () - dialogue.pages[current_page].teeth_amount);
		}
		else if(game_codes.give_health == _selection){
			t_player_controller.player_controller.Set_Health (t_player_controller.player_controller.Get_Health () + dialogue.pages[current_page].health_amount);
		}
		else if(game_codes.take_health == _selection){
			t_player_controller.player_controller.Set_Health (t_player_controller.player_controller.Get_Health () - dialogue.pages[current_page].health_amount);
		}
	}

	public void Activate(){
		this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
	}

	public void Exit(){
		this.gameObject.transform.GetChild (0).gameObject.SetActive (false);
	}
}
