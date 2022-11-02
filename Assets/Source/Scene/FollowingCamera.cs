using UnityEngine;

namespace Splatrika.LongLongStep.Scene
{
    public class FollowingCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        private Vector3 _offset;
        private Transform _transform;


        private void Start()
        {
            _transform = transform;
            _offset = transform.position - _target.position;
        }


        private void Update()
        {
            _transform.position = _target.position + _offset;
        }
    }
}
