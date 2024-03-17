using UnityEngine;
using ZiplineValley.Controllers.Characters;
using ZiplineValley.Controllers.PathBuilder;
using ZiplineValley.Models.Level;

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

        private PathBuilderController pathBuilderController => ControllersStorage.TryGetController<PathBuilderController>();
        private CharacterMovementController characterMovementController => ControllersStorage.TryGetController<CharacterMovementController>();

        private void Start()
        {
            LaunchLevel();
        }

        private void LaunchLevel()
        {
            pathBuilderController.Initialize(_levelModel.StartPlatformModel, _levelModel.HomeModel);
            characterMovementController.Initialize(_levelModel.StartPlatformModel, _levelModel.HomeModel, _levelModel.InitialCharacterCount);
        }
    }
}
