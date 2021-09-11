using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedList<T> : RandomizedList<T>
{
    public List<WeightedElement<T>> elements;

    public static T GetRandomItem(WeightedElement<T>[] elements)
    {
        float[] incrementalWeights = new float[elements.Length];
        incrementalWeights[0] = elements[0].weight;
        for(int i = 1; i < incrementalWeights.Length; i++)
        {
            incrementalWeights[i] = incrementalWeights[i - 1] + elements[i].weight;
        }

        float randomResult = Random.Range(0.0f, incrementalWeights[incrementalWeights.Length - 1]);

        for(int i = 0; i < incrementalWeights.Length; i++)
        {
            if(randomResult <= incrementalWeights[i])
            {
                return elements[i].item;
            }
        }

        throw new System.ArgumentOutOfRangeException();
    }

    public override T GetRandomItem()
    {
        return GetRandomItem(elements.ToArray());
    }
}

[System.Serializable]
public class WeightedElement<T> : RandomizedElement<T>
{
    public float weight;
}