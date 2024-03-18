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
        private TMP_Text _textHeader;
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
            bool success,
            int aliveCharacters,
            int minCharacters,
            int initialCharacters)
        {
            try
            {
                if (success)
                {
                    _textHeader.text = aliveCharacters >= initialCharacters
                        ? "PERFECT!"
                        : "COMPLETED";
                }
                else
                {
                    _textHeader.text = "FAILED";
                }

                if (success)
                {
                    _textStatistics.text =
                        $"Alive characters: {aliveCharacters}";
                }
                else
                {
                    if (aliveCharacters <= 0)
                    {
                        _textStatistics.text = "All characters died";
                    }
                    else
                    {
                        _textStatistics.text = $"You need to escort {minCharacters - aliveCharacters} characters more";
                    }
                }

                gameObject.SetActive(true);
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }
    }
}
