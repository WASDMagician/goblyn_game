using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player_controller : character_controller {

	public List<soldier_controller> dead_soldiers;
	public player_hud_controller player_hud;
	// Use this for initialization
	void Start () {
		movement = GetComponent <character_movement> ();
		animator = GetComponent <Animator> ();
		weapon = GetComponentInChildren <weapon_controller> ();
		dead_soldiers = new List<soldier_controller> ();
		player_hud.Update_Stats (Get_Gold (), Get_Teeth ());
	}
	
	// Update is called once per frame
	void Update () {
		Handle_User_Input ();
	}

	void Handle_User_Input(){
		if(Input.GetKey (KeyCode.LeftArrow)){
			movement.Set_Direction (-1);
		}
		else if(Input.GetKey (KeyCode.RightArrow)){
			movement.Set_Direction (1);
		}
		else{
			movement.Set_Direction (0);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			movement.Jump ();
		}

		if(Input.GetKeyDown (KeyCode.A)){
			StartCoroutine (weapon.Start_Attack ());
		}

		if(Input.GetKeyUp (KeyCode.E)){
			for(int i = 0; i < dead_soldiers.Count; i++){
				soldier_controller soldier = dead_soldiers [i];
				if(soldier.Get_Gold () > 0){
					Add_Gold (soldier.Get_Gold ());
					soldier.Remove_Gold (soldier.Get_Gold ());
					player_hud.Update_Stats (Get_Gold (), Get_Teeth ());
				}
				else if(soldier.Get_Teeth () > 0){
					Add_Teeth (soldier.Get_Teeth ());
					soldier.Remove_Teeth (soldier.Get_Teeth ());
					player_hud.Update_Stats (Get_Gold (), Get_Teeth ());
				}
				else if(soldier.weapon != null){
					
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D _col){
		if(_col.gameObject.layer == LayerMask.NameToLayer ("Enemy")){
			soldier_controller soldier = _col.GetComponent <soldier_controller>();
			if(soldier.alive == false){
				dead_soldiers.Add (soldier);
			}
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		if(_col.gameObject.layer == LayerMask.NameToLayer ("Enemy")){
			dead_soldiers.Remove (_col.GetComponent <soldier_controller>());
		}
	}

}
