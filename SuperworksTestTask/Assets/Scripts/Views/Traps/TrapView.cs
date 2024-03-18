using UnityEngine;

namespace ZiplineValley.Views.Traps
{
    public class TrapView : MonoBehaviour
    {
        [SerializeField]
        private bool _isMoving;
        [SerializeField]
        private Transform _startMovePoint;
        [SerializeField]
        private Transform _endMovePoint;
        [SerializeField]
        private float _moveSpeed = 1f;
        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        private Transform currentMoveTarget;

        private void Start()
        {
            if (_isMoving)
            {
                transform.position = _startMovePoint.position;
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.angularVelocity = 0;
                currentMoveTarget = _endMovePoint;
            }
        }

        private void FixedUpdate()
        {
            TryMove();
        }

        private void TryMove()
        {
            if (!_isMoving) { return; }

            _rigidbody2D.position = Vector2.MoveTowards(_rigidbody2D.position, currentMoveTarget.position, _moveSpeed);

            if (Vector2.Distance(currentMoveTarget.position, _rigidbody2D.position) <= 0.01f)
            {
                currentMoveTarget = currentMoveTarget == _startMovePoint ? _endMovePoint : _startMovePoint;
            }
        }

    }
}
