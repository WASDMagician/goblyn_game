using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class t_onclick_exit_to_main : MonoBehaviour {

	public void Exit_To_Main_Menu(){
		SceneManager.LoadScene (0);
	}
}
