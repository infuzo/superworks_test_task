using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZiplineValley.Models.Obstacles
{
    internal class CustomPointsObstacle : ObstacleModel
    {
        private const float minStepBetweenPoints = 0.05f;

        [SerializeField]
        private PolygonCollider2D _collider;

        protected virtual void Awake()
        {
            InitializePointListByColliderCorners();
        }

        protected virtual void InitializePointListByColliderCorners()
        {
            points = _collider.GetPath(0)
                .Select(p => {
                    var point = new PointReferenceModel(transform.TransformPoint(p));
                    var direction = (point.Position - (Vector2)transform.position).normalized * 0.01f;
                    point.Position += direction;

                    return point;
                })
                .ToList();

            var additionalPoints = new List<PointReferenceModel>();
            for (int i = 1; i <= points.Count; i++)
            {
                var lastPointPosition = points[i - 1].Position;
                Vector2 currentPointPosition;
                if (i == points.Count)
                {
                    currentPointPosition = points[0].Position;
                }
                else
                {
                    currentPointPosition = points[i].Position;
                }

                var distance = Vector2.Distance(lastPointPosition, currentPointPosition);
                if (distance <= minStepBetweenPoints) { continue; }

                var pointsCount = Mathf.FloorToInt(distance / minStepBetweenPoints);

                for (int additionalPointCounter = 1; additionalPointCounter < pointsCount; additionalPointCounter++)
                {
                    additionalPoints.Add(new PointReferenceModel(Vector2.Lerp(lastPointPosition, currentPointPosition,
                        (float)additionalPointCounter / (float)pointsCount)));
                }
            }

            points.AddRange(additionalPoints);
        }

        public override bool IsPointInsideObstacle(Vector2 position)
        {
            return _collider.OverlapPoint(position);
        }
    }
}
