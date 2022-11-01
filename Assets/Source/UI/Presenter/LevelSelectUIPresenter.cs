using Splatrika.LongLongStep.Architecture;
using UnityEngine.UI;

namespace Splatrika.LongLongStep
{
    public class LevelSelectUIPresenter : Presenter<LevelSelectUI>
    {
        private LevelButton[] _levelButtons;
        private Button _previousPage;
        private Button _nextPage;


        public void Init(
            LevelButton[] levelButtons,
            Button previousPage,
            Button nextPage)
        {
            _levelButtons = levelButtons;
            _previousPage = previousPage;
            _nextPage = nextPage;

            _previousPage.onClick.AddListener(OnPreviousPageClicked);
            _nextPage.onClick.AddListener(OnNextPageClicked);

            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].Init(i);
                _levelButtons[i].Clicked += OnLevelButtonClicked;
            }

            OnPageChanged(TypedModel.Current);
        }


        protected override sealed void OnConstruct(LevelSelectUI model)
        {
            model.PageChanged += OnPageChanged;
        }


        protected override sealed void OnDispose()
        {
            TypedModel.PageChanged -= OnPageChanged;

            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].Clicked -= OnLevelButtonClicked;
            }
        }


        private void OnPageChanged(LevelSelectUI.Page page)
        {
            for (int i = 0; i < _levelButtons.Length; i++)
            {
                _levelButtons[i].SetLevel(page.FirstLevel + i);
                _levelButtons[i].SetActive(i < page.LevelsCount);
            }
            _previousPage.interactable = TypedModel.PreviousEnabled;
            _nextPage.interactable = TypedModel.NextEnabled;
        }


        private void OnNextPageClicked()
        {
            TypedModel.GoNextPage();
        }


        private void OnPreviousPageClicked()
        {
            TypedModel.GoPreviousPage();
        }


        private void OnLevelButtonClicked(LevelButton sender)
        {
            var level = TypedModel.Current.FirstLevel + sender.Index;
            TypedModel.SelectLevel(level);
        }
    }
}
