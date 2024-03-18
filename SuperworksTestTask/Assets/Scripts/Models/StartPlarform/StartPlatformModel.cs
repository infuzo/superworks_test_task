using UnityEngine;

namespace ZiplineValley.Models.StartPlatform
{
    public class StartPlatformModel : MonoBehaviour
    {
        [SerializeField]
        private Transform _pathInitialPoint;
        [SerializeField]
        private Transform _pathStartTargetPoint;

        public Vector2 PathInitialPosition => _pathInitialPoint.position;
        public Vector2 PathStartTargetPosition => _pathStartTargetPoint.position;
    }
}
