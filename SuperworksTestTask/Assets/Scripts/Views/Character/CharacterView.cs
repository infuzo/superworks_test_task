using System;
using UnityEngine;
using ZiplineValley.Views.Traps;

namespace ZiplineValley.Views.Character
{
    public class CharacterView : MonoBehaviour
    {
        public event Action OnTrapCollision = delegate { }; 

        [SerializeField]
        private GameObject _normalStateRepresentation;
        [SerializeField]
        private GameObject _movingStateRepresentation;
        [SerializeField]
        private Vector2 _normalPositionOffset;
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public void SetState(CharacterViewState state)
        {
            try
            {
                _normalStateRepresentation.gameObject.SetActive(state == CharacterViewState.Normal);
                _movingStateRepresentation.gameObject.SetActive(state == CharacterViewState.Moving);    
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }

        public void SetPosition(Vector2 position)
        {
            try
            {
                if (_normalStateRepresentation.activeInHierarchy)
                {
                    _rigidbody.position = position + _normalPositionOffset;
                }
                else
                {
                    _rigidbody.position = position;
                }
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }

        public Vector2 GetPosition()
        {
            try
            {
                return _rigidbody.position;
            }
            catch (Exception ex) { Debug.LogException(ex); }
            return Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<TrapView>() != null)
            {
                OnTrapCollision.Invoke();
            }
        }
    }
}
