using Atoms;
using UnityEngine;

public class DefensiveTower : MonoBehaviour
{
    [SerializeField] private TransformList playerFortressList;

    [Header("Healing")]
    [SerializeField] private Damagable playerDamagable;
    [SerializeField] private float healAmount = 20f;
    [SerializeField] private float healDelay = 3f;

    private float remainingHealTime;

    private void OnEnable()
    {
        playerFortressList.Items.Add(transform);
    }

    private void OnDisable()
    {
        playerFortressList.Items.Remove(transform);
    }

    private void Update()
    {
        remainingHealTime -= Time.deltaTime;

        if(remainingHealTime <= 0f)
        {
            playerDamagable.Heal(healAmount);
            remainingHealTime = healDelay;
        }
    }
}
