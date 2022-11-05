using System.Collections.Generic;
using Splatrika.LongLongStep.Architecture;
using UnityEngine;
using Zenject;

namespace Splatrika.LongLongStep.Scene
{
    public class ServicesUpdater : MonoBehaviour
    {
        private List<IUpdatable> _updatables;
        private List<IFixedUpdatable> _fixedUpdatables;


        [Inject]
        public void Init(
            List<IUpdatable> updatables,
            List<IFixedUpdatable> fixedUpdatables)
        {
            _updatables = updatables;
            _fixedUpdatables = fixedUpdatables;
        }


        private void Update()
        {
            for (var i = 0; i < _updatables.Count; i++)
            {
                _updatables[i].Update(Time.deltaTime);
            }
        }


        private void FixedUpdate()
        {
            for (var i = 0; i < _fixedUpdatables.Count; i++)
            {
                _fixedUpdatables[i].FixedUpdate(Time.fixedDeltaTime);
            }
        }
    }
}
