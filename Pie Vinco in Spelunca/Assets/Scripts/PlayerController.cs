using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D myRigidBody;
	private Transform myTransform;
	private Vector2 heading = Vector2.zero;
	public float maxSpeed;
	private float angle = 0f;
	private float rotationDirection = 0;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myTransform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Move(Vector2 playerInput)
	{
		myRigidBody.velocity = playerInput * maxSpeed;
	}
	public void FaceMouse(Vector2 mousePosition)
	{
		//Debug.Log("Mouse Pos\t" + mousePosition);
		Debug.Log (myTransform.up);
		Vector2 localSpaceMousePosition = myTransform.InverseTransformPoint(mousePosition);

		if(localSpaceMousePosition.x < 0f) rotationDirection = -1f;
		else if(localSpaceMousePosition.x > 0f) rotationDirection = 1f;
		else rotationDirection = 0f;
		
		Debug.DrawLine(myTransform.position, myTransform.TransformPoint(myTransform.up) *-1f,Color.green);
		Debug.DrawLine(myTransform.position, mousePosition, Color.blue);
		
		float angleDelta = Vector2.Angle( (Vector2)myTransform.up *-1f , (Vector2) myTransform.position - (Vector2)mousePosition ) ;
		
		if(angleDelta > 0.1f)
		{
			angle = angle + angleDelta * rotationDirection;
		}
		
		if(angle > 360f) angle = angle - 360f;
		else if (angle <0f) angle = angle + 360f;
		
		Debug.Log ("Rot Dir\t" + rotationDirection);
		Debug.Log("angle Delta\t" + angleDelta);
		Debug.Log("angle \t" + angle);
		
		//myRigidBody.MoveRotation(angle);
	}
}
