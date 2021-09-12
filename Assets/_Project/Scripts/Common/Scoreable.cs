using Atoms;
using TMPro;
using UnityEngine;

public class Scoreable : MonoBehaviour
{
    [SerializeField] private GameObject pointsTextPrefab;
    [SerializeField] private IntEvent pointsScored;

    public void GetPoints(float pointsAmount)
    {
        GetPoints((int)pointsAmount);
    }

    public void GetPoints(int pointsAmount)
    {
        GetPoints(pointsAmount, pointsTextPrefab);
    }

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
