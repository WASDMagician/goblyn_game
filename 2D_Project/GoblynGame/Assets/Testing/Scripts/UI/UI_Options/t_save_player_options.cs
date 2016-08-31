using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_save_player_options : MonoBehaviour {

	[SerializeField]
	Slider master_volume_slider;
	[SerializeField]
	Slider effects_volume_slider;
	[SerializeField]
	Slider music_volume_slider;
	[SerializeField]
	Slider difficulty_slider;
	[SerializeField]
	Toggle autosave_toggle;

	public void Save_Player_Prefs(){
		PlayerPrefs.SetInt ("master_volume", (int)master_volume_slider.value);
		PlayerPrefs.SetInt ("effects_volume", (int)effects_volume_slider.value);
		PlayerPrefs.SetInt ("music_volume", (int)music_volume_slider.value);
		PlayerPrefs.SetInt ("difficulty", (int)difficulty_slider.value);
		if(true == autosave_toggle.isOn){
			PlayerPrefs.SetInt ("autosave", 1);
		}
		else{
			PlayerPrefs.SetInt ("autosave", 0);
		}
	}
}
