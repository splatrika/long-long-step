using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Scene
{
    public class UrlService : IUrlService
    {
        public void Open(string url)
        {
            Application.OpenURL(url);
        }
    }
}
