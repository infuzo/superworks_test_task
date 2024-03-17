using System.Collections.Generic;
using UnityEngine;
using ZiplineValley.Models.Home;
using ZiplineValley.Models.Level;
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
        [SerializeField]
        private float _charactersMovementSpeed = 10f;

        private LevelModel levelModel;

        private List<CharacterView> characterViews = new List<CharacterView> ();

        private void Start()
        {
            _userInterfaceView.CharacterControlView.OnMoveCharactersRequested += OnMoveCharactersRequested;
        }

        public void Initialize(
            LevelModel levelModel)
        {
            this.levelModel = levelModel;

            InstantiateCharacters();
        }

        private void InstantiateCharacters()
        {
            for (int i = 0; i < levelModel.InitialCharacterCount; i++)
            {
                var character = InstantiateSingleCharacter();

                character.SetState(CharacterViewState.Normal);
                character.SetPositionWithOffset(Vector2.Lerp(
                    levelModel.StartPlatformModel.StartCharacterPosition,
                    levelModel.StartPlatformModel.EndCharacterPosition,
                    (float)i / (float)levelModel.InitialCharacterCount));
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
            Debug.Log(levelModel.IsPathAttachedToHome);
        }

    }
}
