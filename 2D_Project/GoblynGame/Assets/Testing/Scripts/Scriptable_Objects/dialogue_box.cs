using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue Box", menuName = "Custom Objects/Dialogue", order=1)]
public class dialogue_box : ScriptableObject {
	[System.Serializable]
	public class dialogue_page{
		[System.Serializable]
		public class dialogue_options{
			[Multiline]
			public string button_text;
			public int go_to_page;
			public enum actions {none, modify_health, modify_gold, modify_teeth, give_item};
			public actions action;
			public int action_value;
		}
		[Multiline]
		public string page_text;
		public dialogue_options[] option_buttons;
	}

	public dialogue_page[] pages;
}
