using UnityEngine;
using System.Collections;

public class EnemyMotor : MonoBehaviour {

	public Vector2 destination;
	public int maxSpeed;
	public float closeEnoughDistance;
	
	private Rigidbody2D myRigidBody;
	private Transform myTransform;
	private Animator myAnimator;
	public bool destinationReached;
	
	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D>();
		myTransform = GetComponent<Transform>();
		myAnimator = GetComponent<Animator>();
		
		destinationReached = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!destinationReached)
		{
			Move ();
		}
	}
	
	private void Move()
	{
		if(CloseEnough())
		{
			myRigidBody.velocity = Vector2.zero;
			myAnimator.SetFloat("speed",myRigidBody.velocity.magnitude);
			destinationReached = true;
			
		}
		else
		{
			myRigidBody.MoveRotation(Utility.LookAtAngle2D(myTransform.position,destination));
			myRigidBody.AddForce((destination - (Vector2)myTransform.position).normalized * maxSpeed);
			myAnimator.SetFloat("speed",myRigidBody.velocity.magnitude);
		}
	}
	
	private bool CloseEnough()
	{
		float distanceToDestination = (destination - (Vector2)myTransform.position).magnitude;
		if(distanceToDestination <= closeEnoughDistance) return true;
		else return false;
	}
	
	public void MoveToDestination(Vector2 newDestination)
	{
		destinationReached = false;
		destination = newDestination;
	}


	
}
