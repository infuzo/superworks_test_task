using UnityEngine;
using ZiplineValley.Controllers.Characters;
using ZiplineValley.Controllers.PathBuilder;
using ZiplineValley.Models.Level;
using ZiplineValley.Views.CharacterCounter;
using ZiplineValley.Views.UI;

namespace ZiplineValley.Controllers
{
    /// <summary>
    /// This is an entry point for an entire gameplay. 
    /// All controllers of the game should be initialized here.
    /// </summary>
    public class LevelController : BaseController
    {
        [SerializeField]
        private LevelModel _levelModel;

        [Space, SerializeField]
        private CharacterCounterView _startCharacterCounter;
        [SerializeField]
        private CharacterCounterView _homeCharacterCounter;
        [SerializeField]
        private UserInterfaceView _userInterface;

        private PathBuilderController pathBuilderController => ControllersStorage.TryGetController<PathBuilderController>();
        private CharacterMovementController characterMovementController => ControllersStorage.TryGetController<CharacterMovementController>();

        private void Start()
        {
            _levelModel.OnCharactersAtHomeValueChanged += OnCharactersAtHomeValueChanged;

            LaunchLevel();
        }

        private void OnCharactersAtHomeValueChanged()
        {
            if (_levelModel.CharactersAtHome >= _levelModel.AliveCharacters)
            {
                _userInterface.EndGamePopupView.Show(_levelModel.AliveCharacters, _levelModel.MinCharactersCountToComplete);
            }
        }

        private void LaunchLevel()
        {
            pathBuilderController.Initialize(_levelModel);
            characterMovementController.Initialize(
                _levelModel, 
                _startCharacterCounter, 
                _homeCharacterCounter);
        }
    }
}
