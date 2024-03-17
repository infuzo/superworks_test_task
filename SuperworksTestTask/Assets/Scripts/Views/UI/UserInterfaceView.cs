using UnityEngine;
using ZiplineValley.Views.UI.CharacterControl;

namespace ZiplineValley.Views.UI
{
    public class UserInterfaceView : MonoBehaviour
    {
        [SerializeField]
        private CharacterControlView _characterControlView;

        public CharacterControlView CharacterControlView => _characterControlView;
    }
}
