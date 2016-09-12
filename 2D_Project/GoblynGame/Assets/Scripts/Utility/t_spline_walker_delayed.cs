using UnityEngine;
using System.Collections;

public class t_spline_walker_delayed : t_spline_walker {

	public float time_offset;

	// Use this for initialization
	void Start () {
		Init ();
		can_move = false;
		StartCoroutine (Start_Moving ());
	}

	IEnumerator Start_Moving(){
		yield return new WaitForSeconds (time_offset);
		can_move = true;
	}
}
