using UnityEngine;
using System.Collections;

public class t_bezier_curve : MonoBehaviour {

	public Vector3[] points;

	public void Reset(){
		points = new Vector3[] {
			new Vector3 (1f, 0f, 0f),
			new Vector3 (2f, 0f, 0f),
			new Vector3 (3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};
	}

	public Vector3 Get_Point(float t){
		return transform.TransformPoint (t_bezier_curve_utility.Get_Point (points [0], points [1], points [2], points[3], t));
	}

	private Vector3 Get_Velocity(float t){
		return transform.TransformPoint (t_bezier_curve_utility.Get_First_Derivative (points [0], points [1], points [2], points[3], t)) - transform.position;
	}

	public Vector3 Get_Direction(float t){
		return Get_Velocity (t).normalized;
	}

}
