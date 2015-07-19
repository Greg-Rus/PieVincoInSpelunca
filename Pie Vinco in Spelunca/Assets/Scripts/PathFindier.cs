using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PathFindier : MonoBehaviour {

	int[,] dungeonMap;
	Vector2[] heightEdges = {new Vector2(0,1), new Vector2(1,0), new Vector2(0,-1), new Vector2(-1,0)};
	Vector2[] diagonalEdges = {new Vector2(1,1), new Vector2(1,-1), new Vector2(-1,-1), new Vector2(-1,1)};
	Dictionary<Vector2,Step> closedList;
	Dictionary<Vector2,Step> openList;
	float fitness;
	Vector2 destination;

	public PathFindier(int[,] map)
	{
		dungeonMap = map;
		closedList = new Dictionary<Vector2, Step>();
		openList = new Dictionary<Vector2, Step>();
	}

	public List<Vector2> GetPath(Vector2 from, Vector2 to)
	{
		openList.Add(from, new Step(from,from,0f,0f));
		destination = to;
		return null;
	}
	
	private void EvaluateNeighbours(Step currentTile)
	{
		EvaluateNeighboursByEdge(currentTile, heightEdges, 1f);
		EvaluateNeighboursByEdge(currentTile, diagonalEdges, 1.414f);
	}
	
	private void EvaluateNeighboursByEdge(Step currentTile, Vector2[] edges, float edgeCost)
	{
		foreach(Vector2 edge in edges)
		{
			Vector2 neighbour = currentTile.location + edge;
			if (!isValidNeighbour(neighbour)) continue;
			
			float newTotalCost = currentTile.totalCost + edgeCost;
			float heuristic = Mathf.Abs ((currentTile.location + destination).magnitude); //can this be negative? Must test.
			fitness = newTotalCost + heuristic;
			
			if (!openList.ContainsKey(neighbour))
			{
				openList.Add(neighbour, new Step(neighbour,currentTile.location,currentTile.totalCost + edgeCost ,fitness));
			}
			else if (openList[neighbour].totalCost > newTotalCost)
			{
				openList[neighbour].fitness = fitness;
				openList[neighbour].parent = currentTile.location;
				openList[neighbour].totalCost = newTotalCost;
			}
		}
	}
	
	private bool isValidNeighbour(Vector2 neighbour)
	{
		if (dungeonMap[(int)neighbour.x, (int)neighbour.y] == 1 || closedList.ContainsKey(neighbour)) return false;
		else return true;
	}
	
	private float EvaluateFitness(Step currentTile, float edgeCost)
	{
		return 0f;
	}
	

}

public class Step
{
	public Vector2 location;
	public Vector2 parent;
	public float totalCost;
	public float fitness;
	public int x;
	public int y;
	
	public Step(Vector2 stepLocation, Vector2 stepParent, float stepTotalCost, float stepFitness)
	{
		location = stepLocation;
		parent = stepParent ;
		totalCost = stepTotalCost;
		fitness = stepFitness;
		x = (int)stepLocation.x;
		y = (int)stepLocation.y;
	}
}
