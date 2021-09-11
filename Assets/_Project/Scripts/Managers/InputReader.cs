using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "IceTrooper/InputReader")]
public class InputReader : ScriptableObject
{
    public event UnityAction attackEvent;

    public void OnAttack()
    {
        if(attackEvent != null)
        {
            attackEvent.Invoke();
        }
    }
}
