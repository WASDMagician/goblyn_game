using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class load_save_game : MonoBehaviour {

	public static load_save_game load_save_controller;

	void Start(){
		DontDestroyOnLoad (this.gameObject);
		if(null == load_save_controller){
			load_save_controller = this;
		}
		else if(this != load_save_controller){
			Destroy (this.gameObject);
		}
	}

	public void Save(){
		BinaryFormatter binary_formatter = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/save_game.data");
		Save_Data data = new Save_Data ();
		data.Save_Position (r_player_controller.character_controller.transform.position);
		data.Save_Level (SceneManager.GetActiveScene ().buildIndex);
		data.Save_Weapon_ID (r_player_controller.character_controller.Get_Weapon_ID ());
		data.Save_Armor_ID (r_player_controller.character_controller.Get_Armor_ID());
		data.Save_Gold (r_player_controller.character_controller.Get_Gold ());
		data.Save_Teeth (r_player_controller.character_controller.Get_Teeth ());
		data.Save_Max_Health (r_player_controller.character_controller.Get_Max_Health ());
		data.Save_Health (r_player_controller.character_controller.Get_Health ());
		binary_formatter.Serialize (file, data);

		file.Close ();
		
	}

	public void Load(){
		if(File.Exists (Application.persistentDataPath + "/save_game.data")){
			BinaryFormatter binary_formatter = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/save_game.data", FileMode.Open);
			Save_Data data = (Save_Data)binary_formatter.Deserialize (file);
			StartCoroutine (Load_Level (data));
			Time.timeScale = 1;
			file.Close ();
		}
	}

	IEnumerator Load_Level(Save_Data _data){
		AsyncOperation scene_loading = SceneManager.LoadSceneAsync (_data.Load_Level_Index ());
		while(false == scene_loading.isDone){
			yield return new WaitForSeconds (0);
		}
		StartCoroutine (On_Scene_Load (_data));
	}

	IEnumerator On_Scene_Load(Save_Data data){
		while(null == r_player_controller.character_controller){
			yield return new WaitForSeconds (0);
		}
		r_player_controller.character_controller.gameObject.transform.position = data.Load_Position ();
		r_player_controller.character_controller.Set_Weapon (data.Load_Weapon_ID ());
		r_player_controller.character_controller.Set_Armor (data.Load_Armor_ID ());
		r_player_controller.character_controller.Set_Gold (data.Load_Gold ());
		r_player_controller.character_controller.Set_Teeth (data.Load_Teeth ());
		r_player_controller.character_controller.Set_Max_Health (data.Load_Max_Health ());
		r_player_controller.character_controller.Set_Health (data.Load_Health ());
		r_player_controller.character_controller.Switch_States (r_player_controller.states.idle);
		r_player_controller.character_controller.Update_UI ();
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
