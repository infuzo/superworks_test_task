using System;
using UnityEngine;

namespace ZiplineValley.Models.Home
{
    public class HomeModel : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D _collider;

        [Space, SerializeField]
        private Transform _startCharacterPoint;
        [SerializeField]
        private Transform _endCharacterPoint;

        public bool IsPointerInside(Vector2 position)
        {
            try
            {
                return _collider.bounds.Contains(position);
            }
            catch (Exception ex) { Debug.LogException(ex); }

            return false;
        }

        public Vector2 StartCharacterPosition => _startCharacterPoint.position;
        public Vector2 EndCharacterPosition => _endCharacterPoint.position;
    }
}
