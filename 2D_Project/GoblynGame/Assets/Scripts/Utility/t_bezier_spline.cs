using UnityEngine;
using System.Collections;
using System;

public enum bezier_control_point_mode{
	Free, Aligned, Mirrored
};

public class t_bezier_spline : MonoBehaviour {

	[SerializeField]
	private Vector3[] points;

	[SerializeField]
	private bezier_control_point_mode[] modes;

	[SerializeField]
	private bezier_control_point_mode mode;

	[SerializeField]
	private bool loop;

	public bool Loop{
		get{
			return loop;
		}
		set{
			loop = value;
			if(true == value){
				modes [modes.Length - 1] = modes [0];
				Set_Control_Point (0, points[0]);
			}
		}
	}

	public int Get_Point_Count{
		get{
			return points.Length;
		}
	}

	public Vector3 Get_Control_Point(int index){
		return points [index];
	}

	public void Set_Control_Point(int index, Vector3 point){
		int mode_index = (index + 1) / 3;
		modes [mode_index] = mode;
		if(true == loop){
			if(0 == mode_index){
				modes [modes.Length - 1] = mode;
			}
			else if(modes.Length - 1 == mode_index){
				modes [0] = mode;
			}
		}

		if(index % 3 == 0){
			Vector3 delta = point - points [index];

			if (true == loop) {
				if (0 == index) {
					points [1] += delta;
					points [points.Length - 2] += delta;
					points [points.Length - 1] = point;
				} else if (points.Length - 1 == index) {
					points [0] = point;
					points [1] += delta;
					points [index - 1] += delta;
				} else {
					points [index - 1] += delta;
					points [index + 1] += delta;
				}
			} else {

				if (0 < index) {
					points [index - 1] += delta;
				}
				if (points.Length > index + 1) {
					points [index + 1] += delta;
				}
			}
		}
		points [index] = point;
		Enforce_Mode (index);
	}

	public void Reset(){
		points = new Vector3[] {
			new Vector3 (1f, 0f, 0f),
			new Vector3 (2f, 0f, 0f),
			new Vector3 (3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};

		modes = new bezier_control_point_mode[] {
			bezier_control_point_mode.Free,
			bezier_control_point_mode.Free
		};
	}

	public Vector3 Get_Point(float t){
		int i;
		if(t >= 1f){
			t = 1f;
			i = points.Length - 4;
		}
		else{
			t = Mathf.Clamp01 (t) * Curve_Count;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint (t_bezier_curve_utility.Get_Point (points [i], points [i + 1], points [i + 2], points [i + 3], t));
	}

	private Vector3 Get_Velocity(float t){
		int i;
		if(t >= 1f){
			t = 1f;
			i = points.Length - 4;
		}
		else{
			t = Mathf.Clamp01 (t) * Curve_Count;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint (t_bezier_curve_utility.Get_First_Derivative (points [i], points [i + 1], points [i + 2], points [i + 3], t) - transform.position);
	}

	public Vector3 Get_Direction(float t){
		return Get_Velocity (t).normalized;
	}

	public void Add_Curve(){
		Vector3 point = points [points.Length - 1];
		Array.Resize (ref points, points.Length + 3);
		point.x += 1f;
		points [points.Length - 3] = point;
		point.x += 1f;
		points [points.Length - 2] = point;
		point.x += 1f;
		points [points.Length - 1] = point;

		Array.Resize (ref modes, modes.Length + 1);
		modes [modes.Length - 1] = modes [modes.Length - 2];

		Enforce_Mode (points.Length - 4);

		if(true == loop){
			points [points.Length - 1] = points [0];
			modes [modes.Length - 1] = modes [0];
			Enforce_Mode (0);
		}
	}

	public void Remove_Curve(){
		if (points.Length > 3) {
			Vector3 point = points [points.Length - 4];
			Array.Resize (ref points, points.Length - 3);
		}
	}

	public int Curve_Count {
		get{
			return (points.Length - 1) / 3;
		}
	}

	public bezier_control_point_mode Get_Control_Point_Mode(int index){
		return modes [(index + 1) / 3];
	}

	public void Set_Control_Point_Mode(int index, bezier_control_point_mode mode){
		modes [(index + 1) / 3] = mode;
		Enforce_Mode (index);
	}

	private void Enforce_Mode(int index){
		int mode_index = (index + 1) / 3;
		bezier_control_point_mode mode = modes [mode_index];
		if(bezier_control_point_mode.Free == mode || false == loop && (0 == mode_index || modes.Length - 1 == mode_index)){
			return;
		}

		int middle_index = mode_index * 3;
		int fixed_index, enforced_index;
		if(index <= middle_index){
			fixed_index = middle_index - 1;
			if(0 > fixed_index){
				fixed_index = points.Length - 2;
			}
			enforced_index = middle_index + 1;
			if(points.Length <= enforced_index){
				enforced_index = 1;
			}
		}
		else{
			fixed_index = middle_index + 1;
			if(points.Length <= fixed_index){
				fixed_index = 1;
			}
			enforced_index = middle_index - 1;
			if(0 > enforced_index){
				enforced_index = points.Length - 2;
			}
		}

		Vector3 middle = points [middle_index];
		Vector3 enforced_tangent = middle - points [fixed_index];
		if(bezier_control_point_mode.Aligned == mode){
			enforced_tangent = enforced_tangent.normalized * Vector3.Distance (middle, points [enforced_index]);
		}
		points [enforced_index] = middle + enforced_tangent;
	}
		

}
