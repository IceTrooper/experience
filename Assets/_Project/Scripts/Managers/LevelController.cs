using Atoms;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private AtomEvent playerDiedEvent;
    [SerializeField] private AtomEvent gameOverEvent;
    [SerializeField] private GameObject gameOverObject;

    public static bool IsGamePaused => Time.timeScale < 0.01f;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        playerDiedEvent.Register(GameOver);
    }

    private void OnDisable()
    {
        playerDiedEvent.Unregister(GameOver);
    }

    public void GameOver()
    {
        Debug.Log("Player died. Game over!");
        Time.timeScale = 0f;
        gameOverEvent.Raise();
        gameOverObject.SetActive(true);
    }
}
