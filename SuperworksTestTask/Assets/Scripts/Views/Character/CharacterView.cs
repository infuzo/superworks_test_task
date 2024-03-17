using System;
using UnityEngine;

namespace ZiplineValley.Views.Character
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _normalStateRepresentation;
        [SerializeField]
        private GameObject _movingStateRepresentation;

        public void SetState(CharacterViewState state)
        {
            try
            {
                _normalStateRepresentation.gameObject.SetActive(state == CharacterViewState.Normal);
                _movingStateRepresentation.gameObject.SetActive(state == CharacterViewState.Moving);    
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }
    }
}
