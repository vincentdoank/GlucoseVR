using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FillTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float duration = 3f;
    [SerializeField] private bool playOnStart = true;

    [Header("UI")]
    [SerializeField] private Image targetImage;

    [Header("Events")]
    public UnityEvent onTimerComplete;

    private float currentTime;
    private bool isRunning;

    private void OnEnable()
    {
        if (playOnStart)
            StartTimer();
    }

    private void Update()
    {
        if (!isRunning) return;

        currentTime += Time.deltaTime;

        float progress = Mathf.Clamp01(currentTime / duration);

        if (targetImage != null)
            targetImage.fillAmount = progress;

        if (currentTime >= duration)
        {
            CompleteTimer();
        }
    }

    public void StartTimer()
    {
        currentTime = 0f;
        isRunning = true;

        if (targetImage != null)
            targetImage.fillAmount = 0f;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        currentTime = 0f;
        isRunning = false;

        if (targetImage != null)
            targetImage.fillAmount = 0f;
    }

    private void CompleteTimer()
    {
        isRunning = false;

        if (targetImage != null)
            targetImage.fillAmount = 1f;
        Debug.LogWarning("complete", gameObject);
        onTimerComplete?.Invoke();
    }
}