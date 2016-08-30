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
		data.Save_Position (t_player_controller.player_controller.transform.position);
		data.Save_Level (SceneManager.GetActiveScene ().buildIndex);
		data.Save_Weapon_ID (t_player_controller.player_controller.Get_Weapon_ID ());
		data.Save_Armor_ID (t_player_controller.player_controller.Get_Armor_ID());
		data.Save_Gold (t_player_controller.player_controller.Get_Gold ());
		data.Save_Teeth (t_player_controller.player_controller.Get_Teeth ());
		data.Save_Max_Health (t_player_controller.player_controller.Get_Max_Health ());
		data.Save_Health (t_player_controller.player_controller.Get_Health ());
		binary_formatter.Serialize (file, data);

		file.Close ();
		
	}

	public void Load(){
		if(File.Exists (Application.persistentDataPath + "/save_game.data")){
			BinaryFormatter binary_formatter = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/save_game.data", FileMode.Open);
			Save_Data data = (Save_Data)binary_formatter.Deserialize (file);
			SceneManager.LoadScene (data.Load_Level_Index ());
			t_player_controller.player_controller.gameObject.transform.position = data.Load_Position ();
			t_player_controller.player_controller.Load_Weapon (data.Load_Weapon_ID ());
			t_player_controller.player_controller.Load_Armor (data.Load_Armor_ID ());
			t_player_controller.player_controller.Set_Gold (data.Load_Gold ());
			t_player_controller.player_controller.Set_Teeth (data.Load_Teeth ());
			t_player_controller.player_controller.Set_Max_Health (data.Load_Max_Health ());
			t_player_controller.player_controller.Set_Health (data.Load_Health ());
			t_player_controller.player_controller.Initialise (data.Load_Weapon_ID (), data.Load_Armor_ID ());

			Time.timeScale = 1;
			file.Close ();
		}
	}

	void Load_Game(){
		
	}
}

[System.Serializable]
class Save_Data{

	float x_position, y_position, z_position;
	int level_index;
	int weapon_id;
	int armor_id;
	int max_health;
	int current_health;
	int gold;
	int teeth;

	public void Save_Position(Vector3 _position){
		x_position = _position.x;
		y_position = _position.y;
		z_position = _position.z;
	}

	public Vector3 Load_Position(){
		Vector3 position = new Vector3 (x_position, y_position, z_position);
		return position;
	}

	public void Save_Level(int _level){
		level_index = _level;
	}

	public int Load_Level_Index(){
		return level_index;
	}

	public void Save_Weapon_ID(int _weapon_id){
		weapon_id = _weapon_id;
	}

	public int Load_Weapon_ID(){
		return weapon_id;
	}

	public void Save_Armor_ID(int _armor_id){
		armor_id = _armor_id;
	}

	public int Load_Armor_ID(){
		return armor_id;
	}

	public void Save_Max_Health(int _max_health){
		max_health = _max_health;
	}

	public int Load_Max_Health(){
		return max_health;
	}

	public void Save_Health(int _health){
		current_health = _health;
	}

	public int Load_Health(){
		return current_health;
	}

	public void Save_Gold(int _gold){
		gold = _gold;
	}

	public int Load_Gold(){
		return gold;
	}

	public void Save_Teeth(int _teeth){
		teeth = _teeth;
	}

	public int Load_Teeth(){
		return teeth;
	}
}
