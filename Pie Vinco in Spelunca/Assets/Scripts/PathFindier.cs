using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class PathFindier {

	int[,] dungeonMap;
	Vector2[] heightEdges = {new Vector2(0,1), new Vector2(1,0), new Vector2(0,-1), new Vector2(-1,0)};
	Vector2[] diagonalEdges = {new Vector2(1,1), new Vector2(1,-1), new Vector2(-1,-1), new Vector2(-1,1)};
	Dictionary<Vector2,Step> closedList;
	Dictionary<Vector2,Step> openList;
	float fitness;
	Vector2 destination;
	Vector2 origin;

	public PathFindier(int[,] map)
	{
		dungeonMap = map;
		closedList = new Dictionary<Vector2, Step>();
		openList = new Dictionary<Vector2, Step>();
		Debug.Log ("Construction of PathFinder Complete");
		Debug.Log ("upper bound for y " + dungeonMap.GetUpperBound(1));
	
	}

	public List<Vector2> GetPath(Vector2 from, Vector2 to)
	{
		openList.Add(from, new Step(from, from ,0f,0f));
		destination = to;
		origin = from;
		Step nextStepCandidate = openList[from];
		//for (int i = 0 ; i<30 ;i++) //for loop works good for testing
		while(openList.Count != 0)
		{
			nextStepCandidate = getBestStepCandidate();
			Debug.Log("Next Candidate at " + nextStepCandidate.location + "with Fitness " + nextStepCandidate.fitness);
			
			closedList.Add(nextStepCandidate.location, nextStepCandidate);
			openList.Remove(nextStepCandidate.location);
			
			if(nextStepCandidate.location == destination) break;
			
			EvaluateNeighbours(nextStepCandidate);
		}
		return convertStepsToPath(closedList, nextStepCandidate.location);
	}
	
	private Step getBestStepCandidate()
	{
		float bestFitness = 9999f;
		Step bestCandidate = null;
		foreach (Step step in openList.Values)
		{
			if (step.fitness < bestFitness)
			{
				bestFitness = step.fitness;
				bestCandidate = step;
			}
		}
		return bestCandidate;
	}
	
	private List<Vector2> convertStepsToPath (Dictionary<Vector2,Step> steps, Vector2 end)
	{
		//Vector2[] path = new Vector2[steps.Count];
		int i = steps.Count;
		List<Vector2> path = new List<Vector2>();
		Step lastStep = steps[end];
		path.Add(lastStep.location);
		
		do
		{
			
			lastStep = steps[lastStep.parent];
			path.Add(lastStep.location);
			Debug.Log("Path Setp: " + lastStep.location + "my parent is: " + lastStep.parent);
			
		} while (lastStep.location != lastStep.parent);
		
		
		Debug.Log ("Printing Points in fresh path");
		foreach (Vector2 point in path)
		{
			Debug.Log (point);
		}
		path.Reverse();
		return path;
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
			float heuristic = calculateHeuristic(currentTile.location);
			fitness = newTotalCost + heuristic;
			
			if (!openList.ContainsKey(neighbour))
			{
				openList.Add(neighbour, new Step(neighbour, currentTile.location, newTotalCost ,fitness));
			}
			else if (openList[neighbour].totalCost > newTotalCost)
			{
				openList[neighbour].fitness = fitness;
				openList[neighbour].parent = currentTile.location;
				openList[neighbour].totalCost = newTotalCost;
			}
		}
	}
	
	private float calculateHeuristic(Vector2 location)
	{
		Vector2 heuristicVector = location - destination;
		return (float)(Mathf.Abs(heuristicVector.x) + Mathf.Abs(heuristicVector.y)); //can this be negative? Must test. 
	}
	
	private bool isValidNeighbour(Vector2 neighbour)
	{
		if (neighbour.x < 0 || neighbour.y <0) return false;
		if (neighbour.x > dungeonMap.GetUpperBound(0) || neighbour.y > dungeonMap.GetUpperBound(1)) return false;
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
