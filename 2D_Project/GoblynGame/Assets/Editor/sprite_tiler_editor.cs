using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(sprite_tiler))]
public class sprite_tiler_editor : Editor{

	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		sprite_tiler my_script = (sprite_tiler)target;
		if (GUILayout.Button ("Place sprites")){
			my_script.Place_Sprites ();
		}
	}
}
