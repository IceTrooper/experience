using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Points : MonoBehaviour
{
    [SerializeField] private float animationDuration = 1.5f;
    [SerializeField] private float destroyDuration = 1f;
    [SerializeField] private AnimationCurve animationCurve;

    private float elapsedTime;

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if(elapsedTime < animationDuration)
        {
            transform.localScale = Vector3.one * Mathf.Lerp(0f, 1f, animationCurve.Evaluate(elapsedTime / animationDuration));
        }
        else
        {
            Destroy(gameObject, destroyDuration);
        }

        elapsedTime += Time.deltaTime;
    }
}
