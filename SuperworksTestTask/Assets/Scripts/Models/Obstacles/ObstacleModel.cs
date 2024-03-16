using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ZiplineValley.Models.Obstacles
{
    public class ObstacleModel : MonoBehaviour
    {
        protected List<PointReferenceModel> points;

        public virtual IReadOnlyList<PointReferenceModel> Points => points;
    }
}