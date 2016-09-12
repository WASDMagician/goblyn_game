using UnityEngine;
using System.Collections;

public class cam_track_player : MonoBehaviour {

	public Vector3 camera_offset;
	public GameObject player_object = null;
	public float follow_speed;

	// Use this for initialization
	void Start () {
		//player_object = GameObject.FindGameObjectWithTag ("Player");
		if (r_player_controller.character_controller != null) {
			player_object = r_player_controller.character_controller.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(player_object != null){
			Follow_Player ();
		}
	}

	void Follow_Player(){
		Vector3 new_cam_pos;
		new_cam_pos = Vector3.Lerp (this.transform.position, player_object.transform.position + camera_offset, follow_speed * Time.deltaTime);
		new_cam_pos.z = -10;
		this.transform.position = new_cam_pos;
	}

}
