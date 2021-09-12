using System;
using TMPro;
using UnityEngine;

public class UI_DisplayTime : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    private void SetText()
    {
        var t = TimeSpan.FromSeconds(Time.timeSinceLevelLoad);
        timeText.text = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    private void Update()
    {
        SetText();
    }

    private void Reset()
    {
        timeText = GetComponent<TMP_Text>();
    }
}
