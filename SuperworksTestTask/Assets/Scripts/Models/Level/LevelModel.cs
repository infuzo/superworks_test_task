using System;
using System.Collections.Generic;
using UnityEngine;
using ZiplineValley.Models.Home;
using ZiplineValley.Models.StartPlatform;

namespace ZiplineValley.Models.Level
{
    public class LevelModel : MonoBehaviour
    {
        #region Predefined values

        [SerializeField]
        private HomeModel _homeModel;
        [SerializeField]
        private StartPlatformModel _startPlatformModel;
        [SerializeField]
        private int _initialCharacterCount = 5;
        [SerializeField]
        private float _charactersMovementSpeed = 3f;
        [SerializeField]
        private int _minCharactersCountToComplete = 3;

        public HomeModel HomeModel => _homeModel;
        public StartPlatformModel StartPlatformModel => _startPlatformModel;
        public int InitialCharacterCount => _initialCharacterCount;
        public float CharacterMovementSpeed => _charactersMovementSpeed;
        public int MinCharactersCountToComplete => _minCharactersCountToComplete;

        #endregion

        #region Values changing during the gameplay

        public event Action OnCharactersAtHomeValueChanged = delegate { };
        public event Action OnAliveCharactersValueChanged = delegate { };

        public bool IsPathChanging { get; set; }
        public bool IsPathAttachedToHome { get; set; }
        public bool IsCharacterMovementStarted { get; set; }
        public List<Vector2> Path { get; private set; } = new List<Vector2>();

        private int ro_charactersAtHome = 0;
        public int CharactersAtHome
        {
            get => ro_charactersAtHome;
            set
            {
                ro_charactersAtHome = value;
                try
                {
                    OnCharactersAtHomeValueChanged?.Invoke();
                }
                catch (Exception ex) { Debug.LogException(ex); }
            }
        }

        private int ro_aliveCharacters = 0;
        public int AliveCharacters
        {
            get => ro_aliveCharacters;
            set
            {
                ro_aliveCharacters = value;
                try
                {
                    OnAliveCharactersValueChanged?.Invoke();
                }
                catch (Exception ex) { Debug.LogException(ex); }
            }
        }

        #endregion
    }
}
