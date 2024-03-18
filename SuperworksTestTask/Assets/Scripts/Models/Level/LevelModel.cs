using System.Collections.Generic;
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
        private int _initialCharacterCount = 5;
        [SerializeField]
        private float _charactersMovementSpeed = 3f;

        #region Predefined values
        public HomeModel HomeModel => _homeModel;
        public StartPlatformModel StartPlatformModel => _startPlatformModel;
        public int InitialCharacterCount => _initialCharacterCount;
        public float CharacterMovementSpeed => _charactersMovementSpeed;
        #endregion

        #region Values changing during the gameplay
        public bool IsPathAttachedToHome { get; set; }
        public bool IsCharacterMovementStarted { get; set; }
        public List<Vector2> Path { get; private set; } = new List<Vector2>();
        public int CharactersAtHome { get; set; } = 0;
        #endregion
    }
}
