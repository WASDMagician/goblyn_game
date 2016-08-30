using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_gold: MonoBehaviour {

	public Text gold_text;

	public void Set_Gold(int _gold){
		gold_text.text = _gold.ToString ();
	}
}
