using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_enemy_health_bar : MonoBehaviour {
	[SerializeField]
	string name;
	[SerializeField]
	Text name_text;
	[SerializeField]
	GameObject enemy_health_prefab;
	[SerializeField]
	GameObject enemy_health_object;
	[SerializeField]
	Image background_image;
	[SerializeField]
	Image foreground_image;

	private GameObject health_bar_position;

	t_enemy_controller enemy_controller;

	void Start(){
		enemy_health_object = Instantiate (enemy_health_prefab);
		if(enemy_health_object != null){
			background_image = enemy_health_object.transform.GetChild (1).GetComponent <Image> ();
			foreground_image = enemy_health_object.transform.GetChild (2).GetComponent <Image> ();
			name_text = enemy_health_object.transform.GetChild (0).GetComponent <Text> ();
		}

		enemy_controller = GetComponent<t_enemy_controller>();
		health_bar_position = this.transform.GetChild (1).gameObject;
		name_text.text = name;
		StartCoroutine (Parent_Healthbar_To_UI_Group ());
	}

	IEnumerator Parent_Healthbar_To_UI_Group(){
		while(t_ui_group_enemy_health_controller.enemy_health_group_controller == null){
			yield return new WaitForSeconds (0.25f);
		}
		enemy_health_object.transform.SetParent (t_ui_group_enemy_health_controller.enemy_health_group_controller.transform);
	}

	public void Update_Health_Bar(){
		foreground_image.fillAmount = (float)enemy_controller.Get_Health () / (float)enemy_controller.Get_Max_Health ();
		enemy_health_object.transform.position = Camera.main.WorldToScreenPoint (health_bar_position.transform.position);
	}

	void OnDestroy(){
		Destroy (enemy_health_object);
	}
}
