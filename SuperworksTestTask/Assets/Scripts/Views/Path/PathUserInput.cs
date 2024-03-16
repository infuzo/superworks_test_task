using System;
using UnityEngine;
using ZiplineValley.Models;

namespace ZiplineValley.Views.Path
{
    public class PathUserInput : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;

        private int layerMask;
        private bool wasDraggingPreviousFrame = false;

        public Vector2? TargetPosition { get; private set; } = null;

        private void Start()
        {
            layerMask = LayerMask.GetMask(GlobalConstants.PathEndingLayerName);
        }

        private void Update()
        {
            var isLMBPressing = Input.GetMouseButton(0);

            if (wasDraggingPreviousFrame && !isLMBPressing)
            {
                wasDraggingPreviousFrame = false;
                TargetPosition = null;
                return;
            }

            if (!isLMBPressing) { return; }

            if (!wasDraggingPreviousFrame)
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                var hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, layerMask);

                if (hit.collider == null || hit.collider.transform != transform)
                {
                    return;
                }
            }

            wasDraggingPreviousFrame = true;
            TargetPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public void UpdateStartDraggingPositon(Vector2 position)
        {
            try
            {
                transform.position = position;
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }
    }
}
