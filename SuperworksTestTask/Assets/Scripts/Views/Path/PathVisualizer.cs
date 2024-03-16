using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZiplineValley.Models.Path;

namespace ZiplineValley.Views.Path
{
    public class PathVisualizer : MonoBehaviour
    {
        private const float minDistanceBetweenPoints = 0.01f;

        [SerializeField]
        private LineRenderer _lineRenderer;
        [SerializeField]
        private Transform _lineEnding;

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
                    var positions = new List<Vector3>();
                    positions.Add(path.PathStartPosition);
                    positions.AddRange(path.CollisionPoints
                        .Select(p => new Vector3(p.x, p.y, 0f)));
                    positions.Add(path.PathEndPosition);

                    _lineRenderer.positionCount = positions.Count;
                    _lineRenderer.SetPositions(positions.ToArray());

                    _lineEnding.gameObject.SetActive(true);
                    _lineEnding.transform.position = path.PathEndPosition;
                }
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }

        private void ClearLine()
        {
            _lineRenderer.positionCount = 0;
            _lineRenderer.SetPositions(new Vector3[] { });
            _lineEnding.gameObject.SetActive(false);
        }
    }
}
