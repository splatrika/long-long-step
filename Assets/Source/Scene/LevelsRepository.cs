using System;
using System.Collections.Generic;
using System.Linq;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Scene
{
    [CreateAssetMenu(menuName = "LongLongStep/LevelsRepository")]
    public class LevelsRepository : ScriptableObject, ILevelsRepositoryService
    {
        [SerializeField]
        private Item[] _levels;

        private IReadOnlyCollection<LevelInfo> _levelsInfos;


        private void OnEnable()
        {
            var i = 0;
            _levelsInfos = _levels.Select(x =>
            {
                var info = new LevelInfo(i, x.Scene);
                i++;
                return info;
            })
                .ToList()
                .AsReadOnly();
        }


        public bool Contains(int id)
        {
            return id >= 0 && id < _levels.Length;
        }


        public LevelInfo Get(int id)
        {
            return _levelsInfos.ElementAt(id);
        }


        public IEnumerable<LevelInfo> GetAll()
        {
            return _levelsInfos;
        }


        [Serializable]
        public class Item
        {
            public string Scene;
        }
    }
}
