using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_options_menu_load_values : MonoBehaviour {

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


	void OnEnable(){
		if(null != master_volume_slider){
			if(PlayerPrefs.HasKey ("master_volume")){
				master_volume_slider.value = PlayerPrefs.GetInt ("master_volume");
			}
		}
		if(null != effects_volume_slider){
			if(PlayerPrefs.HasKey ("effects_volume")){
				effects_volume_slider.value = PlayerPrefs.GetInt ("effects_volume");
			}
		}
		if(null != music_volume_slider){
			if(PlayerPrefs.HasKey ("music_volume")){
				music_volume_slider.value = PlayerPrefs.GetInt ("music_volume");
			}
		}
		if(null != difficulty_slider){
			if(PlayerPrefs.HasKey ("difficulty")){
				difficulty_slider.value = PlayerPrefs.GetInt ("difficulty");
			}
		}
		if(null != autosave_toggle){
			if (PlayerPrefs.HasKey ("autosave")) {
				int toggle = PlayerPrefs.GetInt ("autosave");
				if (0 == toggle) {
					autosave_toggle.isOn = false;
				} else {
					autosave_toggle.isOn = true;
				}
			}
		}
	}
}
