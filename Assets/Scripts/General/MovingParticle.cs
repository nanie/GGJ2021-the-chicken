using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleObject;
    [SerializeField] private float _duration = 1;
    public void StartAnimation(Vector2 startPosition, Vector2 endPosition, GradientColorKey[] particleColor = null)
    {
        var color = particleColor != null ? particleColor : _particleObject.main.startColor.gradient.colorKeys;
        StartCoroutine(AnimateParticle(startPosition, endPosition, color));
    }

    public void StartAnimation(RectTransform startPosition, Vector2 endPosition, GradientColorKey[] particleColor = null)
    {
        var startPositionConverted = Camera.main.ScreenToWorldPoint(startPosition.position);
        StartAnimation(startPositionConverted, endPosition);
    }

    public void StartAnimation(RectTransform startPosition, RectTransform endPosition, GradientColorKey[] particleColor = null)
    {
        var startPositionConverted = Camera.main.ScreenToWorldPoint(startPosition.position);
        var endPositionConverted = Camera.main.ScreenToWorldPoint(endPosition.position);
        StartAnimation(startPositionConverted, endPositionConverted);
    }

    public void StartAnimation(Vector2 startPosition, RectTransform endPosition, GradientColorKey[] particleColor = null)
    {
        var endPositionConverted = Camera.main.ScreenToWorldPoint(endPosition.position);
        StartAnimation(startPosition, endPositionConverted);
    }

    public void StartAnimationMovingTarget(RectTransform startPosition, RectTransform endPosition, GradientColorKey[] particleColor = null)
    {
        var startPositionConverted = Camera.main.ScreenToWorldPoint(startPosition.position);
        StartAnimationMovingTarget(startPositionConverted, endPosition);
    }

    public void StartAnimationMovingTarget(Vector2 startPosition, RectTransform endPosition, GradientColorKey[] particleColor = null)
    {
        var color = particleColor != null ? particleColor : _particleObject.main.startColor.gradient.colorKeys;
        StartCoroutine(AnimateParticleMovingTarget(startPosition, endPosition, color));
    }

    IEnumerator AnimateParticle(Vector2 start, Vector2 end, GradientColorKey[] particleColor)
    {
        _particleObject.gameObject.SetActive(false);
        yield return null;
        _particleObject.transform.position = start;
        _particleObject.gameObject.SetActive(true);
        yield return null;
        float elapsed_time = 0;
        while (Vector2.Distance(_particleObject.transform.position, end) > 0.1f)
        {
            _particleObject.transform.position = Vector3.Lerp(start, end, elapsed_time / _duration);
            yield return null;
            elapsed_time += Time.deltaTime;
        }
    }

    IEnumerator AnimateParticleMovingTarget(Vector2 start, RectTransform movingTarget, GradientColorKey[] particleColor)
    {
       
        ParticleSystem.MainModule psMain = _particleObject.main;     
        _particleObject.gameObject.SetActive(false);
        _particleObject.Emit(1);
        yield return null;
        _particleObject.transform.position = start;
        _particleObject.gameObject.SetActive(true);
        Gradient grad = new Gradient();
        grad.SetKeys(particleColor, psMain.startColor.gradient.alphaKeys);
        psMain.startColor = new ParticleSystem.MinMaxGradient(grad);
        yield return null;
        float elapsed_time = 0;
        var endPos = Camera.main.ScreenToWorldPoint(movingTarget.position);
        while (Vector2.Distance(_particleObject.transform.position, endPos) > 0.01f)
        {
            endPos = Camera.main.ScreenToWorldPoint(movingTarget.position);
            _particleObject.transform.position = Vector3.Lerp(start, endPos, elapsed_time / _duration);
            yield return null;
            elapsed_time += Time.deltaTime;
        }
    }
}
