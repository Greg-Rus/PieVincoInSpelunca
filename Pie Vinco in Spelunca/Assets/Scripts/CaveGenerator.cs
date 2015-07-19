using UnityEngine;
using System.Collections;


public class CaveGenerator : MonoBehaviour {

	public int MapHeight;
	public int MapWidth;
	public int spaceReducingCycles;
	public int smoothingCycles;
	public int PercentAreWalls;
	public int PercentIsDenserCenter;
	public int PercentAreDenserWalls;
	public GameObject wall;
	public GameObject floor;
	
	private int[,] Map;
	private GameObject [,] caveTiles;
	// Use this for initialization
	void Start () {
		//Map = new int[MapWidth,MapHeight];
		RandomFillMap();
		RefineCavern();
		BuildCaverns();
	}

	public void RefineCavern()
	{


		//DesignCaverns(false);
		for (int i = 0; i<spaceReducingCycles ; i++)
		{
			DesignCaverns(Map, true);
		}
		
		for (int i = 0; i<smoothingCycles ; i++)
		{
			DesignCaverns(Map, false);
		}
		Debug.Log (countWalls());
	}
	public int countWalls()
	{
		int wallCount = 0;
		foreach (int tile in Map)
		{
			if (tile == 1) wallCount++;
		}
		return  wallCount * 100 / (MapHeight*MapWidth);
	}
	
	public void BuildCaverns()
	{
		caveTiles = new GameObject[MapWidth,MapHeight];
		
		for(int column=0, row=0; row <= MapHeight-1; row++)
		{
			for(column = 0; column <= MapWidth-1; column++)
			{
				if(Map[column,row] == 1)
				{
					caveTiles[column,row] = Instantiate(wall, new Vector3(column*0.5f,row*0.5f,this.transform.position.z),Quaternion.identity) as GameObject;
					caveTiles[column,row].transform.parent = this.transform;
				}
				else if(Map[column,row] == 0)
				{
					caveTiles[column,row] = Instantiate(floor, new Vector3(column*0.5f,row*0.5f,this.transform.position.z),Quaternion.identity) as GameObject;
					caveTiles[column,row].transform.parent = this.transform;
				}	
			}
		}
	}
	
	public void DesignCaverns(int[,] Map, bool reduceOpenSpace = false)
	{
		// By initilizing column in the outter loop, its only created ONCE
		for(int column=0, row=0; row <= MapHeight-1; row++)
		{
			for(column = 0; column <= MapWidth-1; column++)
			{
				Map[column,row] = PlaceWallLogic(column,row, reduceOpenSpace);
			}
		}
	}
	
	public int PlaceWallLogic(int x,int y, bool reduceOpenSpace)
	{
		int numWalls = GetAdjacentWalls(x,y,1,1);
		
		
		if(Map[x,y]==1)
		{
			if( numWalls >= 4 ) //numWalls >= 4 
			{
				return 1;
			}
			if(numWalls<2) //numWalls<2
			{
				return 0;
			}
			
		}
		else
		{
			if(reduceOpenSpace)
			{
				if(numWalls>=5 || numWalls <=1) //numWalls>=5 || numWalls <=1
				{
					return 1;
				}
			}
			else if(numWalls>=5 ) //numWalls>=5
			{
				return 1;
			}
		}
		return 0;
	}
	
	
	public int GetAdjacentWalls(int x,int y,int scopeX,int scopeY)
	{
		int startX = x - scopeX;
		int startY = y - scopeY;
		int endX = x + scopeX;
		int endY = y + scopeY;
		
		int iX = startX;
		int iY = startY;
		
		int wallCounter = 0;
		
		for(iY = startY; iY <= endY; iY++) {
			for(iX = startX; iX <= endX; iX++)
			{
				if(!(iX==x && iY==y))
				{
					if(IsWall(iX,iY))
					{
						wallCounter += 1;
					}
				}
			}
		}
		return wallCounter;
	}
	
	bool IsWall(int x,int y)
	{
		// Consider out-of-bound a wall
		if( IsOutOfBounds(x,y) )
		{
			return true;
		}
		
		if( Map[x,y]==1	 )
		{
			return true;
		}
		
		if( Map[x,y]==0	 )
		{
			return false;
		}
		return false;
	}
	
	bool IsOutOfBounds(int x, int y)
	{
		if( x<0 || y<0 )
		{
			return true;
		}
		else if( x>MapWidth-1 || y>MapHeight-1 )
		{
			return true;
		}
		return false;
	}
	
	
	
	public void BlankMap()
	{
		for(int column=0,row=0; row < MapHeight; row++) {
			for(column = 0; column < MapWidth; column++) {
				Map[column,row] = 0;
			}
		}
	}
	
	public void RandomFillMap()
	{
		// New, empty map
		Map = new int[MapWidth,MapHeight];
		
		int middleOfMapHeight = (MapHeight / 2); // Temp variable
		int middleOfMapWidth = (MapWidth / 2);
		int mapCenterWidth = ((MapWidth *PercentIsDenserCenter) /100)/2; //Marks the area of the maps center. To be used for denser wall seeding.
		int mapCenterHeight = ((MapHeight *PercentIsDenserCenter) /100)/2;
		for(int column=0,row=0; row < MapHeight; row++) {
			for(column = 0; column < MapWidth; column++)
			{
				// If coordinants lie on the the edge of the map (creates a border)
				if(column == 0)
				{
					Map[column,row] = 1;
				}
				else if (row == 0)
				{
					Map[column,row] = 1;
				}
				else if (column == MapWidth-1)
				{
					Map[column,row] = 1;
				}
				else if (row == MapHeight-1)
				{
					Map[column,row] = 1;
				}
				// Else, fill with a wall a random percent of the time
				else
				{
					
					
					
					if(row == middleOfMapHeight ||row == middleOfMapHeight+1 )
					{
						Map[column,row] = 0;
					}
					else if(column>=middleOfMapWidth - mapCenterWidth && column <= middleOfMapWidth + mapCenterWidth)
					{
						Map[column,row] = RandomPercent(PercentAreDenserWalls);
					}
					else
					{
						Map[column,row] = RandomPercent(PercentAreWalls);
					}
				}
			}
		}
	}
	
	int RandomPercent(int percent)
	{
		if(percent>= Random.Range(1,101)) // rand.Next(1,101)
		{
			return 1;
		}
		return 0;
	}
}
