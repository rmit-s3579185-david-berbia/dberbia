using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.SeekHero
{
    public class SeekHero : MonoBehaviour
    {

        private float _speed;
        private Transform _seeker;
        private Transform _target;
		private Transform desired_position;

        public bool IsWalking;
		public bool IsFleeing;

        private readonly Stack<Vector3> _wayPoints = new Stack<Vector3>();

        private Vector3 _targetWaypoint;
        private Pathfinding _pathfinding;
        private Grid _grid;


        private void Awake()
        {
            _pathfinding = gameObject.AddComponent<Pathfinding>();
			_target = new GameObject ().transform;
			desired_position = new GameObject ().transform;
//            _grid = transform.parent.GetComponent<Grid>();
        }

        public void Seek(Transform seeker, Transform target, float speed, Grid grid)
        {
            _target = target;
            _seeker = seeker;
            _grid = grid;
            _speed = speed;
            _pathfinding.StartGettingPath(seeker.transform.position, target.transform.position, _grid);
        }

		public void Flee(Transform seeker, Transform target, float speed, Grid grid)
		{
			_target = target;
			_seeker = seeker;
			desired_position.position = Vector3.Normalize(seeker.position - target.position);
			_grid = grid;
			_speed = speed;
			_pathfinding.StartGettingPath(seeker.transform.position, desired_position.position, _grid);
		}

		public void Patrol(Transform seeker, float speed, Grid grid)
		{
			//Choosing a random point nearby and getting a path to there
			_target.position = new Vector3(seeker.transform.position.x + Random.Range(-10,10), seeker.transform.position.y,
				seeker.transform.position.z + Random.Range(-10,10));
			_seeker = seeker;
			_grid = grid;
			_speed = speed;
			_pathfinding.StartGettingPath(seeker.transform.position, _target.transform.position, _grid);
		}

        public void Stop()
        {
            _wayPoints.Clear();
            IsWalking = false;
        }

        public void FoundPath(IEnumerable<Vector3Int> path)
        {
            _grid = transform.parent.GetComponent<Grid>();
            foreach (var waypoint in path)
            {
                _wayPoints.Push(_grid.CellToWorld(waypoint));
            }

            // Always need to pop the first off the stack as it includes where the Seeker was (so it travels backwards)
            _wayPoints.Pop();

        }

        private void Update()
        {
            if (IsWalking) Walk();
        }

        private void Walk()
        {
            if (_wayPoints.Count == 0)
            {
                Stop();
                return;
            }

            if (_targetWaypoint == Vector3.zero)
            {
                _targetWaypoint = _wayPoints.Pop();
            }

            // move towards the target
            _seeker.transform.position = Vector3.MoveTowards(_seeker.transform.position, _targetWaypoint, _speed * Time.deltaTime);
			//Smooth turning so predator faces forward
			Vector3 lookDirection = _targetWaypoint - _seeker.transform.position;
			_seeker.transform.rotation = Quaternion.RotateTowards(_seeker.transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 150);

            if (_seeker.transform.position == _targetWaypoint)
            {
                _targetWaypoint = _wayPoints.Pop();
            }
        }
    }
}
