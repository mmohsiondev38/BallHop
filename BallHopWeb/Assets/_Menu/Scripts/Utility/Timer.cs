using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float _duration;

    private float _remainingTime;

    public void StopTimer()
    {
        StopCoroutine(CalculateTimer());
    }

    public IEnumerator CalculateTimer(Action<float> amount = null, Action onTimerEnd = null)
    {
        _remainingTime = _duration;
        while (_remainingTime > 0)
        {
            amount(Mathf.InverseLerp(0, _duration, _remainingTime));
            _remainingTime -= Time.deltaTime;
            yield return null;
        }
        onTimerEnd?.Invoke();
    }
}
