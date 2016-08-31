using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_effects_volume : MonoBehaviour {

	[SerializeField]
	Slider effects_volume_slider;

	void Start(){
		effects_volume_slider.onValueChanged.AddListener (delegate{ValueChangeCheck();});
		ValueChangeCheck ();
	}

	public void ValueChangeCheck(){
		if(null != effects_volume_slider){

		}
	}
}
