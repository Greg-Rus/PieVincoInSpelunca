using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Rigidbody2D myRigidBody;
	private Transform myTransform;
	private Vector2 heading = Vector2.zero;
	public float maxSpeed;
	public float maxVelocity;
	public float stoppingPower;
	private float angle = 0f;
	private float rotationDirection = 0;
//	private float movementDrag;
	public float restingDragMultiplyer;
	private Animator myAnimator;
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myTransform = GetComponent<Transform>();
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Move(Vector2 playerInput)
	{
		//myRigidBody.velocity = playerInput * maxSpeed;
		
		if(playerInput.magnitude >= 0f && myRigidBody.velocity.magnitude <= maxVelocity)
		{
			myRigidBody.AddForce(playerInput * maxSpeed);
		}
/*		else if(playerInput.magnitude == 0f && myRigidBody.velocity.magnitude >0f)
		{
			myRigidBody.AddForce(myRigidBody.velocity * -restingDragMultiplyer);
		}
		
		Debug.Log (myRigidBody.velocity.magnitude);
*/		
		myAnimator.SetFloat("speed", playerInput.magnitude);
	}
	public void FaceMouse(Vector2 mousePosition)
	{
		myRigidBody.MoveRotation(Utility.LookAtAngle2D(myTransform.position,mousePosition));
	}
	public void Attack()
	{
		myAnimator.SetTrigger("attack");
	}
}
