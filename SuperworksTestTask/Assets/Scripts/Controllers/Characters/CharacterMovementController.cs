using System;
using System.Collections.Generic;
using UnityEngine;
using ZiplineValley.Models.Level;
using ZiplineValley.Views.Character;
using ZiplineValley.Views.CharacterCounter;
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
        private float _minPassedDistanceToMoveNextCharacter = 0.5f;

        private LevelModel levelModel;
        private bool areCharactersMoving;
        private int charactersAtStart;

        private List<CharacterMoveData> characterViews = new List<CharacterMoveData> ();

        private CharacterCounterView startCharacterCounter;
        private CharacterCounterView homeCharacterCounter;

        private void Start()
        {
            _userInterfaceView.CharacterControlView.OnMoveCharactersRequested += OnMoveCharactersRequested;
        }

        private void Update()
        {
            TryMoveCharacters();
        }

        private void TryMoveCharacters()
        {
            if (!areCharactersMoving) { return; }

            for (int i = 0; i < characterViews.Count; i++)
            {
                var thisCharacter = characterViews[i];
                if (!thisCharacter.WasMovementStarted)
                {
                    charactersAtStart--;
                    thisCharacter.Character.SetPositionWithOffset(levelModel.Path[0]);
                    startCharacterCounter.SetLeftPart(charactersAtStart);
                    
                    thisCharacter.WasMovementStarted = true;
                }

                CharacterMoveData nextCharacter = null;
                if (i < characterViews.Count - 1)
                {
                    nextCharacter = characterViews[i + 1];
                }

                if (MoveSingleCharacter(thisCharacter)) { continue; }
                
                if (nextCharacter != null)
                {
                    if (thisCharacter.PassedDistance < _minPassedDistanceToMoveNextCharacter)
                    {
                        break;
                    }
                }
            }
        }

        private bool MoveSingleCharacter(CharacterMoveData character)
        {
            character.Character.SetState(CharacterViewState.Moving);

            var characterTransform = character.Character.transform;

            if (character.CurrentPointIndex < 0)
            {
                character.CurrentPointIndex = 1;
                characterTransform.position = levelModel.Path[0];
            }

            var currentTargetPoint = levelModel.Path[character.CurrentPointIndex];

            var speed = levelModel.CharacterMovementSpeed * Time.deltaTime;
            var newPosition = Vector2.MoveTowards(characterTransform.position, currentTargetPoint, speed);
            character.PassedDistance += Vector2.Distance(characterTransform.position, newPosition);
            characterTransform.position = newPosition;

            var distance = Vector2.Distance(characterTransform.position, currentTargetPoint);
            if (distance <= 0.01f)
            {
                characterTransform.position = currentTargetPoint;
                character.CurrentPointIndex++;
            }

            if (character.CurrentPointIndex >= levelModel.Path.Count)
            {
                SetCharacterToHome(character);
                return true;
            }

            return false;
        }

        public void Initialize(
            LevelModel levelModel, 
            CharacterCounterView startCharacterCounter,
            CharacterCounterView homeCharacterCounter)
        {
            try
            {
                this.levelModel = levelModel;
                this.startCharacterCounter = startCharacterCounter;
                this.homeCharacterCounter = homeCharacterCounter;

                InstantiateCharacters();

                homeCharacterCounter.SetLeftPart(0);
                homeCharacterCounter.SetRightPart(levelModel.MinCharactersCountToComplete);
            }
            catch (Exception ex) { Debug.LogException(ex); }
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
                character.name = $"Character{i}";
            }

            characterViews.Reverse();
            charactersAtStart = characterViews.Count;
            
            startCharacterCounter.SetRightPart(levelModel.InitialCharacterCount);
            startCharacterCounter.SetLeftPart(charactersAtStart);
        }

        private CharacterView InstantiateSingleCharacter()
        {
            var character = Instantiate(_prefabCharacter, _charactersParent);
            characterViews.Add(new CharacterMoveData 
            {
                Character = character, 
                CurrentPointIndex = 0 
            });
            return character;
        }

        private void OnMoveCharactersRequested(bool start)
        {
            if (start && levelModel.IsPathAttachedToHome)
            {
                levelModel.IsCharacterMovementStarted = true;
                areCharactersMoving = true;
            }
            else
            {
                areCharactersMoving = false;
            }
        }

        private void SetCharacterToHome(CharacterMoveData character)
        {
            characterViews.Remove(character);

            var homeModel = levelModel.HomeModel;
            var homePosition = Vector2.Lerp(
                homeModel.StartCharacterPosition,
                homeModel.EndCharacterPosition,
                (float)levelModel.CharactersAtHome / (float)levelModel.InitialCharacterCount);
            character.Character.SetPositionWithOffset(homePosition);
            character.Character.SetState(CharacterViewState.Normal);

            levelModel.CharactersAtHome++;
            homeCharacterCounter.SetLeftPart(levelModel.CharactersAtHome);
        }
    }
}
