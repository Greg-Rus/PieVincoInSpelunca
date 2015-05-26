using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

	private float hotizontalMovement;
	private float verticalMovement;
	private Vector2 heading;
	private Vector3 mousePositionInWorldSpace;
	private PlayerController myController;
	
	// Use this for initialization
	void Start () {
		myController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1"))
		{
			Debug.Log ("fire button");
			myController.Attack();
		}
	}
	
	void FixedUpdate()
	{
		hotizontalMovement = Input.GetAxis("Horizontal");
		verticalMovement = Input.GetAxis("Vertical");
		heading = new Vector2(hotizontalMovement, verticalMovement);
		myController.Move(heading);
		mousePositionInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		myController.FaceMouse(mousePositionInWorldSpace);
	}
}
