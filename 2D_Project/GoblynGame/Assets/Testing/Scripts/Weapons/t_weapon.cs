using UnityEngine;
using System.Collections;

[RequireComponent(typeof(t_weapon_details))]
public class t_weapon : MonoBehaviour {

	//components
	protected Animator weapon_animator;

	//weapon varaibles
	[SerializeField]
	protected int weapon_damage_amount;

	//animation values
	//amount of time at start of attack to not do damage
	[SerializeField]
	protected float weapon_animation_ignore_start_time;
	//how long, during the attack, to do damage for after the initial ignore has ended
	[SerializeField]
	protected float weapon_animation_damage_time;
	//amount of time at end of attack to not do damage
	[SerializeField]
	protected float weapon_animation_ignore_end_time;

	protected bool can_attack = true; 


	protected virtual void Initialise(){
		weapon_animator = GetComponent <Animator> ();
	}

	/// <summary>
	/// Is the weapon in a state in which it can be used to attack.
	/// </summary>
	/// <returns><c>true</c> if this instance can attack; otherwise, <c>false</c>.</returns>
	public bool Can_Attack(){
		return can_attack;
	}

	public virtual void Attack(){
		if(true == can_attack){
			StartCoroutine (Attack_Start ());
		}
	}

	/// <summary>
	/// Attack start-phase, while animation gets to attack-phase no damage done to enemies.
	/// </summary>
	/// <returns>The start.</returns>
	protected virtual IEnumerator Attack_Start(){
		can_attack = false;
		yield return new WaitForSeconds (weapon_animation_ignore_start_time);
		StartCoroutine (Attack_Damage ());
	}

	/// <summary>
	/// Attack damage-phase, any enemies in range will be damaged.
	/// </summary>
	/// <returns>The damage.</returns>
	protected virtual IEnumerator Attack_Damage(){
		yield return new WaitForSeconds (0);
		StartCoroutine (Attack_End ());
	}

	/// <summary>
	/// Attack end-phase while animation ends, no damage done to enemies.
	/// </summary>
	/// <returns>The end.</returns>
	protected virtual IEnumerator Attack_End(){
		yield return new WaitForSeconds (weapon_animation_ignore_end_time);
		can_attack = true;
	}
}
