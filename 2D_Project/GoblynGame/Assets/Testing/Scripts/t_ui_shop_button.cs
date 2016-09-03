using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_shop_button : MonoBehaviour {

	private Text button_text;
	private Button button;
	private int button_number;

	void Start(){
		Get_Components ();
	}

	void Get_Components(){
		if(null == button){
			button = GetComponentInChildren<Button> ();
			button_text = GetComponentInChildren <Text> ();
		}
	}

	public void Set_Button_Text(string _text){
		if(null == button || null == button_text){
			Get_Components ();
		}
		if(null != button && null != button_text){
			button_text.text = _text;
		}
		else{
			print ("There is no button or there is no button text :(");
		}
	}

	public void Set_Button_Number(int _number){
		button_number = _number;
	}

	public int Get_Button_Number(){
		return button_number;
	}

	public void Button_Click(){
		t_ui_dialogue_controller.dialogue_controller.Handle_Button_Click (button_number);
	}
}
