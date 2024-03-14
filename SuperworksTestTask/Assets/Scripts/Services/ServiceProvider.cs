using UnityEngine;

namespace ZiplineValley.Services
{
    public class ServiceProvider : MonoBehaviour
    {
        private static ServiceProvider serviceProvider;

        private void Awake()
        {
            serviceProvider = this;
        }
    }
}