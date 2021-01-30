using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSmoothAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 valuesToAdd;
    [SerializeField] private float speed = 1;
    private Vector3 originalPosition;
    bool isOnOriginalPosition;
    void Start()
    {
        originalPosition = transform.position;
        isOnOriginalPosition = true;
    }

    public void Animate()
    {
        StartCoroutine(AnimatePosition());
    }

    IEnumerator AnimatePosition()
    {
        Vector3 target = isOnOriginalPosition ? (originalPosition + valuesToAdd) : originalPosition;
        while (Vector3.Distance(target, transform.position) > 0.01f)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            yield return null;
        }
        isOnOriginalPosition = !isOnOriginalPosition;
    }
}
