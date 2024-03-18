using System;
using TMPro;
using UnityEngine;

namespace ZiplineValley.Views.CharacterCounter
{
    public class CharacterCounterView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _textRemains;
        [SerializeField]
        private TMP_Text _textCurrent;

        public void SetLeftPart(int count)
        {
            try
            {
                _textRemains.text = count.ToString();
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }

        public void SetRightPart(int count)
        {
            try
            {
                _textCurrent.text = count.ToString();
            }
            catch (Exception ex) { Debug.LogException(ex); }
        }
    }
}
