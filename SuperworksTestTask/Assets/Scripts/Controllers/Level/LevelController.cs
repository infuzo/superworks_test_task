using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField]
        private GameObject _levelCompletedParticles;

        private PathBuilderController pathBuilderController => ControllersStorage.TryGetController<PathBuilderController>();
        private CharacterMovementController characterMovementController => ControllersStorage.TryGetController<CharacterMovementController>();

        private void Start()
        {
            _levelModel.OnCharactersAtHomeValueChanged += OnCharactersAtHomeValueChanged;
            _levelModel.OnAliveCharactersValueChanged += OnAliveCharactersValueChanged;
            
            _userInterface.EndGamePopupView.OnRestartRequested += OnRestartRequested;
            _userInterface.OnRestartRequested += OnRestartRequested;

            LaunchLevel();
        }

        private void OnAliveCharactersValueChanged()
        {
            if (_levelModel.AliveCharacters == 0)
            {
                _userInterface.EndGamePopupView.Show(false, 0, 0, 0);
            }
            OnCharactersAtHomeValueChanged();
        }

        private void OnRestartRequested()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnCharactersAtHomeValueChanged()
        {
            if (_levelModel.CharactersAtHome >= _levelModel.AliveCharacters)
            {
                var success = _levelModel.AliveCharacters >= _levelModel.MinCharactersCountToComplete;
                if (!success || _levelCompletedParticles == null)
                {
                    _userInterface.EndGamePopupView.Show(
                        success,
                        _levelModel.AliveCharacters, 
                        _levelModel.MinCharactersCountToComplete,
                        _levelModel.InitialCharacterCount);
                }
                else
                {
                    StartCoroutine(CoroutineShowCompletePopupAfterParticles());
                }
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

        private IEnumerator CoroutineShowCompletePopupAfterParticles()
        {
            _levelCompletedParticles.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            _userInterface.EndGamePopupView.Show(
                true,
                _levelModel.AliveCharacters, 
                _levelModel.MinCharactersCountToComplete,
                _levelModel.InitialCharacterCount);
        }
    }
}
