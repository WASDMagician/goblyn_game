using UnityEngine;
using System.Collections;

public class r_shop_interaction : r_interactible {
	[SerializeField]
	private t_shop_launcher shop_launcher;

	public override void Interact (){
		shop_launcher.Launch_Shop ();
	}
}
