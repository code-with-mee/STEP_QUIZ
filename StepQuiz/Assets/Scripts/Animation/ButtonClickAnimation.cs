using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickAnimation : MonoBehaviour, IPointerClickHandler
{
    public float scaleAmount = 1.1f;
    public float duration = 0.1f;

    private Vector3 originalScale;
    private float t = 0f;
    private bool playing = false;
    private bool goingUp = true;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        t = 0f;
        goingUp = true;
        playing = true;
    }

    void Update()
    {
        if (!playing) return;

        t += Time.unscaledDeltaTime;
        float progress = t / duration;

        if (goingUp)
        {
            transform.localScale = Vector3.Lerp(originalScale, originalScale * scaleAmount, progress);
            if (progress >= 1f)
            {
                goingUp = false;
                t = 0f;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(originalScale * scaleAmount, originalScale, progress);
            if (progress >= 1f)
            {
                transform.localScale = originalScale;
                playing = false;
            }
        }
    }
}
