using UnityEngine;
using System.Collections;

public class Grid {
	public int mapHeight;
	public int mapWidth;
	private int[,] dungeonMap;
	
	public Grid(int height, int width)
	{
		mapHeight = height;
		mapWidth = width;
		dungeonMap = new int[mapWidth,mapHeight];
		InitializeDungeon();
	}
	
	public void InitializeDungeon()
	{
		for(int column=0,row=0; row < mapHeight; row++) {
			for(column = 0; column < mapWidth; column++) {
				dungeonMap[column,row] = 1;
			}
		}
	}

}
