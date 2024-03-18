using UnityEngine;
using ZiplineValley.Controllers.Characters;
using ZiplineValley.Controllers.PathBuilder;
using ZiplineValley.Models.Level;
using ZiplineValley.Views.CharacterCounter;

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

        private PathBuilderController pathBuilderController => ControllersStorage.TryGetController<PathBuilderController>();
        private CharacterMovementController characterMovementController => ControllersStorage.TryGetController<CharacterMovementController>();

        private void Start()
        {
            LaunchLevel();
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
