using UnityEngine;
using System.Collections;

public class spawn_canvas_element : MonoBehaviour {

	GameObject object_to_spawn = null;

	public void Set_Object(GameObject _object_to_spawn){
		object_to_spawn = _object_to_spawn;
	}

	public void Load_Object(GameObject _object_to_spawn){
		object_to_spawn = _object_to_spawn;
		Load_Object ();
	}

	public void Load_Object(){
		Delete_Current_Child ();
		GameObject _gameobject = Instantiate (object_to_spawn) as GameObject;
		_gameobject.transform.parent = this.transform;
	}

	void Delete_Current_Child(){
		if(this.transform.childCount > 0){
			Destroy (this.transform.GetChild (0).gameObject);
		}
	}
}
