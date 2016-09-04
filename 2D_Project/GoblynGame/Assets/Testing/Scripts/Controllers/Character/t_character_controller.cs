using UnityEngine;
using System.Collections;

public class t_character_controller : MonoBehaviour, IKillable, IDamageable<int> {

	[SerializeField]
	protected int character_max_health;
	[SerializeField]
	protected int character_health;
	[SerializeField]
	protected int character_gold;
	[SerializeField]
	protected int character_teeth;

	[SerializeField]
	protected int weapon_id;

	//armor values and components
	[SerializeField]
	protected int armor_id = 11;

	protected bool is_invulnerable;

	protected bool is_alive = true;

	public void Set_Max_Health(int _max_health){
		character_max_health = _max_health;
	}

	public int Get_Max_Health(){
		return character_max_health;
	}

	public virtual void Set_Health(int _health){
		if (false == is_invulnerable) {
			character_health = _health;
			Health_Check ();
		}
	}

	protected virtual void Health_Check(){
		if(Get_Health () <= 0 && true == is_alive){
			Kill ();
		}
	}

	public int Get_Health(){
		return character_health;
	}

	public virtual void Set_Gold(int _gold){
		character_gold = _gold;
	}

	public int Get_Gold(){
		return character_gold;
	}

	public virtual void Set_Teeth(int _teeth){
		character_teeth = _teeth;
	}

	public int Get_Teeth(){
		return character_teeth;
	}

	public int Get_Weapon_ID(){
		return weapon_id;
	}

	public int Get_Armor_ID(){
		return armor_id;
	}
		
	public virtual void Kill(){
		Destroy (this.gameObject);
	}

	public virtual void Damage(int _damage_amount){
		Set_Health (Get_Health () - _damage_amount);
	}

	public IEnumerator Make_Invulnerable(float _time){
		is_invulnerable = true;
		yield return new WaitForSeconds (_time);
		is_invulnerable = false;
	}

	public bool Is_Invulnerable(){
		return is_invulnerable;
	}

	public bool Is_Alive(){
		return is_alive;
	}

	public void Set_Alive(bool _alive){
		is_alive = _alive;
	}


}
