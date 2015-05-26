using UnityEngine;
using System.Collections;

public static class Utility {

	// Utility functions to be used anywhere staticly. Should investigate making a library for this maybe.
	public static float LookAtAngle2D (Vector2 lookerPosition,Vector2 targetPosition)
	{
		Vector2 localTargetPosition = targetPosition - lookerPosition;
		float angle = Vector2.Angle( Vector2.up, localTargetPosition);
		if(localTargetPosition.x <0) angle =360f - angle; 
		
		return angle *-1f; //should investigate why -1 is necessary. Using Scale.y -1 does not help.
	}
}
