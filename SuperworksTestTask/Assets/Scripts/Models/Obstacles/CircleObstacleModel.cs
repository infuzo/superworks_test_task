using System.Collections.Generic;
using UnityEngine;

namespace ZiplineValley.Models.Obstacles
{
    internal class CircleObstacleModel : ObstacleModel
    {
        [SerializeField]
        private float _stepAngle = 10f;

        private void Awake()
        {
            CalculatePoints();
        }

        private void CalculatePoints()
        {
            points = new List<PointReferenceModel>();
            var radius = GetRadius();

            var angle = 0f;
            while (angle < 360f)
            {
                var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                var direction = rotation * Vector3.up * radius;
                points.Add(new PointReferenceModel(transform.position + direction));

                angle += _stepAngle;
            }
        }

        private float GetRadius()
        {
            return (transform.lossyScale.x / 2f) + 0.01f; 
        }

        public override bool IsPointInsideObstacle(Vector2 position)
        {
            return Vector2.Distance(transform.position, position) <= GetRadius();
        }
    }
}
