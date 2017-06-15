using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AStarPathfind {
	
	protected Vector3 Target;
	protected ArrayList ClosedNodes;
	protected ArrayList OpenNodes;
	protected float LowestScore = 99999;
	protected Vector3 StepSize;

	public void AddClosedNodeSet(Vector3[] closedPositions){
		foreach(Vector3 closed in closedPositions){
			Node n = new Node();
			n.position = closed;
			this.ClosedNodes.Add(n);
		}
	}

	public AStarPathfind(Vector3 Target,Vector3 StepSize){
		this.Target = Target;
		this.StepSize = StepSize;
		this.ClosedNodes = new ArrayList();
		this.OpenNodes = new ArrayList();
	}

	protected ArrayList GetNeighborOpenNodes(Node node){
		ArrayList nodeList = new ArrayList();
		for(int x = -1;x < 2;x++){
			for(int y = -1;y < 2;y++){
				//no diagonals or sametile
				if(x == -1 && y == -1){ 
					continue;
				}
				if(x == 1 && y == -1){
					continue;
				}
				if(x == -1 && y ==1){
					continue;
				}
				if(x == 1 && y == 1){
					continue;
				}
				if(x == 0 && y == 0){
					continue;
				}
				Vector3 nNode = new Vector3((node.position.x+(StepSize.x*(x))), node.position.y, (node.position.z+(StepSize.z*(y))));
				Node n = new Node();
				n.position = nNode;
				n.parent = node;
				n.value = CalculateH(n);
				n.pathscore = (node.pathscore) + n.value;
				if(IsClosed(n)){
					continue; //ignore
				}
				if(IsOpen(n)){
					Node oNode = GetOpen(n); //recalculate
					if(n.pathscore < oNode.pathscore){
						oNode.parent = node;
						oNode.value = n.value;
					}
					continue;
				}
				nodeList.Add(n);
			}
		}
		return nodeList;
	}

	protected Node GetOpen(Node n){
		foreach(Node CNode in OpenNodes){
			if(CNode.position == n.position){
				return CNode;
			}
		}
		throw new Exception("No Such Node Exists (A*)");
	}

	protected bool IsOpen(Node n){
		foreach(Node CNode in OpenNodes){
			if(CNode.position == n.position){
				return true;
			}
		}
		return false;
	}

	protected bool IsClosed(Node Node){
		foreach(Node CNode in ClosedNodes){
			if(CNode.position == Node.position){
				return true;
			}
		}
		return false;
	}

	public class Node : IComparable {
		public Node(){}
		public float pathscore;
		public float value;
		public Vector3 position;
		public Node parent;

		public virtual int CompareTo(object obj) {
			Node n = obj as Node;
			return (pathscore.CompareTo(n.pathscore));
		}
	}
	
	public Vector3 GetTopNode(){
		return ((Node) this.OpenNodes[0]).position;
	}

	int MaxSearchDepth = 100;

	public virtual Node FindPath(Node Position){
		ArrayList Adjacent = this.GetNeighborOpenNodes(Position);
		bool pathFound = false;
		int searchDepth = 0;
		while(!pathFound && searchDepth < MaxSearchDepth){
			searchDepth++;
			this.ClosedNodes.Add(Position);
			Adjacent = this.GetNeighborOpenNodes(Position);
			Adjacent.Sort();
			Position = Adjacent[0] as Node;
			this.OpenNodes.AddRange(Adjacent);
			if(Position.value <= 1f){
				pathFound = true;
				break;
			}
		}
		return Position;
	}

    //Heuristic
    virtual protected float CalculateH(Node n){
		return CalculateDistance(n.position);
	}

	protected float CalculateDistance(Vector3 Position){
		return (this.Target - Position).sqrMagnitude;
	}
}
