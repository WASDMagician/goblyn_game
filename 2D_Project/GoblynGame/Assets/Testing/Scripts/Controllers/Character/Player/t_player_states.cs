using UnityEngine;
using System.Collections;

public static class t_player_states : object {
	public enum states {free_moving, in_menu, in_dialogue, in_shop};
	static states state = states.free_moving;
	static states previous_state = states.free_moving;

	public static bool Is_Free_Moving(){
		return states.free_moving == state;
	}

	public static void Set_Free_Moving(){
		Set_Previous_State ();
		state = states.free_moving;
	}

	public static bool Is_In_Menu(){
		return states.in_menu == state;
	}

	public static void Set_In_Menu(){
		Set_Previous_State ();
		state = states.in_menu;
	}

	public static bool Is_In_Dialogue(){
		return states.in_dialogue == state;
	}

	public static void Set_In_Dialogue(){
		Set_Previous_State ();
		state = states.in_dialogue;
	}

	public static bool Is_In_Shop(){
		return states.in_shop == state;
	}

	public static void Set_In_Shop(){
		Set_Previous_State ();
		state = states.in_shop;
	}

	public static states Get_Current_State(){
		return state;
	}

	private static void Set_Previous_State(){
		previous_state = state;
	}

	public static void Return_To_Previous_State(){
		state = previous_state;
	}

}
