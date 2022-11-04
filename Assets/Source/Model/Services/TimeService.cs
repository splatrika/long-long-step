using System.Collections.Generic;

namespace Splatrika.LongLongStep.Model
{
    public class TimeService : ITimeService
    {
        private List<IRevertableAction> _registered;


        public TimeService()
        {
            _registered = new List<IRevertableAction>();
        }


        public void Register(IRevertableAction @object)
        {
            _registered.Add(@object);
        }


        public void Unregister(IRevertableAction @object)
        {
            _registered.Remove(@object);
        }


        public void RevertToPreviousAction()
        {
            foreach (var item in _registered)
            {
                item.RevertToPreviousAction();
            }
        }
    }
}
