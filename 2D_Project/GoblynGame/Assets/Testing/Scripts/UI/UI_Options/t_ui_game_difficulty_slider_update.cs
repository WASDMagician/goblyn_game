using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class t_ui_game_difficulty_slider_update : MonoBehaviour {

	[SerializeField]
	Slider difficulty_slider;
	[SerializeField]
	Text difficulty_readout;
	static Dictionary<int, string> difficulty_readings = new Dictionary<int, string> (){
		{0, "Baby Goblin"},
		{1, "Competent Goblin"},
		{2, "Goblin King"},
		{3, "A Real Boy"},
		{4, "Addicted"},
		{5, "Rehab"}
	};

	void Start(){
		difficulty_slider.onValueChanged.AddListener (delegate{ValueChangeCheck();});
		ValueChangeCheck ();
	}

	public void ValueChangeCheck(){
		if(null != difficulty_slider && null != difficulty_readout){
			difficulty_readout.text = difficulty_readings [(int)difficulty_slider.value];
		}
	}
}
