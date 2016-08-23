using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class load_save_game : MonoBehaviour {
	
	public void Save(){
		BinaryFormatter binary_formatter = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/save_game.data");
		Save_Data data = new Save_Data ();
		data.Save_Position (player_controller.player_object.gameObject.transform.position);
		data.Save_Level_Index (SceneManager.GetActiveScene ().buildIndex);
		binary_formatter.Serialize (file, data);
		file.Close ();
		
	}

	public void Load(){
		if(File.Exists (Application.persistentDataPath + "/save_game.data")){
			BinaryFormatter binary_formatter = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/save_game.data", FileMode.Open);
			Save_Data data = (Save_Data)binary_formatter.Deserialize (file);
			player_controller.player_object.transform.position = data.Get_Position ();
			SceneManager.LoadScene (data.Get_Level_Index ());
			file.Close ();
		}
	}

	void Load_Game(){
		
	}
}

[System.Serializable]
class Save_Data{

	float x_position;
	float y_position;
	float z_position;

	int level_index;

	public void Save_Position(Vector3 _position){
		x_position = _position.x;
		y_position = _position.y;
		z_position = _position.z;
	}

	public Vector3 Get_Position(){
		Vector3 position = new Vector3 (x_position, y_position, z_position);
		return position;
	}

	public void Save_Level_Index(int _level){
		level_index = _level;
	}

	public int Get_Level_Index(){
		return level_index;
	}
}
