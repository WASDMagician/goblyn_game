using UnityEngine;
using System.Collections;

public class r_interactible : MonoBehaviour {

	public enum interactible_types {none, shop, npc, pickup, dead_enemy};
	public interactible_types interactible_type;

	public virtual void Interact(){
		
	}
}
