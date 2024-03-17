using UnityEngine;
using ZiplineValley.Models.Home;
using ZiplineValley.Models.StartPlatform;
using ZiplineValley.Views.Character;

namespace ZiplineValley.Controllers.Characters
{
    public class CharacterMovementController : BaseController
    {
        [SerializeField]
        private CharacterView _prefabCharacter;

        public void Initialize(
            StartPlatformModel startPlatform, 
            HomeModel homeModel)
        {

        }
    }
}
