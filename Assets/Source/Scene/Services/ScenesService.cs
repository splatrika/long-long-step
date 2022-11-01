using Splatrika.LongLongStep.Model;
using UnityEngine.SceneManagement;

namespace Splatrika.LongLongStep.Scene
{
    public class ScenesService : IScenesService
    {
        public void Load(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}
