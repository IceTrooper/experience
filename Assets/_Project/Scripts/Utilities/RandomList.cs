using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomList<T> : RandomizedList<T>
{
    public List<RandomizedElement<T>> elements;

    public static T GetRandomItem(RandomizedElement<T>[] elements)
    {
        return elements[Random.Range(0, elements.Length)].item;
    }

    public override T GetRandomItem()
    {
        return GetRandomItem(elements.ToArray());
    }
}