using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Splatrika.LongLongStep
{
    public class LevelButton : MonoBehaviour
    {
        public int Index { get; private set; }

        [SerializeField]
        private Button _button;

        [SerializeField]
        private TextMeshProUGUI _text;

        public event Action<LevelButton> Clicked;


        public void Init(int index)
        {
            Index = index;
        }


        public void SetLevel(int id)
        {
            _text.text = (id + 1).ToString();
        }


        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }


        private void Start()
        {
            _button.onClick.AddListener(OnButtonClick);
        }


        private void OnButtonClick()
        {
            Clicked?.Invoke(this);
        }
    }
}
