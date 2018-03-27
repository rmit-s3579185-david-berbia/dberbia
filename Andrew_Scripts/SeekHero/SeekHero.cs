using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.SeekHero
{
    public class SeekHero : MonoBehaviour {

        public Transform Target;
        public LayerMask UnwalkableMask;
        public float Speed;

        private readonly Stack<Vector3> _wayPoints = new Stack<Vector3>();

        private Vector3 _targetWaypoint;
        private Pathfinding _pathfinding;
        private Grid _grid; 

        private void Awake()
        {
            _pathfinding = gameObject.AddComponent<Pathfinding>();
            _grid = transform.parent.GetComponent<Grid>();
        }

        private void Start()
        {
            _pathfinding.StartGettingPath(transform.position, Target.transform.position);
        }

        public void FoundPath(IEnumerable<Vector3Int> path)
        {
            foreach (var waypoint in path)
            {
                _wayPoints.Push(_grid.CellToWorld(waypoint));
            }
        }

        // Update is called once per frame    
        private void Update () {
            Walk();
        }

        private void Walk()
        {
            if (_wayPoints.Count == 0) return;

            if (_targetWaypoint == Vector3.zero)
            {
                _targetWaypoint = _wayPoints.Pop();
            }

            // move towards the target
            transform.position = Vector3.MoveTowards(transform.position, _targetWaypoint, Speed * Time.deltaTime);

            if (transform.position == _targetWaypoint)
            {
                _targetWaypoint = _wayPoints.Pop();
            }
        }
    }
}
