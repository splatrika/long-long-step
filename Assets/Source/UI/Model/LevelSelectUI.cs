using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Splatrika.LongLongStep.Model;
using UnityEngine;

namespace Splatrika.LongLongStep
{
    public class LevelSelectUI
    {
        public Page Current { get; private set; }
        public int LevelsOnPage { get; }
        public int PagesCount { get; }
        public int LevelsCount { get; }
        public bool PreviousEnabled { get; private set; }
        public bool NextEnabled { get; private set; }

        private ILevelsRepositoryService _repository { get; }
        private ILevelsManagementService _managementService { get; }

        public event Action<Page> PageChanged;


        public LevelSelectUI(
            ILevelsRepositoryService repository,
            ILevelsManagementService managementService,
            LevelSelectUIConfiguration configuration)
        {
            _repository = repository;
            _managementService = managementService;

            LevelsOnPage = configuration.LevelsOnPage;
            LevelsCount = _repository.GetAll().Count();
            PagesCount = (LevelsCount - 1) / LevelsOnPage + 1;

            SetPage(0);
        }


        public bool GoNextPage()
        {
            if (Current.Index == PagesCount - 1)
            {
                return false;
            }
            SetPage(Current.Index + 1);
            return true;
        }


        public bool GoPreviousPage()
        {
            if (Current.Index == 0)
            {
                return false;
            }
            SetPage(Current.Index - 1);
            return true;
        }


        public void SelectLevel(int id)
        {
            _managementService.Load(id);
        }


        private void SetPage(int page)
        {
            var firstLevel = LevelsOnPage * page;
            var levelsCount = LevelsCount - firstLevel;
            if (levelsCount > LevelsOnPage)
            {
                levelsCount = LevelsOnPage;
            }
            Current = new Page(
                index: page,
                firstLevel: firstLevel,
                levelsCount: levelsCount);

            PreviousEnabled = page != 0;
            NextEnabled = page != PagesCount - 1;

            PageChanged?.Invoke(Current);
        }


        public struct Page
        {
            public int Index { get; }
            public int FirstLevel { get; }
            public int LevelsCount { get; }


            public Page(int index, int firstLevel, int levelsCount)
            {
                Index = index;
                FirstLevel = firstLevel;
                LevelsCount = levelsCount;
            }
        }
    }
}
