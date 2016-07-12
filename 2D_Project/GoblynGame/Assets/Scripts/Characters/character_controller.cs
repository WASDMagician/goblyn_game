using UnityEngine;
using System.Collections;

public class character_controller : MonoBehaviour {

	public character_movement movement;
	public Animator animator;
	public int health;
	public weapon_controller weapon;

	public int gold;
	public int teeth;

	public bool is_invulnerable;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public virtual void Add_Health(int amount){
		health += amount;
	}

	public virtual void Remove_Health(int amount){
		if (is_invulnerable == false) {
			health -= amount;
			Health_Check ();
		}
	}

	public int Get_Health(){
		return health;
	}

	public void Health_Check(){
		if(health <= 0){
			Die ();
		}
	}

	public virtual void Die(){
		
	}

	public void Add_Gold(int amount){
		gold += amount;
	}

	public void Remove_Gold(int amount){
		gold -= amount;
	}

	public void Add_Teeth(int amount){
		teeth += amount;
	}

	public void Remove_Teeth(int amount){
		teeth -= amount;
	}		

	public virtual void Interact(){
		//do nothing
	}

	//this is for use with the attacking animations
	//once enemy is hit during attack make them invulnerable
	//for the remainder of the attack time
	public virtual IEnumerator Make_Invulnerable_For_Seconds(float time){
		//do nothing
		yield return new WaitForSeconds (0);
	}
}
