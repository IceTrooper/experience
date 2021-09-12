using Atoms;
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

        pointsScored.Raise(pointsAmount);
    }
}
