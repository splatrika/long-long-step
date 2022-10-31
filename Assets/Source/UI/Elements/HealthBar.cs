using UnityEngine;
using UnityEngine.UI;

namespace Splatrika.LongLongStep
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _lifes;

        [SerializeField]
        private Image _noLifes;


        public void SetMaxLifes(int value)
        {
            _noLifes.rectTransform.sizeDelta = new Vector2(
                x: _noLifes.sprite.rect.width * value,
                y: _noLifes.sprite.rect.height);
        }


        public void SetLifes(int value)
        {
            _lifes.rectTransform.sizeDelta = new Vector2(
                x: _lifes.sprite.rect.width * value,
                y: _lifes.sprite.rect.height);
        }
    }
}
