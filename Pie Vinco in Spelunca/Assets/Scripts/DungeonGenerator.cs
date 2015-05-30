using UnityEngine;
using System.Collections;

public class DungeonGenerator : MonoBehaviour {

	private int[,] dungeonMap;
	private GameObject[,] dungeonTiles;
	public GameObject wall; //This should be in a singleton
	public GameObject floor;
	public int mapHeight;
	public int mapWidth;
	public int numberOfRooms;
	public int minRoomSize;
	public int maxRoomSize;
	public int roomPlacementAttempts;
	public int seed = 42;
	
	
	// Use this for initialization
	void Start () {
		Random.seed = seed;
		dungeonMap = new int[mapWidth,mapHeight];
		InitializeDungeon();
		
		PlaceRandomRooms();
		
		BuildDungeon();
	}
	
	private void PlaceRandomRooms()
	{
		for (int i = 0 ; i < numberOfRooms ; i++)
		{
			PlaceRoom();
		}
	}
	
	public void InitializeDungeon()
	{
		for(int column=0,row=0; row < mapHeight; row++) {
			for(column = 0; column < mapWidth; column++) {
				dungeonMap[column,row] = 1;
			}
		}
	}
	
	private void DesignRandomRoom(out int roomWidth, out int roomHeight, out int[,] roomMap)
	{
		roomWidth = Random.Range(minRoomSize,maxRoomSize);
		roomHeight = Random.Range(minRoomSize,maxRoomSize);
		roomMap = new int[roomWidth,roomHeight];
		
		for(int column=0,row=0; row < roomHeight; row++) {
			for(column = 0; column < roomWidth; column++) {
				roomMap[column,row] = 0;
			}
		}
	}
	
	private void PlaceRoom()
	{
		int roomWidth = 0;
		int roomHeight = 0;
		int[,] roomMap;
		DesignRandomRoom(out roomWidth, out roomHeight, out roomMap);
		Vector2 roomOrigin = Vector2.zero;
		
		for (int i = 0 ; i <roomPlacementAttempts ; i++)
		{
			Debug.Log("Room placement attempt: " + i);
			roomOrigin = PickRandomRoomLocation(roomWidth, roomHeight);
			if(RoomsWillOverlap(roomMap,roomOrigin,roomHeight,roomWidth)) continue;
			else break;
		}
		
		for(int column = 0, row = 0; row < roomHeight; row++) {
			for(column = 0; column < roomWidth; column++) {
				dungeonMap[column + (int)roomOrigin.x,row + (int)roomOrigin.y] = roomMap[column,row];
			}
		}
	
	}
				   
	private bool RoomsWillOverlap(int[,] roomMap, Vector2 roomOrigin, int roomHeight, int roomWidth)
	{
		for(int column = 0, row = 0; row < roomHeight; row++) {
			for(column = 0; column < roomWidth; column++) {
				if(dungeonMap[column + (int)roomOrigin.x,row + (int)roomOrigin.y] == 0 && roomMap[column,row] == 0)
				{
					Debug.Log ("Overlap detected");
					Debug.Log (dungeonMap[column + (int)roomOrigin.x,row + (int)roomOrigin.y] + " || " + roomMap[column,row]);
					return true;
				}
			 
			}
		}
		Debug.Log ("No Overlap");
		return false;
	}
	
	private Vector2 PickRandomRoomLocation(int widthOffset, int heightOffset)
	{
		int mapX = Random.Range(0, mapWidth - widthOffset + 1);
		int mapY = Random.Range(0, mapHeight - heightOffset + 1);
		return new Vector2(mapX,mapY);
	}

	public void BuildDungeon() //This should be emancipated to a separate builder script
	{
		dungeonTiles = new GameObject[mapWidth,mapHeight];
		
		for(int column=0, row=0; row <= mapHeight-1; row++)
		{
			for(column = 0; column <= mapWidth-1; column++)
			{
				if(dungeonMap[column,row] == 1)
				{
					dungeonTiles[column,row] = Instantiate(wall, new Vector3(column*0.5f,row*0.5f,this.transform.position.z),Quaternion.identity) as GameObject;
					dungeonTiles[column,row].transform.parent = this.transform;
				}
				else if(dungeonMap[column,row] == 0)
				{
					dungeonTiles[column,row] = Instantiate(floor, new Vector3(column*0.5f,row*0.5f,this.transform.position.z),Quaternion.identity) as GameObject;
					dungeonTiles[column,row].transform.parent = this.transform;
				}	
			}
		}
	}
	
	
}
