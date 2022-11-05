using Splatrika.LongLongStep.Architecture;
using UnityEngine;

namespace Splatrika.LongLongStep
{
    public class TutorialUIPresenter : Presenter<TutorialUI>
    {
        private PressButtonHint _leftHint;
        private PressButtonHint _rightHint;
        private GameObject _levelUI;


        public void Init(
            PressButtonHint leftHint,
            PressButtonHint rightHint,
            GameObject levelUI)
        {
            _leftHint = leftHint;
            _rightHint = rightHint;
            _levelUI = levelUI;

            _levelUI.SetActive(false);
            HideControlHint();
        }


        protected override sealed void OnConstruct(TutorialUI model)
        {
            model.ShowedControlHint += OnShowedControlHint;
            model.HidedControlHint += OnHidedControlHint;
            model.TutorialFinished += OnTutorialFinished;
        }


        private void OnDestroy()
        {
            TypedModel.ShowedControlHint -= OnShowedControlHint;
            TypedModel.HidedControlHint -= OnHidedControlHint;
            TypedModel.TutorialFinished -= OnTutorialFinished;
        }


        private void Update()
        {
            if (Input.GetButtonDown("Left"))
            {
                TypedModel.SetLeft(true);
            }
            if (Input.GetButtonUp("Left"))
            {
                TypedModel.SetLeft(false);
            }
            if (Input.GetButtonDown("Right"))
            {
                TypedModel.SetRight(true);
            }
            if (Input.GetButtonUp("Right"))
            {
                TypedModel.SetRight(false);
            }
        }


        private void OnShowedControlHint()
        {
            ShowControlHints(TypedModel.HintDirection);
        }


        private void OnHidedControlHint()
        {
            HideControlHint();
        }


        private void OnTutorialFinished()
        {
            gameObject.SetActive(false);
            _levelUI.SetActive(true);
        }


        private void ShowControlHints(TutorialUI.Direction direction)
        {
            _leftHint.gameObject.SetActive(true);
            _rightHint.gameObject.SetActive(true);

            _leftHint.SetActive(direction == TutorialUI.Direction.Left);
            _rightHint.SetActive(direction == TutorialUI.Direction.Right);
        }


        private void HideControlHint()
        {
            _leftHint.gameObject.SetActive(false);
            _rightHint.gameObject.SetActive(false);
        }
    }
}
