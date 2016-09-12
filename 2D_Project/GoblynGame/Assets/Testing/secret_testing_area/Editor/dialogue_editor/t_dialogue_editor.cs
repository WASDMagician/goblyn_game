using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Page{
	public string page_message;
}

public class t_dialogue_editor : EditorWindow {

	List<Rect> windows;
	List<Page> pages;

	[MenuItem("Window/Dialogue Editor")]
	static void Show_Editor(){
		t_dialogue_editor editor = EditorWindow.GetWindow <t_dialogue_editor> ();
		editor.Init ();
	}

	public void Init(){
		windows = new List<Rect> ();
	}

	void OnGUI(){
		if(GUILayout.Button ("Add Page")){
			Add_Window ();
		}
		if(GUILayout.Button ("Save Dialogue Box")){
			Save_Dialogue_Box ();
		}

		if (windows.Count > 0) {
			Draw_Windows ();
		}
	}

	void Add_Window(){
		windows.Add (new Rect(10, 10, 100, 100));
		pages.Add (new Page());
	}

	void Draw_Windows(){
		BeginWindows ();
		for(int i = 0; i < windows.Count; i++){
			windows [i] = GUI.Window (i, windows [i], Draw_Window, "Window " + i.ToString ());
		}
		EndWindows ();
	}
		
	void Draw_Window(int id){
		GUI.DragWindow ();
	}

	void Save_Dialogue_Box(){
		
	}
}
