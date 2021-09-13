using Atoms;
using TMPro;
using UnityEngine;

public class Scoreable : MonoBehaviour
{
    /// <summary>
    /// Text prefab in world coordinates.
    /// </summary>
    [SerializeField] private GameObject pointsTextPrefab;
    /// <summary>
    /// Event raised when points collected.
    /// </summary>
    [SerializeField] private IntEvent pointsScored;

    /// <summary>
    /// Receive points.
    /// </summary>
    /// <param name="pointsAmount">Points amount</param>
    public void GetPoints(float pointsAmount)
    {
        GetPoints((int)pointsAmount);
    }

    /// <summary>
    /// Receive points.
    /// </summary>
    /// <param name="pointsAmount">Points amount</param>
    public void GetPoints(int pointsAmount)
    {
        GetPoints(pointsAmount, pointsTextPrefab);
    }

    /// <summary>
    /// Receive points
    /// </summary>
    /// <param name="pointsAmount">Points amount</param>
    /// <param name="textPrefab">Prefab to Instantiate when points collected.</param>
    public void GetPoints(int pointsAmount, GameObject textPrefab)
    {
        if(pointsAmount == 0) return;

        if(pointsTextPrefab != null)
        {
            var spawnPosition = transform.position;
            spawnPosition.y += 2f;

            var pointsText = Instantiate(pointsTextPrefab, spawnPosition, transform.rotation * Quaternion.Euler(Vector3.up * 180f)).GetComponent<TMP_Text>();
            pointsText.text = pointsAmount.ToString();
        }

        pointsScored.Raise(pointsAmount);
    }
}
