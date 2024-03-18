using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZiplineValley.Views.UI.EndGamePopup
{
    public class EndGamePopupView : MonoBehaviour
    {
        public event Action OnRestartRequested = delegate { };

        [SerializeField]
        private TMP_Text _textStatistics;
        [SerializeField]
        private Button _buttonRestart;

        private void Start()
        {
            _buttonRestart.onClick.AddListener(OnButtonRestartClick);
        }

        private void OnButtonRestartClick()
        {
            OnRestartRequested.Invoke();
        }

        public void Show(
            int aliveCharacters,
            int minCharacters)
        {
            var successRate = Mathf.RoundToInt(Mathf.Clamp01((float)aliveCharacters / (float)minCharacters) * 100f);

            _textStatistics.text =
                $"Alive characters: {aliveCharacters}\n" +
                $"Success Rate: {successRate} %";

            gameObject.SetActive(true);
        }
    }
}
