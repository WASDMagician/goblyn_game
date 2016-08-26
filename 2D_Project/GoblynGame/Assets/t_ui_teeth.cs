using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_teeth : MonoBehaviour {
	public Text teeth_value;

	public void Set_Teeth(int _teeth){
		teeth_value.text = _teeth.ToString ();
	}

}
