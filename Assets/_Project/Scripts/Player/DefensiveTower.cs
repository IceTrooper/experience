using Atoms;
using UnityEngine;

public class DefensiveTower : MonoBehaviour
{
    [SerializeField] private TransformList playerFortressList;

    private void OnEnable()
    {
        playerFortressList.Items.Add(transform);
    }

    private void OnDisable()
    {
        playerFortressList.Items.Remove(transform);
    }
}
