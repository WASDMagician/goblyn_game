using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class t_spline_walker : MonoBehaviour {

	public enum spline_walker_modes {once, loop, ping_pong};
	public spline_walker_modes walker_mode;
	private bool moving_forward = true;
	protected bool can_move = true;
	public t_bezier_spline spline;
	public float duration;
	private float progress;
	Rigidbody2D walker_rigidbody;

	private Vector3 position;

	void Start(){
		Init ();
	}

	protected void Init(){
		walker_rigidbody = GetComponent <Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if (true == can_move) {
			Move ();
		}
	}

	public virtual void Move(){
		if (true == moving_forward) {
			progress += Time.deltaTime / duration;
			if (progress > 1f) {
				if (spline_walker_modes.once == walker_mode) {
					progress = 1f;
				} else if (spline_walker_modes.loop == walker_mode) {
					progress = 0f; //tutorial sets this to -1f
				} else if (spline_walker_modes.ping_pong == walker_mode) {
					progress = 2f - progress;
					moving_forward = false;
				}
			}
		}
		else{
			progress -= Time.deltaTime / duration;
			if(0f > progress){
				progress = -progress;
				moving_forward = true;
			}
		}

		position = spline.Get_Point (progress);
		walker_rigidbody.MovePosition (position);
	}
}
