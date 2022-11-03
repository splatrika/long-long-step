using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private Vector3 _axis;

        private Transform _transform;
        private float _rotationDegress;


        private void Start()
        {
            _transform = transform;
            _rotationDegress = 0;
        }


        private void Update()
        {
            _rotationDegress += _speed * Time.deltaTime;
            _transform.rotation = Quaternion.Euler(_axis * _rotationDegress);
        }
    }
}
