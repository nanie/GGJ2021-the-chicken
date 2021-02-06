using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingParticle : MonoBehaviour
{
    [SerializeField] private GameObject _particleObject;
    [SerializeField] private float _duration = 1;

    public RectTransform teste1;
    public Transform teste2;

    private void Start()
    {
        StartAnimation(teste2.position, teste1);
    }
    public void StartAnimation(Vector2 startPosition, Vector2 endPosition)
    {
        StartCoroutine(AnimateParticle(startPosition, endPosition));
    }

    public void StartAnimation(RectTransform startPosition, Vector2 endPosition)
    {
        var startPositionConverted = Camera.main.ScreenToWorldPoint(startPosition.position);
        StartAnimation(startPositionConverted, endPosition);
    }

    public void StartAnimation(RectTransform startPosition, RectTransform endPosition)
    {
        var startPositionConverted = Camera.main.ScreenToWorldPoint(startPosition.position);
        var endPositionConverted = Camera.main.ScreenToWorldPoint(endPosition.position);
        StartAnimation(startPositionConverted, endPositionConverted);
    }

    public void StartAnimation(Vector2 startPosition, RectTransform endPosition)
    {
        var endPositionConverted = Camera.main.ScreenToWorldPoint(endPosition.position);
        StartAnimation(startPosition, endPositionConverted);
    }

    IEnumerator AnimateParticle(Vector2 start, Vector2 end)
    {
        _particleObject.transform.position = start;
        float elapsed_time = 0;
        while (Vector2.Distance(_particleObject.transform.position, end) > 0.1f)
        {
            _particleObject.transform.position = Vector3.Lerp(start, end, elapsed_time / _duration);
            yield return null;
            elapsed_time += Time.deltaTime;
        }
    }
}
