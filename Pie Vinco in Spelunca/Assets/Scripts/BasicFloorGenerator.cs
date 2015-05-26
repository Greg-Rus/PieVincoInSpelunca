using UnityEngine;
using System.Collections;

public class BasicFloorGenerator : MonoBehaviour {

	public GameObject floorTile;
	public int floorWidth;
	public int floorHeight;
	// Use this for initialization
	void Start () {
		for (int i = 0; i< floorWidth; i++)
		{
			for (int j =0; j< floorHeight; j++)
			{
				GameObject newFloorTile = Instantiate( floorTile, new Vector3(i,j, this.transform.position.z), Quaternion.identity) as GameObject;
				newFloorTile.transform.parent = this.transform;
			}
		}
		Vector3 newFloorCenter = new Vector3(floorWidth *-0.5f, floorHeight *-0.5f, this.transform.position.z);
		this.transform.position = newFloorCenter;
	}
}
