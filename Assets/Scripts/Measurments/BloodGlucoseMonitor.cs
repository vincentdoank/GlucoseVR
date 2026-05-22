using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BloodGlucoseMonitor : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text glucoseValueText;
    [SerializeField] private TMP_Text glucoseValueMirrorText;
    [SerializeField] private TMP_Text glucoseStatusText;
    [SerializeField] private TMP_Text glucoseStatusMirrorText;

    [Header("Timing")]
    [SerializeField] private float countdownInterval = 1f;

    [Header("Events")]
    [SerializeField] private UnityEvent onLowResult;
    [SerializeField] private UnityEvent onHighResult;
    [SerializeField] private UnityEvent<string> onResult;

    private Coroutine measureRoutine;

    private int glucoseValue;
    private string glucoseStatus;

    public void StartMeasurement()
    {
        if (measureRoutine != null)
            StopCoroutine(measureRoutine);

        measureRoutine = StartCoroutine(MeasurementRoutine());
    }

    private IEnumerator MeasurementRoutine()
    {
        ClearUI();

        for (int i = 5; i >= 0; i--)
        {
            SetValueText(i.ToString());
            yield return new WaitForSeconds(countdownInterval);
        }

        GenerateResult();
        ShowResult();
        InvokeResultEvent();
    }

    private void GenerateResult()
    {
        bool isLow = Random.value < 0.5f;

        if (isLow)
        {
            glucoseValue = Random.Range(60, 70);
            glucoseStatus = "Low";
        }
        else
        {
            glucoseValue = Random.Range(140, 151);
            glucoseStatus = "High";
        }
    }

    private void ShowResult()
    {
        SetValueText($"{glucoseValue} mg/dL");
        SetStatusText(glucoseStatus);
    }

    private void InvokeResultEvent()
    {
        if (glucoseStatus == "Low")
            onLowResult?.Invoke();
        else if (glucoseStatus == "High")
            onHighResult?.Invoke();

        onResult?.Invoke(glucoseValue.ToString());
    }

    private void ClearUI()
    {
        SetValueText("5");
        SetStatusText("--");
    }

    private void SetValueText(string value)
    {
        if (glucoseValueText != null)
            glucoseValueText.text = value;

        if (glucoseValueMirrorText != null)
            glucoseValueMirrorText.text = value;
    }

    private void SetStatusText(string status)
    {
        if (glucoseStatusText != null)
            glucoseStatusText.text = status;

        if (glucoseStatusMirrorText != null)
            glucoseStatusMirrorText.text = status;
    }
}