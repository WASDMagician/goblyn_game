using UnityEngine;
using System.Collections;

public static class t_bezier_curve_utility : object {

	public enum bezier_control_point_mode { Free, ALigned, Mirrored};

	public static Vector3 Get_Point(Vector3 point_0, Vector3 point_1, Vector3 point_2, Vector3 point_3, float t){
		t = Mathf.Clamp01 (t);
		float one_minus_t = 1f - t;
		return one_minus_t * one_minus_t * one_minus_t * point_0 +
		3f * one_minus_t * one_minus_t * t * point_1 +
		3f * one_minus_t * t * t * point_2 +
		t * t * t * point_3;
	}

	public static Vector3 Get_First_Derivative(Vector3 point_0, Vector3 point_1, Vector3 point_2, Vector3 point_3,float t){
		t = Mathf.Clamp01 (t);
		float one_minus_t = 1f - t;
		return 3f * one_minus_t * one_minus_t * (point_1 - point_0) +
		6f * one_minus_t * t * (point_2 - point_1) +
		3f * t * t * (point_3 - point_2);
	}
}
