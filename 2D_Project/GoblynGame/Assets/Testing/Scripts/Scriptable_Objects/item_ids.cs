using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[CreateAssetMenu(fileName = "Item_List", menuName = "Custom Objects/Item_ID_List", order=1)]
public class item_ids : ScriptableObject {
	[System.Serializable]
	public class item{
		public string item_name;
		public int item_id;
		public GameObject associated_prefab;
	}

	public item[] items;
}
