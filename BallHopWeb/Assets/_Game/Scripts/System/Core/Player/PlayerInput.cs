using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnJump { get; private set; }

    [SerializeField] Slider _uiSlider;

    bool firstMove = false;
    public Image bgImage;

    public static event Action<float> OnPointerDrag;

    private void Awake()
    {
        _uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
        bgImage.color = Color.white;
    }

    public void ResetFirstMove()
    {
        firstMove = false;
    }

    public void OnSliderValueChanged(float value)
    {
        if (!firstMove)
        {
            firstMove = true;
            OnJump?.Invoke();
            StartCoroutine(ChangeBackgroundColor());
        }

        OnPointerDrag?.Invoke(value);
    }
    IEnumerator ChangeBackgroundColor()
    {
        float time = 0;
        float duration = 1;
        while (time < duration)
        {
            bgImage.color = Color.Lerp(bgImage.color, new Color(0.4862745f, 0.4862745f, 0.4862745f), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
