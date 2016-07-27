using UnityEngine;
using UnityEditor;
using System.Collections;

public class sprite_tiler : MonoBehaviour {

	public Sprite sprite_to_tile;

	public int x_iterations;
	public int y_iterations;

	float sprite_width;
	float sprite_height;

	public bool has_collider;


	void Start(){
		
	}

	void Calculate_Height_And_Width(){
		sprite_width = sprite_to_tile.bounds.extents.x * 2;
		sprite_height = sprite_to_tile.bounds.extents.y * 2;
	}

	GameObject Generate_Sprite_Gameobject(){
		GameObject sprite_object = new GameObject ();
		Undo.RegisterCreatedObjectUndo (sprite_object, "Created base object");

		SpriteRenderer sprite_renderer = sprite_object.AddComponent <SpriteRenderer> ();
		sprite_renderer.sprite = sprite_to_tile;
		return sprite_object;
	}

	public void Place_Sprites(){
		if (sprite_to_tile != null && (x_iterations != 0 && y_iterations != 0)) {

			Calculate_Height_And_Width ();

			GameObject base_object = Generate_Sprite_Gameobject ();
			Undo.RegisterCreatedObjectUndo (base_object, "Created base object");

			for (int r = 0; r < y_iterations; r++) {
				for (int c = 0; c < x_iterations; c++) {
					Vector3 base_position = this.transform.position;
					base_position.x += c * sprite_width;
					base_position.y += r * sprite_height;
					GameObject _object = Instantiate (base_object, base_position, Quaternion.identity) as GameObject;
					Undo.RegisterCreatedObjectUndo (_object, "Created base object");
					_object.transform.parent = this.transform;
				}
			}

			DestroyImmediate (base_object);
		}
	}
}
