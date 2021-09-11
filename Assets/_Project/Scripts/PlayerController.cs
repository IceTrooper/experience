using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Gun gun;

    [SerializeField] private float shootDelay = 0.2f;
    private float delay;

    private void OnEnable()
    {
        inputReader.attackEvent += OnAttack;
    }

    private void OnDisable()
    {
        inputReader.attackEvent -= OnAttack;
    }

    private void Update()
    {
        if(delay > 0f)
        {
            delay -= Time.deltaTime;
        }
    }

    public void OnAttack()
    {
        UseActiveGun();
    }

    private void UseActiveGun()
    {
        if(delay > 0f) return;

        delay = shootDelay;
        gun.Fire();
    }
}
