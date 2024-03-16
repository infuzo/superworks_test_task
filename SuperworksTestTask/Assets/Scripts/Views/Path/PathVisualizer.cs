using System;
using System.Linq;
using UnityEngine;
using ZiplineValley.Models.Path;

namespace ZiplineValley.Views.Path
{
    public class PathVisualizer : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _lineRenderer;

        public void Draw(PathModel path)
        {
            try
            {
                if (path == null)
                {
                    ClearLine();
                }
                else
                {
                    _lineRenderer.positionCount = path.Points.Count;
                    _lineRenderer.SetPositions(path.Points
                        .Select(p => new Vector3(p.x, p.y, 0f))
                        .ToArray());
                }
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }

        private void ClearLine()
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.SetPositions(new Vector3[] { });
        }
    }
}
