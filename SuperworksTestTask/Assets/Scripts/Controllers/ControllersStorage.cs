using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZiplineValley.Controllers
{
    public class ControllersStorage : MonoBehaviour
    {
        private static ControllersStorage instance;

        private Dictionary<Type, BaseController> controllers = new Dictionary<Type, BaseController>();

        private void Awake()
        {
            instance = this;
        }

        public static T TryGetController<T>() where T : BaseController
        {
            try
            {
                var type = typeof(T);
                if (instance.controllers.TryGetValue(type, out var controller) && controller != null)
                {
                    return (T)controller;
                }

                controller = FindObjectOfType<T>(true);
                if (controller == null)
                {
                    Debug.LogError($"Can't find a controller of type {type}");
                }
                else
                {
                    instance.controllers[type] = controller;
                    return (T)controller;
                }
            }
            catch (Exception ex) { Debug.LogException(ex); }
            return null;
        }
    }
}
