using UnityEngine;
using ZiplineValley.Models.Home;
using ZiplineValley.Models.StartPlatform;

namespace ZiplineValley.Models.Level
{
    public class LevelModel : MonoBehaviour
    {
        [SerializeField]
        private HomeModel _homeModel;
        [SerializeField]
        private StartPlatformModel _startPlatformModel;
        [SerializeField]
        private int _initialCharacterCount;

        public HomeModel HomeModel => _homeModel;
        public StartPlatformModel StartPlatformModel => _startPlatformModel;
        public int InitialCharacterCount => _initialCharacterCount;

        public bool IsPathAttachedToHome { get; set; }
        public bool IsCharacterMovementStarted { get; set; }
    }
}
