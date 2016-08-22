using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class t_item_controller : MonoBehaviour {

	public Image weapon_image;
	public Text name;
	public Text damage;
	public Text cost;

	public void Set_Details(Sprite _sprite, string _name, int _damage, int _cost){
		weapon_image.sprite = _sprite;
		name.text = "Name: " + _name;
		damage.text = "Damage: " + _damage.ToString ();
		cost.text = "Cost: " + _cost.ToString ();
	}
}
