using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(fileName = "base weapon", menuName = "Custom Objects/Weapon", order=1)]
public class weapon_object : ScriptableObject {
	public enum weapon_types {melee, ranged};
	public weapon_types type;

	public GameObject arrow_prefab;

	public Sprite sprite_image;
	public string name;
	public int damage;
	public int cost;
	public int range;

	public float attack_ignore_animation_start;
	public float attack_animation_damage_duraction;
	public float attack_ignore_animation_end;
}
