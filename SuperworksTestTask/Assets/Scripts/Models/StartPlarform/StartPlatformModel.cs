using UnityEngine;

namespace ZiplineValley.Models.StartPlatform
{
    public class StartPlatformModel : MonoBehaviour
    {
        [SerializeField]
        private Transform _pathInitialPoint;
        [SerializeField]
        private Transform _pathStartTargetPoint;

        [Space, SerializeField]
        private Transform _startCharacterPoint;
        [SerializeField]
        private Transform _endCharacterPoint;

        public Vector2 PathInitialPosition => _pathInitialPoint.position;
        public Vector2 PathStartTargetPosition => _pathStartTargetPoint.position;

        public Vector2 StartCharacterPosition => _startCharacterPoint.position;
        public Vector2 EndCharacterPosition => _endCharacterPoint.position;
    }
}
