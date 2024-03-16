using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using ZiplineValley.Models.Obstacles;
using ZiplineValley.Models.Path;
using ZiplineValley.Views.Path;

namespace ZiplineValley.Controllers.PathBuilder
{
    public class PathBuilderController : BaseController
    {
        [SerializeField]
        private Transform _firstPoint;
        [SerializeField]
        private Transform _targetPoint;
        [SerializeField]
        private PathVisualizer _visualizer;

        private PathModel currentPath = new PathModel();

        private Vector3? lastFirstPointPosition, lastTargetPointPosition;

        private void Update()
        {
            if (WerePointsChanged())
            {
                currentPath.Points.Clear();
                currentPath.Points.Add(_firstPoint.position);

                var collisionHitPosition = Vector2.zero;
                ObstacleModel obstacleModel = null;

                var direction = (Vector2)(_targetPoint.position - _firstPoint.position).normalized;
                var raycast2DHit = Physics2D.Raycast(_firstPoint.position, direction);

                if (raycast2DHit.collider != null)
                {
                    obstacleModel = raycast2DHit.collider.GetComponent<ObstacleModel>();
                    collisionHitPosition = raycast2DHit.point;
                }

                if (obstacleModel != null)
                {
                    currentPath.Points.AddRange(GetBypassingPath(obstacleModel, _firstPoint.position, _targetPoint.position, collisionHitPosition));
                }
                currentPath.Points.Add(_targetPoint.position);

                _visualizer.Draw(currentPath);
            }
        }

        private List<Vector2> GetBypassingPath(
            ObstacleModel obstacle, 
            Vector2 startPosition,
            Vector2 targetPosition,
            Vector2 hitPosition)
        {
            var points = obstacle.Points.ToList();
            var results = new List<List<PointReferenceModel>>();
            int? firstCount = null;
            var direction = new Vector2();
            RaycastHit2D raycast2DHit;

            for (int i = 0; i < 2; i++)
            {
                var pointTo = GetNearestPoint(points, hitPosition);
                if (pointTo != null)
                {
                    var fromStartPoint = new List<PointReferenceModel>();
                    while (true)
                    {
                        points.Remove(pointTo);
                        fromStartPoint.Add(pointTo);

                        if (points.Count == 0) { break; }

                        direction = (targetPosition - pointTo.Position).normalized;
                        raycast2DHit = Physics2D.Raycast(pointTo.Position, direction);
                        if (raycast2DHit.collider == null
                            || raycast2DHit.collider.GetComponent<ObstacleModel>() != obstacle)
                        {
                            break;
                        }

                        pointTo = GetNearestPoint(points, pointTo.Position);
                        if (pointTo == null)
                        {
                            break;
                        }
                    }
                    fromStartPoint.Reverse();

                    var toStartPoint = new List<PointReferenceModel>();
                    foreach (var pointFrom in fromStartPoint)
                    {
                        toStartPoint.Add(pointFrom);

                        if (firstCount != null && toStartPoint.Count > firstCount.Value)
                        {
                            break;
                        }

                        direction = (startPosition - pointFrom.Position).normalized;
                        raycast2DHit = Physics2D.Raycast(pointFrom.Position, direction);
                        if (raycast2DHit.collider == null
                            || raycast2DHit.collider.GetComponent<ObstacleModel>() != obstacle)
                        {
                            break;
                        }
                    }
                    toStartPoint.Reverse();

                    if (firstCount == null)
                    {
                        firstCount = toStartPoint.Count;
                    }

                    results.Add(toStartPoint);
                }
            }

            return results
                .OrderBy(r => r.Count)
                .FirstOrDefault()
                .Select(p => p.Position)
                .ToList();
        }

        private PointReferenceModel GetNearestPoint(IReadOnlyList<PointReferenceModel> points, Vector2 position)
        {
            if (points == null || points.Count == 0)
            {
                Debug.LogError($"{name} obstacle is not initialized.");
                return null;
            }
            else
            {
                return points
                    .OrderBy(p => Vector2.Distance(position, p.Position))
                    .FirstOrDefault();
            }
        }

        private Vector2 RotateVector2(Vector2 vector, float degrees)
        {
            var rotation = Quaternion.AngleAxis(degrees, Vector3.forward);
            return rotation * vector;
        }



        private bool WerePointsChanged()
        {
            var result = false;

            //todo: remove checking of changing of the first point
            if (lastFirstPointPosition == null 
                || !Mathf.Approximately(Vector3.Distance(lastFirstPointPosition.Value, _firstPoint.position), 0f))
            {
                lastFirstPointPosition = _firstPoint.position;
                result = true;
            }

            if (lastTargetPointPosition == null
                || !Mathf.Approximately(Vector3.Distance(lastTargetPointPosition.Value, _targetPoint.position), 0f))
            {
                lastTargetPointPosition = _targetPoint.position;
                result = true;
            }

            return result;
        }
    }
}
