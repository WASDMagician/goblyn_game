using UnityEngine;
using UnityEditor;
using System.Collections;

public static class sprite_tiler_menu_object{
	[MenuItem("GameObject/2D Object/Sprite Tiler", false, 0)]
	static void Create_Tiler(){
		GameObject ob = (GameObject)AssetDatabase.LoadAssetAtPath ("Assets/Custom_Objects/sprite_tiler.prefab", typeof(GameObject)) as GameObject;
		PrefabUtility.InstantiatePrefab (ob);
	}
}