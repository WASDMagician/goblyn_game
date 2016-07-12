using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class textbox_manager : MonoBehaviour {

	bool is_active;
	public Vector3 textbox_position;

	public GameObject positioning_element;

	public RectTransform panel_transform;

	public TextAsset text_file;
	public string[] lines;

	public int line_to_start_at;
	public int line_to_end_at;
	public int current_line;

	public Text text_line;

	float box_width;
	float box_height;

	public float char_height;
	public float char_width;

	// Use this for initialization
	void Start () {
	}

	public void Activate(){
		panel_transform.gameObject.SetActive (true);
		Load_Text_File (text_file);
		Set_Position ();
	}

	public void Deactivate(){
		panel_transform.gameObject.SetActive (false);
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
		text_line.text = text;
	}

	void Set_Position(){
		positioning_element.GetComponent <RectTransform>().anchoredPosition = textbox_position;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (GetComponent<BoxCollider2D>().bounds.extents.x * 2, 1, 1));
	}
}
