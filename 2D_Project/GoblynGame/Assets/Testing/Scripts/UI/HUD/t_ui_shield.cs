using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_shield : MonoBehaviour {

	public Text shield_text;

	public void Set_Shield(int _shield){
		shield_text.text = _shield.ToString ();
	}
}
