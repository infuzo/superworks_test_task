using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZiplineValley.Views.UI.CharacterControl
{
    public class CharacterControlView :
        MonoBehaviour,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerExitHandler
    {
        public event Action<bool> OnMoveCharactersRequested = delegate { };

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnMoveCharactersRequested.Invoke(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            OnMoveCharactersRequested.Invoke(false);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            OnMoveCharactersRequested.Invoke(false);
        }
    }
}
