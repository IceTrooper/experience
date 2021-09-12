using Atoms;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private AtomEvent playerDiedEvent;

    private void OnEnable()
    {
        playerDiedEvent.Register(GameOver);
    }

    private void OnDisable()
    {
        playerDiedEvent.Unregister(GameOver);
    }

    public void GameOver()
    {
        Debug.Log("Player died. Game over!");
    }
}
