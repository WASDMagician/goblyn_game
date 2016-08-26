using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_ui_coins : MonoBehaviour {

	public Text coin_text;

	public void Set_Coins(int _coins){
		coin_text.text = _coins.ToString ();
	}
}
