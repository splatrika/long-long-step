using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep.Scene
{
    public class LevelsManagementService : ILevelsManagementService
    {
        public LevelInfo Current { get; private set; }

        private ILogger _logger { get; }
        private ILevelsRepositoryService _repository { get; }
        private IScenesService _scenesService { get; }


        public LevelsManagementService(
            ILogger logger,
            ILevelsRepositoryService repository,
            IScenesService scenesService)
        {
            _logger = logger;
            _repository = repository;
            _scenesService = scenesService;
        }


        public void Load(int id)
        {
            if (!_repository.Contains(id))
            {
                _logger.LogError(nameof(LevelsManagementService),
                    $"{nameof(Load)}: " +
                    $"There is no level with id {id}");
                return;
            }
            var info = _repository.Get(id);
            _scenesService.Load(info.Scene);
            Current = info;
        }


        public bool HasNext()
        {
            if (!CheckCurrent(nameof(HasNext)))
            {
                return false;
            }
            var next = Current.Id + 1;
            return _repository.Contains(next);
        }


        public void TryLoadNext()
        {
            if (!CheckCurrent(nameof(TryLoadNext)) || !HasNext())
            {
                return;
            }
            var next = Current.Id + 1;
            var info = _repository.Get(next);
            Load(next);
        }


        private bool CheckCurrent(string method)
        {
            if (Current == null)
            {
                _logger.LogError(nameof(LevelsManagementService),
                    $"{method}: " +
                    "There is no any loaded level, but you're " +
                    "trying to load next");
                return false;
            }
            return true;
        }
    }
}
