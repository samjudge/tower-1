using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfindAroundWalls : AStarPathfind{
	
	public AStarPathfindAroundWalls(Vector3 Target,Vector3 StepSize) : base(Target, StepSize){}
    
	//Heuristic
	override protected float CalculateH(Node n){
        LayerMask Mask = LayerMask.GetMask("Walls");
        RaycastHit hit = new RaycastHit();
        Physics.Linecast(
            new Vector3(
                n.parent.position.x,
                n.parent.position.y,
                n.parent.position.z
            ), new Vector3(
                n.position.x,
                n.position.y,
                n.position.z
        ), out hit
        ,Mask);
        if(hit.transform != null) {
            return this.LowestScore;
        }
		return CalculateDistance(n.position);
	}
    
}
