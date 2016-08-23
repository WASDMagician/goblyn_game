using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class player_hud_controller : MonoBehaviour {

	public Text teeth_amount_text;
	public Text gold_amount_text;

	public int gold_amount;
	public int teeth_amount;

	public void Update_Stats(int _gold_amount, int _teeth_amount){
		print ("UPDATE STATS: " + _gold_amount.ToString () + " " + _teeth_amount.ToString ());
		gold_amount_text.text = "Gold: " + _gold_amount.ToString ();
		teeth_amount_text.text = "Teeth: " + _teeth_amount.ToString ();
	}
}
