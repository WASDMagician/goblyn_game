using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class t_dialogue_editor : EditorWindow {

	List<Rect>windows;

	[MenuItem("Window/Dialogue_Editor")]
	static void Show_Editor(){
		t_dialogue_editor editor = EditorWindow.GetWindow <t_dialogue_editor> ();
		editor.windows = new List<Rect> ();
	}

	void OnGUI(){
		GUILayout.BeginHorizontal (EditorStyles.toolbar);
		Draw_ToolBar ();
		GUILayout.EndHorizontal ();

		BeginWindows ();
		for(int i = 0; i < windows.Count; i++){
			windows [i] = GUI.Window (i, windows[i], Draw_Node_Window, "window " + i.ToString ());
		}
		EndWindows ();
	}

	void Draw_ToolBar(){
		if(GUILayout.Button ("Add Node", EditorStyles.toolbarButton)){
			Add_Window ();
			EditorGUIUtility.ExitGUI ();
		}
		GUILayout.FlexibleSpace ();
	}

	void Add_Window(){
		windows.Add (new Rect(10, 10, 100, 100));
	}

	void Update_Windows(){
		for(int i = 0; i < windows.Count; i++){
			
		}
	}

	void Draw_Node_Window(int id){
		GUI.DragWindow ();
	}
}
