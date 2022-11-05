using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Splatrika.LongLongStep
{
    public class PressButtonHint : MonoBehaviour
    {
        private const float _unpressedTime = 0.35f;
        private const float _pressTime = 0.25f;
        private const float _holdTime = 0.8f;

        [SerializeField]
        private Image _view;

        [SerializeField]
        private Sprite _unactive;

        [SerializeField]
        private Sprite _active;

        [SerializeField]
        private Sprite _pressed;

        [SerializeField]
        private bool _hold;

        private Coroutine _coroutine;


        public void SetActive(bool value)
        {
            if (value)
            {
                if (_coroutine == null)
                {
                    StartCoroutine(HintCoroutine());
                }
            }
            else
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                }
                _view.sprite = _unactive;
            }
        }


        private void Awake()
        {
            SetActive(false);
        }


        private IEnumerator HintCoroutine()
        {
            var pressTime = _hold ? _holdTime : _pressTime;
            while (gameObject.activeSelf)
            {
                _view.sprite = _active;
                yield return new WaitForSeconds(_unpressedTime);
                _view.sprite = _pressed;
                yield return new WaitForSeconds(pressTime);
            }
        }
    }
}
