using System.Collections.Generic;
using UnityEngine;

namespace ZiplineValley.Models.Path
{
    public class PathModel 
    {
        public Vector2 PathStartPosition { get; set; }
        public List<Vector2> CollisionPoints { get; set; } = new List<Vector2>();
        public Vector2 PathEndPosition { get; set; }
    }
}
