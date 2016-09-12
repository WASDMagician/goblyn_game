using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class item_codes : object {

	public static Dictionary<int, string> game_items = new Dictionary<int, string> {
		{0, "Characters/Player/player"},
		{1, "Items/Weapons/bone_dagger"},
		{2, "Items/Weapons/sword"},
		{3, "Items/Weapons/bow"},
		{11, "Items/Armor/goblin_armor"},
		{12, "Items/Armor/peasant_armor"},
		{13, "Items/Armor/knight_armor"}
	};

	public static Dictionary<int, string> game_item_names = new Dictionary<int, string> () {
		{ 0, "Player"},
		{ 1, "Bone Dagger" },
		{ 2, "Sword" },
		{ 3, "Bow" },
		{ 11, "Goblin Armor" },
		{ 12, "Peasant Armor" },
		{ 13, "Knight Armor" },
	};

	public static Dictionary<string, int> item_name_index = new Dictionary<string, int> () {
		{ "player", 0 },
		{ "bone_dagger", 1 },
		{ "sword", 2 },
		{ "bow", 3 },
		{ "goblin_armor", 11 },
		{ "peasant_armor", 12 },
		{ "knight_armor", 13 }
	};
}
