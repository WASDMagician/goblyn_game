using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_shop_item_controller : MonoBehaviour {
	
	Image item_image;
	Text item_name;
	Text item_description;
	Text item_cost;

	[SerializeField]
	private Sprite image;
	[SerializeField]
	[Multiline]
	private string name;
	[SerializeField]
	[Multiline()]
	private string description;
	[SerializeField]
	private int cost;
	[SerializeField]
	private int item_id;

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

	public Sprite Get_Image(){
		return image;
	}

	public string Get_Name(){
		return name;
	}

	public string Get_Description(){
		return description;
	}

	public int Get_Cost(){
		return cost;
	}

	public int Get_ID(){
		return item_id;
	}


}
