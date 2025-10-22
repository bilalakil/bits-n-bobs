using TMPro;
using UnityEngine;

namespace BitsNBobs.UI.Text
{
    public class GameTime : MonoBehaviour
    {
        TextMeshProUGUI _text;
        string _stringFormat;

        public void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _stringFormat = _text.text;
        }

        public void Update()
        {
            _text.text = string.Format(_stringFormat, (int)Time.time);
        }
    }
}