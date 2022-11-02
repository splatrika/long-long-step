using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Scene
{
    [CreateAssetMenu(menuName = "LongLongStep/Application Settings")]
    public class ApplictionSettings : ScriptableObject
    {
        [SerializeField]
        private string _gitHubUrl;


        public ApplicationConfiguration GetConfiguration()
        {
            return new ApplicationConfiguration(
                _gitHubUrl, Application.version);
        }
    }
}
