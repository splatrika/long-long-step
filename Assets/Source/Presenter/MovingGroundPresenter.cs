using System.Collections;
using System.Collections.Generic;
using Splatrika.LongLongStep.Architecture;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Presenter
{
    public class MovingGroundPresenter : Presenter<MovingGround>
    {
        private Vector3 _anchorOffset;
        private Transform _transform;


        public void Init(Transform selfTransform, Transform anchor)
        {
            _transform = selfTransform;

            _anchorOffset = anchor.position - selfTransform.position;
        }


        protected override void OnUpdate(float deltaTime)
        {
            _transform.position = TypedModel.Anchor - _anchorOffset;
        }
    }
}
