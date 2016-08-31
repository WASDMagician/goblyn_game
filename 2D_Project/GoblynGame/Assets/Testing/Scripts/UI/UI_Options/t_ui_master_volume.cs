using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class t_ui_master_volume : MonoBehaviour {

	[SerializeField]
	Slider master_volume_slider;

	void Start(){
		master_volume_slider.onValueChanged.AddListener (delegate{ValueChangeCheck();});
		ValueChangeCheck ();
	}

	public void ValueChangeCheck(){
		if(null != master_volume_slider){
			
		}
	}
}
