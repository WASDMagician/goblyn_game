using UnityEngine;
using System.Collections;

public static class r_spline_walking_utility : object {

	private static t_bezier_spline spline;
	private static Rigidbody2D walker_rigidbody;
	private static float duration;
	private static float progress;
	private static bool variables_set;

	public static void Set(t_bezier_spline _spline, Rigidbody2D _walker_rigidbody, float _duration, float _progress){
		Debug.Log ("Set spline walking utility");
		if (false == variables_set) {
			spline = _spline;
			walker_rigidbody = _walker_rigidbody;
			duration = _duration;
			progress = _progress;
			variables_set = true;
		}
	}

	public static void Reset(){
		Debug.Log ("Reset spline walking utility");
		spline = null;
		walker_rigidbody = null;
		duration = 0;
		progress = 0;
		variables_set = false;
	}

	public static void Move_Up_Bezier_Spline(){
		progress += Time.deltaTime / duration;
		Vector3 position = spline.Get_Point (progress);
		walker_rigidbody.MovePosition (position);
	}

	public static void Move_Down_Bezier_Spline(){
		try{
			progress -= Time.deltaTime / duration;
			Vector3 position = spline.Get_Point (progress);
			walker_rigidbody.MovePosition (position);
		}
		catch(System.Exception e){
			Debug.Log ("Rigidbody: " + walker_rigidbody);
			Debug.Log ("PRogress: " + progress);
			Debug.Log ("Spline: " + spline);
		}
	}
}
