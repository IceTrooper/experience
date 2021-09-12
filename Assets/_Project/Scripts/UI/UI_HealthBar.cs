using Atoms;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillingBarImage;
    [SerializeField] private Image damageBarImage;
    [SerializeField] private float followingDuration;
    [SerializeField] private AnimationCurve followingEasing;

    [SerializeField] private FloatEvent healthChangedNormalized;

    private float remainingTime = 0f;
    private float followingStartValue;

    private void OnEnable()
    {
        healthChangedNormalized.Register(SetFill);
    }

    private void OnDisable()
    {
        healthChangedNormalized.Unregister(SetFill);
    }

    private void Update()
    {
        if(remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
            damageBarImage.fillAmount = Mathf.Lerp(followingStartValue, fillingBarImage.fillAmount, followingEasing.Evaluate((followingDuration - remainingTime) / followingDuration));
        }
    }

    private void SetFill(float fillAmount)
    {
        fillingBarImage.fillAmount = fillAmount;
        followingStartValue = damageBarImage.fillAmount;
        remainingTime = followingDuration;
    }
}
