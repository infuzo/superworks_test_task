using System;
using UnityEngine;
using UnityEngine.UI;
using ZiplineValley.Views.UI.CharacterControl;
using ZiplineValley.Views.UI.EndGamePopup;

namespace ZiplineValley.Views.UI
{
    public class UserInterfaceView : MonoBehaviour
    {
        public event Action OnRestartRequested = delegate { };

        [SerializeField]
        private CharacterControlView _characterControlView;
        [SerializeField]
        private EndGamePopupView _endGamePopupView;
        [SerializeField]
        private Button _buttonRestart;

        public CharacterControlView CharacterControlView => _characterControlView;
        public EndGamePopupView EndGamePopupView => _endGamePopupView;

        private void Start()
        {
            _buttonRestart.onClick.AddListener(OnButtonRestartClick);
        }

        private void OnButtonRestartClick()
        {
            OnRestartRequested?.Invoke();
        }
    }
}
