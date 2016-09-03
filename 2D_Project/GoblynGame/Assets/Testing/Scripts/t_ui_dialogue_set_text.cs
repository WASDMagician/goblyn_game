using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_dialogue_set_text : MonoBehaviour {

	private Text dialogue_text_object;

	// Use this for initialization
	void Start () {
		dialogue_text_object = GetComponentInChildren <Text> ();
	}
	
	public void Set_Text(string _text){
		if(null == dialogue_text_object){
			dialogue_text_object = GetComponentInChildren<Text> ();
		}
		if(null != dialogue_text_object){
			dialogue_text_object.text = _text;
		}
	}
}
