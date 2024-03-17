using System.Collections.Generic;
using UnityEngine;

namespace ZiplineValley.Models.Obstacles
{
    public class ObstacleModel : MonoBehaviour
    {
        protected List<PointReferenceModel> points;

        public virtual IReadOnlyList<PointReferenceModel> Points => points;

        public virtual bool IsPointInsideObstacle(Vector2 position)
        {
            return false;
        }
    }
}