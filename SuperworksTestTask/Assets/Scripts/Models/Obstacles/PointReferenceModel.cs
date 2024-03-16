using UnityEngine;

namespace ZiplineValley.Models.Obstacles
{
    public class PointReferenceModel
    {
        internal PointReferenceModel(Vector2 position)
        {
            this.Position = position;
        }

        public Vector2 Position { get; set; }
    }
}
