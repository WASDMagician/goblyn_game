using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_shop_item_controller : MonoBehaviour {
	
	Image item_image;
	Text item_name;
	Text item_description;
	Text item_cost;

	public Sprite image;
	public string name;
	public string description;
	public int cost;
	public int item_id;

	void Start(){
		item_image = this.transform.GetChild (0).GetComponent <Image> ();
		item_name = this.transform.GetChild (1).GetComponent <Text> ();
		item_description = this.transform.GetChild (2).GetComponent <Text> ();
		item_cost = this.transform.GetChild (3).GetComponent <Text> ();
		Setup ();
	}

	public void Setup(){
		item_image.sprite = image;
		item_name.text = name;
		item_description.text = description;
		item_cost.text = "Cost: " + cost.ToString ();
	}
}
