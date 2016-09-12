using UnityEngine;
using System.Collections;

public class t_character_controller : MonoBehaviour, IKillable, IDamageable<int> {

	[SerializeField]
	protected int max_health;
	[SerializeField]
	protected int health;
	[SerializeField]
	protected int gold;
	[SerializeField]
	protected int teeth;

	[SerializeField]
	protected int weapon_id;

	//armor values and components
	[SerializeField]
	protected int armor_id;

	protected bool is_invulnerable;

	protected bool is_alive = true;

	public virtual void Set_Max_Health(int _max_health){
		max_health = _max_health;
	}

	public int Get_Max_Health(){
		return max_health;
	}

	public virtual void Set_Health(int _health){
		if (false == is_invulnerable) {
			health = _health;
			Health_Check ();
		}
	}

	public virtual void Add_Health(int _health){
		if(false == is_invulnerable){
			health += _health;
			Health_Check ();
		}
	}

	protected virtual void Health_Check(){
		if(Get_Health () <= 0 && true == is_alive){
			Kill ();
		}
	}

	public int Get_Health(){
		return health;
	}

	public virtual void Set_Gold(int _gold){
		gold = _gold;
	}

	public virtual void Add_Gold(int _gold){
		gold += _gold;
	}

	public int Get_Gold(){
		return gold;
	}

	public virtual void Set_Teeth(int _teeth){
		teeth = _teeth;
	}

	public virtual void Add_Teeth(int _teeth){
		teeth += _teeth;
	}

	public int Get_Teeth(){
		return teeth;
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
