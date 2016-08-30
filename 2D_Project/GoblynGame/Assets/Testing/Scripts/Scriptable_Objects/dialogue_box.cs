using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue Box", menuName = "Custom Objects/Dialogue", order=1)]
public class dialogue_box : ScriptableObject {
	[System.Serializable]
	public class dialogue_page{
		//maybe look into how to restrict string length
		[TextArea(1, 10)]
		public string dialogue_area;
		[TextArea(1, 3)]
		public string answer_one;
		[TextArea(1, 3)]
		public string answer_two;

		[Space(10)]
		public int answer_one_page_index = -1;
		public int answer_one_action_index = 0;
		[Space(10)]
		public int answer_two_page_index = -1;
		public int answer_two_action_index = 0;
		[Space(10)]
		[Header("Amount of each item to give, depends on action index")]
		public int teeth_amount;
		public int gold_amount;
		public int health_amount;

		public UnityAction my_action;
	}
	public dialogue_page[] pages;
}
