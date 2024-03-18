using UnityEngine;
using ZiplineValley.Views.UI.CharacterControl;
using ZiplineValley.Views.UI.EndGamePopup;

namespace ZiplineValley.Views.UI
{
    public class UserInterfaceView : MonoBehaviour
    {
        [SerializeField]
        private CharacterControlView _characterControlView;
        [SerializeField]
        private EndGamePopupView _endGamePopupView;

        public CharacterControlView CharacterControlView => _characterControlView;
        public EndGamePopupView EndGamePopupView => _endGamePopupView;
    }
}
