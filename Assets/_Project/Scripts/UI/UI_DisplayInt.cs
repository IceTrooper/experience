using Atoms;
using TMPro;
using UnityEngine;

public class UI_DisplayInt : MonoBehaviour
{
    [SerializeField] private TMP_Text displayedText;
    [SerializeField] private IntEvent intEvent;
    [SerializeField] private AtomEvent baseEvent;
    [SerializeField] private bool shouldIncrease = true;

    [Header("Optional settings")]
    [SerializeField] private bool isDelayed;
    [SerializeField] private float delayDuration = 2f;
    [SerializeField] private AnimationCurve delayCurve;

    private int desiredValue;
    private int currentValue;
    private float remainingTime;
    private int delayedStartValue;

    private void Start()
    {
        SetText();
    }

    private void OnEnable()
    {
        intEvent?.Register(OnValueChanged);
        baseEvent?.Register(OnValueChanged);
    }

    private void OnDisable()
    {
        intEvent?.Unregister(OnValueChanged);
        baseEvent?.Unregister(OnValueChanged);
    }

    private void Update()
    {
        if(isDelayed && remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            currentValue = Mathf.CeilToInt(Mathf.Lerp(delayedStartValue, desiredValue, delayCurve.Evaluate((delayDuration - remainingTime) / delayDuration)));
            SetText();
        }
    }

    private void OnValueChanged()
    {
        OnValueChanged(1);
    }

    private void OnValueChanged(int newValue)
    {
        if(shouldIncrease)
        {
            desiredValue += newValue;
        }
        else
        {
            desiredValue = newValue;
        }

        if(!isDelayed)
        {
            currentValue = desiredValue;
            SetText();
        }
        else
        {
            delayedStartValue = currentValue;
            remainingTime = delayDuration;
        }
    }

    private void SetText()
    {
        displayedText.text = currentValue.ToString();
    }

    private void Reset()
    {
        displayedText = GetComponent<TMP_Text>();
    }
}
