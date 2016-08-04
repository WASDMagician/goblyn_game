using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class weapon_controller : MonoBehaviour {

	bool is_attacking;
	public enum weapons {none, bone_dagger, sword, bow};
	public enum attack_methods {melee, ranged};

	public GameObject arrow_prefab;

	attack_methods[] attack_types;
	float[] attack_ranges;
	public int[] damage_amounts;
	public string[] names;
	public int[] costs;

	//terrible naming here
	//animation starts, ignore amount, after x time do damage, at next time stop doing damage
	float[] attack_ignore_animation_start; //how much animation to ignore at start
	float[] attack_animation_damage_time; // which part of the animation to begin doing damage
	float[] attack_ignore_animation_end; //how much animation to ignore at end


	public weapons weapon = weapons.bone_dagger;
	public attack_methods attack_method;

	public float range;
	public int damage;

	public float attack_ignore_start; //example: sword raise
	public float attack_do_damage; //example: sword swipe down
	public float attack_ignore_end; //example: return to idle

	float total_attack_time;

	float attack_start_time;

	CircleCollider2D range_collider;

	Color draw_color;


	float enemies_in_range;
	public List<character_controller> enemies;

	public Animator parent_animator; //currently for parent might change


	// Use this for initialization
	void Awake () {
		//none, bone_dagger, sword, bow
		attack_types = new attack_methods[] { attack_methods.melee, attack_methods.melee, attack_methods.melee, attack_methods.ranged };
		attack_ranges = new float[] {0, 1, 3, 5};
		damage_amounts = new int[] { 1, 30, 40, 50};
		names = new string[] {"Bone Dagger", "Sword", "Bow" };
		costs = new int[] {10, 20, 30};

		//none, bone_dagger, sword, bow
		attack_ignore_animation_start = new float[] { 0, 0.5f, 0.5f, 0.25f }; //do no damage for this long
		attack_animation_damage_time = new float[] { 0, 1, 1, 1 }; //damage at this point
		attack_ignore_animation_end = new float[] { 0, 0, 0, 0}; //stop doing damage at this point
		

		range_collider = GetComponent <CircleCollider2D> ();

		Set_Weapon (weapon);

		enemies = new List<character_controller> ();

		parent_animator = GetComponentInParent <Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Set_Weapon(weapons _weapon){
		weapon = _weapon;
		attack_method = attack_types [(int)weapon];
		range = attack_ranges [(int)weapon];
		damage = damage_amounts [(int)weapon];
		if (GetComponent <CircleCollider2D> () != null) {
			range_collider.radius = range;
		}

		attack_ignore_start = attack_ignore_animation_start [(int)weapon];
		attack_do_damage = attack_animation_damage_time [(int)weapon];
		attack_ignore_end = attack_ignore_animation_end [(int)weapon];

		total_attack_time = attack_ignore_start + attack_do_damage + attack_ignore_end;
	}

	public weapons Get_Weapon(){
		return weapon;
	}

	public IEnumerator Start_Attack(){
		if (!is_attacking) {
			Set_Character_Attack_Animation (true);
			draw_color = Color.green;
			attack_start_time = Time.fixedTime;
			is_attacking = true;
			yield return new WaitForSeconds (attack_ignore_start);
			StartCoroutine (Attack ());
		}
	}

	//eeew oh god oh god oh god whyyyy
	public IEnumerator Attack(){
		if (attack_method == attack_methods.melee) {
			draw_color = Color.red;
			for (int i = 0; i < enemies.Count; i++) {
				enemies [i].is_invulnerable = false;
			}
			while (Time.fixedTime < attack_start_time + attack_ignore_start + attack_do_damage) {
				for (int i = 0; i < enemies.Count; i++) {
					//maybe change the enemies array container to use enemy_controller instead of character_controller
					//this also happens in arrow_controller
					enemies [i].GetComponent <enemy_controller> ().attack_level = 8; //makes enemy attack you if you attack them
					enemies [i].Remove_Health (damage);
					enemies [i].is_invulnerable = true;
				}
				yield return new WaitForSeconds (0);
			}
		}
		else{
			GameObject arrow_object = Instantiate (arrow_prefab, this.transform.position, Quaternion.identity) as GameObject;
			Physics2D.IgnoreCollision (arrow_object.GetComponent <BoxCollider2D>(), this.transform.parent.GetComponent <BoxCollider2D>());
			arrow_controller arrow = arrow_object.GetComponent <arrow_controller> ();
			arrow.Set_Active (true, (int)this.transform.parent.transform.localScale.x, 20, damage);

		}
		StartCoroutine (End_Attack ());
	}
		
	public IEnumerator End_Attack(){
		draw_color = Color.blue;
		yield return new WaitForSeconds (attack_ignore_end);
		Set_Character_Attack_Animation (false);
		is_attacking = false;
	}

	void Set_Character_Attack_Animation(bool _on_off){
		
	}

	void OnTriggerEnter2D(Collider2D _col){
		//don't like tag usage
		if(_col.gameObject.layer == 9 || _col.gameObject.tag == "Player"){
			enemies_in_range++;
			enemies.Add (_col.gameObject.GetComponent <character_controller>());
		}
	}

	void OnTriggerExit2D(Collider2D _col){
		//don't like tag usage
		if(_col.gameObject.layer == 9 || _col.gameObject.tag == "Player"){
			enemies_in_range--;
			for(int i = 0; i < enemies.Count; i++){
				if(enemies[i] == _col.gameObject.GetComponent <character_controller>()){
					enemies.Remove (_col.gameObject.GetComponent <character_controller> ());
				}
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = draw_color;
		Gizmos.DrawWireSphere (this.transform.position, range);
	}
}
