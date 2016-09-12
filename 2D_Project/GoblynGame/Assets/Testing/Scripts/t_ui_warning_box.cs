using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_warning_box : MonoBehaviour {

	public static t_ui_warning_box warning_box;

	[SerializeField]
	private float warning_appearance_length;

	[SerializeField]
	private Text warning_text_object;
	[SerializeField]
	private string warning_message;
	[SerializeField]
	private bool warning_is_active = false;

	void Start(){
		if(null == warning_box){
			warning_box = this;
		}
		else if(warning_box != this){
			Destroy (this.gameObject);
		}

		if(null == warning_text_object){
			Get_Text_Object ();
		}
	}

	void Update(){
		if(true == warning_is_active){
			Handle_User_Input ();	
		}
	}

	void Handle_User_Input(){
		/*if(Input.GetKeyDown (KeyCode.E)){
			Disable_Warning_Text_Object ();
		}*/
	}

	private void Get_Text_Object(){
		warning_text_object = GetComponentInChildren <Text> ();
	}

	public void Set_Warning_Message(string _warning_message){;
		warning_message = _warning_message;

		if(null == warning_text_object){
			Get_Text_Object ();
		}
		if(null != warning_text_object){
			warning_text_object.text = warning_message;
		}
	}

	public void Enable_Warning_Text_Object(){
		warning_is_active = true;
		this.transform.GetChild (0).gameObject.SetActive (true);
		StartCoroutine (Warning_Box_Timer ());
	}

	public void Disable_Warning_Text_Object(){
		warning_is_active = false;
		this.transform.GetChild (0).gameObject.SetActive (false);
	}

	IEnumerator Warning_Box_Timer(){
		yield return new WaitForSeconds (6);
		Disable_Warning_Text_Object ();
	}
}
