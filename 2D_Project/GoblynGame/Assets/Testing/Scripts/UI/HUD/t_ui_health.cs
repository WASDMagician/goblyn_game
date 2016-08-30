using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_health : MonoBehaviour {

	public Image health_background;
	public Image health_foreground;

	public void Set_Health(float max_health, float current_health){
		health_foreground.fillAmount = (current_health / max_health);
	}
}
