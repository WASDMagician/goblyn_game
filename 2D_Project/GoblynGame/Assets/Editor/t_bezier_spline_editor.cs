using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(t_bezier_spline))]
public class t_bezier_splite_editor
: Editor {

	private t_bezier_spline spline;
	private Transform handle_transform;
	private Quaternion handle_rotation;

	private const int line_steps = 10;
	private const float direction_scale = 0.5f;
	private const int steps_per_curve = 10;
	private const float handle_size = 0.04f;
	private const float pick_size = 0.06f;

	private int selected_index = -1;

	private static Color[] mode_colors = {
		Color.white,
		Color.yellow,
		Color.cyan
	};

	public override void OnInspectorGUI ()
	{
		spline = target as t_bezier_spline;
		EditorGUI.BeginChangeCheck ();
		bool loop = EditorGUILayout.Toggle ("Loop", spline.Loop);
		if(EditorGUI.EndChangeCheck ()){
			Undo.RecordObject (spline, "Toggle Loop");
			EditorUtility.SetDirty (spline);
			spline.Loop = loop;
		}

		if (selected_index >= 0 && selected_index < spline.Get_Point_Count) {
			Draw_Selected_Point_In_Inspector ();
		}
		if(GUILayout.Button ("Add Curve")){
			Undo.RecordObject (spline, "Add Curve");
			spline.Add_Curve ();
			EditorUtility.SetDirty (spline);
		}
		if(GUILayout.Button ("Remove Curve")){
			Undo.RecordObject (spline, "Remove Curve");
			spline.Remove_Curve ();
			EditorUtility.SetDirty (spline);
		}

	}

	private void Draw_Selected_Point_In_Inspector(){
		GUILayout.Label ("Selected Point");
		EditorGUI.BeginChangeCheck ();
		Vector3 point = EditorGUILayout.Vector3Field ("Position", spline.Get_Control_Point (selected_index));
		if(EditorGUI.EndChangeCheck ()){
			Undo.RecordObject (spline, "Move Point");
			EditorUtility.SetDirty (spline);
			spline.Set_Control_Point (selected_index, point);
		}

		EditorGUI.BeginChangeCheck ();
		bezier_control_point_mode mode = (bezier_control_point_mode)
			EditorGUILayout.EnumPopup ("Mode", spline.Get_Control_Point_Mode (selected_index));
		if(EditorGUI.EndChangeCheck ()){
			Undo.RecordObject (spline, "Change Point Mode");
			spline.Set_Control_Point_Mode (selected_index, mode);
			EditorUtility.SetDirty (spline);
		}

	}

	private void OnSceneGUI(){
		spline = target as t_bezier_spline;
		handle_transform = spline.transform;
		handle_rotation = Tools.pivotRotation == PivotRotation.Local ?
			handle_transform.rotation : Quaternion.identity;

		Vector3 point_0 = Show_Point (0);
		for (int i = 1; i < spline.Get_Point_Count; i += 3) {
			Vector3 point_1 = Show_Point (i);
			Vector3 point_2 = Show_Point (i + 1);
			Vector3 point_3 = Show_Point (i + 2);

			Handles.color = Color.grey;
			Handles.DrawLine (point_0, point_1);
			Handles.DrawLine (point_1, point_2);
			//should draw point 3 here?

			Handles.DrawBezier (point_0, point_3, point_1, point_2, Color.white, null, 2f);
			point_0 = point_3;
		}
		Show_Directions ();

	}

	private Vector3 Show_Point(int index){
		Vector3 point = handle_transform.TransformPoint (spline.Get_Control_Point (index));

		float size = HandleUtility.GetHandleSize (point);
		if(0 == index){
			size *= 4f;
		}
		Handles.color = mode_colors [(int)spline.Get_Control_Point_Mode (index)];

		if(Handles.Button (point, handle_rotation, size * handle_size, size * pick_size, Handles.DotCap )){
			selected_index = index;
			Repaint ();
		}
		if (selected_index == index) {
			EditorGUI.BeginChangeCheck ();
			point = Handles.DoPositionHandle (point, handle_rotation);
			if (EditorGUI.EndChangeCheck ()) {
				Undo.RecordObject (spline, "Move  Point");
				EditorUtility.SetDirty (spline);
				spline.Set_Control_Point (index, handle_transform.InverseTransformPoint (point));
			}
		}
		return point;
	}

	private void Show_Directions(){
		Handles.color = Color.green;
		Vector3 point = spline.Get_Point (0f);
		Handles.DrawLine (point, point + spline.Get_Direction (0f) * direction_scale);
		int steps = steps_per_curve * spline.Curve_Count;
		for(int i = 1; i < steps; i++){
			point = spline.Get_Point (i / (float)steps);
			Handles.DrawLine (point, point + spline.Get_Direction(i / (float)steps) * direction_scale);
		}
	}
}
