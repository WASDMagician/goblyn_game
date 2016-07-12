using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class weapon_controller : MonoBehaviour {

	bool is_attacking;

	public enum weapons {hand, club, spiked_club, dagger, short_sword, sword, long_sword, bow, old_bow};
	public enum attack_methods {melee, ranged};

	attack_methods[] attack_types;
	float[] attack_ranges;
	int[] damage_amounts;

	//terrible naming here
	//animation starts, ignore amount, after x time do damage, at next time stop doing damage
	float[] attack_ignore_animation_start; //how much animation to ignore at start
	float[] attack_animation_damage_time; // which part of the animation to begin doing damage
	float[] attack_ignore_animation_end; //how much animation to ignore at end


	public weapons weapon;
	public attack_methods attack_method;

	public float range;
	public int damage;

	public float attack_ignore_start; //example: sword raise
	public float attack_do_damage; //example: sword swipe down
	public float attack_ignore_end; //example: return to idle

	float total_attack_time;

	float attack_start_time;

	CircleCollider2D collider;

	Color draw_color;


	float enemies_in_range;
	public List<character_controller> enemies;

	// Use this for initialization
	void Start () {
		attack_types = new attack_methods[] {
			attack_methods.melee, attack_methods.melee, attack_methods.melee, attack_methods.melee, attack_methods.melee, 
			attack_methods.melee, attack_methods.melee, attack_methods.ranged, attack_methods.ranged
		};
		attack_ranges = new float[] {1, 2, 3, 4, 5, 6, 7, 8 };
		damage_amounts = new int[] { 9, 10, 11, 12, 13, 14, 15, 16 };

		attack_ignore_animation_start = new float[] { 1, 1, 1, 1, 1, 1, 1, 1 }; //do no damage for this long
		attack_animation_damage_time = new float[] { 1, 1, 1, 1, 1, 1, 1, 1 }; //damage at this point
		attack_ignore_animation_end = new float[] { 1, 1, 1, 1, 1, 1, 1, 1}; //stop doing damage at this point
		

		collider = GetComponent <CircleCollider2D> ();

		Set_Weapon ();

		enemies = new List<character_controller> ();

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Set_Weapon(){
		attack_method = attack_types [(int)weapon];
		range = attack_ranges [(int)weapon];
		damage = damage_amounts [(int)weapon];
		collider.radius = range;

		attack_ignore_start = attack_ignore_animation_start [(int)weapon];
		attack_do_damage = attack_animation_damage_time [(int)weapon];
		attack_ignore_end = attack_ignore_animation_end [(int)weapon];

		total_attack_time = attack_ignore_start + attack_do_damage + attack_ignore_end;
	}

	//fix this shit
	public IEnumerator Start_Attack(){
		if (!is_attacking) {
			draw_color = Color.green;
			attack_start_time = Time.fixedTime;
			is_attacking = true;
			yield return new WaitForSeconds (attack_ignore_start);
			StartCoroutine (Attack ());
		}
	}

	//eeew oh god oh god oh god whyyyy
	public IEnumerator Attack(){
		draw_color = Color.red;
		for(int i = 0; i < enemies.Count; i++){
			enemies [i].is_invulnerable = false;
		}
		while(Time.fixedTime < attack_start_time + attack_ignore_start + attack_do_damage){
			for (int i = 0; i < enemies.Count; i++) {
				enemies [i].Remove_Health (damage);
				enemies [i].is_invulnerable = true;
			}
			yield return new WaitForSeconds (0);
		}
		StartCoroutine (End_Attack ());
	}

	//meh
	public IEnumerator End_Attack(){
		draw_color = Color.blue;
		yield return new WaitForSeconds (attack_ignore_end);
		print ("Done now");
		is_attacking = false;
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
