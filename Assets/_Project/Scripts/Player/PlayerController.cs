using Atoms;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Gun gun;
    [SerializeField] private TransformList playerFortressList;
    [SerializeField] private AtomEvent playerDiedEvent;

    [Header("Shooting")]
    [SerializeField] private Image reloadFillingImage;
    [SerializeField] private float shootDelay = 0.2f;
    private float delay;

    private void OnEnable()
    {
        inputReader.attackEvent += OnAttack;
        playerFortressList.Items.Add(transform);
    }

    private void OnDisable()
    {
        inputReader.attackEvent -= OnAttack;
        playerFortressList.Items.Remove(transform);
    }

    private void Update()
    {
        if(delay > 0f)
        {
            delay -= Time.deltaTime;
            reloadFillingImage.fillAmount = (shootDelay - delay) / shootDelay;
        }
    }

    /// <summary>
    /// Handler for Attack event.
    /// </summary>
    public void OnAttack()
    {
        UseActiveGun();
    }

    /// <summary>
    /// Tries to shoot with the active gun.
    /// </summary>
    private void UseActiveGun()
    {
        if(delay > 0f) return;

        delay = shootDelay;
        gun.Fire();
    }

    /// <summary>
    /// Handler for Die event.
    /// </summary>
    public void OnDie()
    {
        playerDiedEvent.Raise();
    }
}
