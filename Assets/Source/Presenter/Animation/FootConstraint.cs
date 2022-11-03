using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter
{
    public class FootConstraint : MonoBehaviour
    {
        [SerializeField]
        private Transform _firstFootAnchor;

        [SerializeField]
        private Transform _secondFootAnchor;

        [SerializeField]
        private Transform _stepTargetAnchor;

        [SerializeField]
        private Transform _firstFootBone;

        [SerializeField]
        private Transform _secondFootBone;

        [SerializeField]
        private Foot _firstFootOptions;

        [SerializeField]
        public Foot _secondFootOptions;

        [SerializeField]
        private float _checkFrequency;

        [SerializeField]
        private float _moveDuration;


        private void Start()
        {
            StartCoroutine(UpdateCoroutine());
        }


        private IEnumerator UpdateCoroutine()
        {
            while (true)
            {
                var firstFootDistance = Vector3.Distance(
                    _firstFootAnchor.position, _stepTargetAnchor.position);
                var secondFootDistance = Vector3.Distance(
                    _secondFootAnchor.position, _stepTargetAnchor.position);
                var frontFoot = (firstFootDistance > secondFootDistance)
                    ? _firstFootBone
                    : _secondFootBone;
                var backFoot = (frontFoot == _firstFootBone)
                    ? _secondFootBone
                    : _firstFootBone;

                var front = (frontFoot == _firstFootBone)
                    ? _firstFootOptions
                    : _secondFootOptions;
                var back = (backFoot == _firstFootBone)
                    ? _firstFootOptions
                    : _secondFootOptions;

                frontFoot
                    .DOLocalMove(front.FrontPosition, _moveDuration)
                    .SetAutoKill();
                frontFoot
                    .DOLocalRotateQuaternion(front.FrontRotation, _moveDuration)
                    .SetAutoKill();
                backFoot
                    .DOLocalMove(back.BackPosition, _moveDuration)
                    .SetAutoKill();
                backFoot
                    .DOLocalRotateQuaternion(back.BackRotation, _moveDuration)
                    .SetAutoKill();

                yield return new WaitForSeconds(_checkFrequency);
            }
        }


        [Serializable]
        public class Foot
        {
            public Vector3 FrontPosition;
            public Quaternion FrontRotation;
            public Vector3 BackPosition;
            public Quaternion BackRotation;
        }
    }
}
