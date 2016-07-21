using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class player_hud_controller : MonoBehaviour {

	public Text teeth_amount_text;
	public Text gold_amount_text;

	public int gold_amount;
	public int teeth_amount;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Update_Stats(int _gold_amount, int _teeth_amount){
		gold_amount_text.text = "Gold: " + _gold_amount.ToString ();
		teeth_amount_text.text = "Teeth: " + _teeth_amount.ToString ();
	}
}
