using UnityEngine;
using System.Collections;

public class MainCameraFolow : MonoBehaviour {

	public Transform player;
	private Transform myTransform;
	private Vector3 newCameraPosition;

	
	void Start()
	{
		myTransform=GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		newCameraPosition.Set (player.position.x, player.position.y, myTransform.position.z);
		myTransform.position = newCameraPosition;
	}
}
