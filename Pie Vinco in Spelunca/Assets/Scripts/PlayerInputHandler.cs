using UnityEngine;
using System.Collections;

public class PlayerInputHandler : MonoBehaviour {

	private float hotizontalMovement;
	private float verticalMovement;
	private Vector2 heading;
	private Vector3 mousePositionInWorldSpace;
	public PlayerController myController;
	
	// Use this for initialization
	void Start () {
		myController = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
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
