using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_music_volume : MonoBehaviour {

	[SerializeField]
	Slider music_volume_slider;

	void Start(){
		music_volume_slider.onValueChanged.AddListener (delegate{ValueChangeCheck();});
		ValueChangeCheck ();
	}

	public void ValueChangeCheck(){
		if(null != music_volume_slider){

		}
	}
}
