using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textbox_manager : MonoBehaviour {

	bool is_active;
	public Vector3 textbox_position; //position to move to

	public GameObject text_box_positioner_object; //text_box_positioner gameobject
	RectTransform text_box_positioner_rect;

	public GameObject text_box_object;
	public RectTransform text_box_rect;
	public Image text_box_image;

	public GameObject text_element_object;
	public Text text_element_text; //text_element gameobject

	public TextAsset text_file;
	public string[] lines;

	public bool button_press_destroy;

	// Use this for initialization
	void Start () {
		text_box_positioner_object = GameObject.FindGameObjectWithTag ("text_box_positioner").gameObject;
		text_box_positioner_rect = text_box_positioner_object.GetComponent <RectTransform> ();

		text_box_object = GameObject.FindGameObjectWithTag ("text_box").gameObject;
		text_box_rect = text_box_object.GetComponent <RectTransform> ();
		text_box_image = text_box_object.GetComponent <Image> ();

		text_element_object = GameObject.FindGameObjectWithTag ("text_element").gameObject;
		text_element_text = text_element_object.GetComponent <Text> ();

	}

	void Update(){
		if(is_active == true){
			Handle_Input ();
		}
	}

	void Handle_Input(){
		if(button_press_destroy == true){
			if(Input.GetKeyUp (KeyCode.E)){
				Deactivate ();
				Destroy (this.gameObject);
			}
		}
	}


	public void Activate(){
		text_box_image.enabled = true;
		text_element_text.enabled = true;

		Load_Text_File (text_file);
		Set_Position ();

		is_active = true;
	}

	public void Deactivate(){
		text_box_image.enabled = false;
		text_element_text.enabled = false;
		is_active = false;
	}

	public void Load_Text_File(TextAsset _text_file){
		text_file = _text_file;
		lines = new string[0];
		lines = (text_file.text.Split ('\n'));
		string text = "";
		for(int i = 0; i < lines.Length; i++){
			text += lines [i];
			text += "\n";
		}
		text_element_text.text = text;
	}

	void Set_Position(){
		text_box_positioner_rect.anchoredPosition = textbox_position;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (GetComponent<BoxCollider2D>().bounds.extents.x * 2, 1, 1));
	}
}
