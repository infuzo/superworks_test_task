using UnityEngine;
using ZiplineValley.Models.Home;

namespace ZiplineValley.Models.Level
{
    public class LevelModel : MonoBehaviour
    {
        [SerializeField]
        private HomeModel _homeModel;

        public HomeModel HomeModel => _homeModel;
    }
}
