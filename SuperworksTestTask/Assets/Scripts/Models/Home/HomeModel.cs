using System;
using UnityEngine;

namespace ZiplineValley.Models.Home
{
    public class HomeModel : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider2D _collider;

        public bool IsPointerInside(Vector2 position)
        {
            try
            {
                return _collider.bounds.Contains(position);
            }
            catch (Exception ex) { Debug.LogException(ex); }

            return false;
        }
    }
}
