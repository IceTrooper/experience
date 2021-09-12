using Atoms;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private AtomEvent playerDiedEvent;
    [SerializeField] private AtomEvent gameOverEvent;
    [SerializeField] private GameObject gameOverObject;

    [Header("Difficulty")]
    [SerializeField] private bool increaseDifficulty = true;
    // Can be changed to array of abstracts Spawner classes
    [SerializeField] private RoundSpawner spawner;
    [Tooltip("In what time (in seconds) change difficulty to max value")]
    [SerializeField] private float difficultyChangeDuration = 360f;
    [SerializeField] private float minSpawnerRatio = 0.2f;
    [SerializeField] private AnimationCurve difficultyCurve;

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

    private void Update()
    {
        if(increaseDifficulty)
        {
            spawner.delayMultiplier = Mathf.Lerp(1.0f, minSpawnerRatio, difficultyCurve.Evaluate(Mathf.Clamp01(Time.timeSinceLevelLoad / difficultyChangeDuration)));
        }
    }

    public void GameOver()
    {
        Debug.Log("Player died. Game over!");
        Time.timeScale = 0f;
        gameOverEvent.Raise();
        gameOverObject.SetActive(true);
    }
}
