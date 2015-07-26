using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestActor : MonoBehaviour {
	public GameObject floor;
	public GameObject wall;
	private PathFindier testPathFinder;
	private List<Vector2> path;
	private LineRenderer myLineRenderer;
	                      //0,1,2,3,4,5,6
	private int[,] map =  {{9,0,0,0,0,0,0},//0
		                   {0,1,1,0,0,0,0},//1
		                   {0,1,0,0,0,1,0},//2
		                   {0,0,0,8,0,1,9},//3
		                   {0,0,0,0,0,1,0},//4
		                   {0,0,0,0,0,0,0},//5
		                   {0,0,0,0,0,0,0} //6
		                   };
	// Use this for initialization
	void Start () {
	//Incarnate objects for testing
		myLineRenderer = GetComponent<LineRenderer>();
		generateLevel(map);
		testPathFinder = new PathFindier(map);
		Debug.Log(testPathFinder);
		testGetPaht();
	
	}
	
	void testGetPaht()
	{
		path = testPathFinder.GetPath(new Vector2(3,3), new Vector2(0,9));
		renderPath(path);
	}
	
	void generateLevel(int[,] map)
	{
		int i,j;
		for (i = 0; i<7 ;i++)
		{
			for(j = 0; j<7 ;j++)
			{
				if(map[i,j] == 1)
				{
					GameObject newFloorTile = Instantiate( wall, new Vector3(i,j, this.transform.position.z), Quaternion.identity) as GameObject;
					newFloorTile.transform.parent = this.transform;
				}
				else
				{
					GameObject newFloorTile = Instantiate( floor, new Vector3(i,j, this.transform.position.z), Quaternion.identity) as GameObject;
					newFloorTile.transform.parent = this.transform;
				}
			}
		}
	}
	
	void renderPath(List<Vector2> path)
	{
		
		myLineRenderer.SetVertexCount(path.Count);
		int i = 0;
		foreach(Vector2 point in path)
		{
			Debug.Log ("Path item: " +i + " - " + point);
			myLineRenderer.SetPosition(i, new Vector3(point.x, point.y, 0f));
			i++;
		}
		Debug.Log("Line Renderer Set");
		
	}
	
	// Update is called once per frame
//	void Update () {}
}

