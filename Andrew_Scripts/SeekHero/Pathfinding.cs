using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Priority_Queue;
using _Scripts.SeekHero;

    public class Pathfinding : MonoBehaviour {
    
        private SeekHero _seekHero;
        private Grid _grid;
    
        private void Awake()
        {
            _grid = transform.parent.GetComponent<Grid>();
            _seekHero = GetComponent<SeekHero>();
        }
    
        public void StartGettingPath(Vector3 startPos, Vector3 targetPos)
        {
            StartCoroutine(FindPath(startPos, targetPos));
        }
    
        private IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {
            var startNode = new Node(_grid.WorldToCell(startPos));
            var targetNode = new Node(_grid.WorldToCell(targetPos));
            
            var visited = new List<Vector3Int>();
            var path = new List<Node>();
    
            var initialInfo = new TraversalInfo();
    
            initialInfo.SetCurrentNode(startNode);
            initialInfo.SetPath(path);
    
            var priorityQueue = new SimplePriorityQueue<TraversalInfo>();
    
            priorityQueue.Enqueue(initialInfo, 0);
            while (priorityQueue.Count > 0) {
                var currentInfo = priorityQueue.Dequeue();
                var currentNode = currentInfo.GetCurrentNode();
                path = currentInfo.GetPath();
                
                if (visited.Contains(currentNode.Location)) continue; 
                
                visited.Add(currentNode.Location);
                if (currentNode.Location == targetNode.Location)
                {
                    targetNode.Parent = currentNode.Parent;
                    break;
                }
                foreach (var successor in GetSuccessors(currentNode))
                {
                    var tempPath = path;
                    if (visited.Contains(successor.Location)) continue; 
                   
                    var ti = new TraversalInfo();
                    tempPath.Add(successor);
                    ti.SetCurrentNode(successor);
                    ti.SetPath(tempPath);
                    successor.Parent = currentNode;
                    var totalCost = tempPath.Count + (int)GetEuclideanDistance(successor, targetNode);
                    priorityQueue.Enqueue(ti, totalCost);
                }
            }
            
            yield return null;
            
            PrunePathAndReturn(startNode, targetNode);    
        }

        private void PrunePathAndReturn(Node startNode, Node targetNode)
        {
            var newpath = new List<Vector3Int>();
    
            var startNodeFound = false;
            var node = targetNode;
    
            while (!startNodeFound)
            {
                if (node.Location == startNode.Location)
                {
                    startNodeFound = true;
                }
                newpath.Add(node.Location);
                node = node.Parent;
            }
            
            _seekHero.FoundPath(newpath);
        }
        
        private IEnumerable<Node> GetSuccessors(Node node) {
            var successors = new List<Node>();
    
            for (var x = -1; x <= 1; x++) {
                for (var y = -1; y <= 1; y++) {
                    if (x == 0 && y == 0)
                        continue;
    
                    var xPos = node.Location.x + x;
                    var yPos = node.Location.y + y;
                    var locationToCheck = new Vector3Int(xPos, yPos, node.Location.z);
                    var worldPoint = _grid.GetCellCenterWorld(locationToCheck);
                    
                    var isValidMovement = !(Physics.CheckSphere(worldPoint, transform.lossyScale.x, _seekHero.UnwalkableMask));
    
                    if (isValidMovement)
                    {
                        successors.Add(new Node(locationToCheck));
                    }
                    
                }
            }
    
            return successors;
        }
    
        // Adapted from http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html
        private double GetEuclideanDistance(Node node, Node goal)
        {
            var dx = Mathf.Abs(node.Location.x - goal.Location.x);
            var dy = Mathf.Abs(node.Location.z - goal.Location.z);
            return 8 * (dx * dx + dy * dy);
        }
    
        private class TraversalInfo
        {
            private Node _currentNode;
            private List<Node> _path = new List<Node>();
            
            public Node GetCurrentNode()
            {
                return _currentNode;
            }
    
            public List<Node> GetPath()
            {
                return _path;
            }
    
            public void SetCurrentNode(Node node)
            {
                _currentNode = node;
            }
    
            public void SetPath(List<Node> path)
            {
                _path = path;
            }
        }
    
        public class Node
        {
            public Vector3Int Location;
            public Node Parent;
            
            public Node(Vector3Int location)
            {
                Location = location;
            }
        }

}

