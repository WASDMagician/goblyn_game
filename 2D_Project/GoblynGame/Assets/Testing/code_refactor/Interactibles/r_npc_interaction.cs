using UnityEngine;
using System.Collections;

public class r_npc_interaction : r_interactible {
	[SerializeField]
	private t_dialogue_launcher dialogue_launcher;

	public override void Interact (){
		dialogue_launcher.Launch_Dialogue ();
	}
}
