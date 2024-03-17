using System.Collections.Generic;
using UnityEngine;
using ZiplineValley.Models.Home;
using ZiplineValley.Models.StartPlatform;
using ZiplineValley.Views.Character;
using ZiplineValley.Views.UI;

namespace ZiplineValley.Controllers.Characters
{
    public class CharacterMovementController : BaseController
    {
        [SerializeField]
        private CharacterView _prefabCharacter;
        [SerializeField]
        private Transform _charactersParent;
        [SerializeField]
        private UserInterfaceView _userInterfaceView;

        private StartPlatformModel startPlatform;
        private HomeModel homeModel;
        private int initialCharactersCount;

        private List<CharacterView> characterViews = new List<CharacterView> ();

        private void Start()
        {
            _userInterfaceView.CharacterControlView.OnMoveCharactersRequested += OnMoveCharactersRequested;
        }

        public void Initialize(
            StartPlatformModel startPlatform, 
            HomeModel homeModel,
            int initialCharactersCount)
        {
            this.startPlatform = startPlatform;
            this.homeModel = homeModel;
            this.initialCharactersCount = initialCharactersCount;

            InstantiateCharacters();
        }

        private void InstantiateCharacters()
        {
            for (int i = 0; i < initialCharactersCount; i++)
            {
                var character = InstantiateSingleCharacter();

                character.SetState(CharacterViewState.Normal);
                character.SetPositionWithOffset(Vector2.Lerp(startPlatform.StartCharacterPosition, startPlatform.EndCharacterPosition,
                    (float)i / (float)initialCharactersCount));
            }
        }

        private CharacterView InstantiateSingleCharacter()
        {
            var character = Instantiate(_prefabCharacter, _charactersParent);
            characterViews.Add(character);
            return character;
        }

        private void OnMoveCharactersRequested(bool start)
        {
            Debug.Log(start);
        }

    }
}
