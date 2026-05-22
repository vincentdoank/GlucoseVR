using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BloodPressureMonitor : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text pressureText;
    [SerializeField] private TMP_Text systolicText;
    [SerializeField] private TMP_Text diastolicText;
    [SerializeField] private TMP_Text heartRateText;
    [SerializeField] private GameObject heartBeatIcon;

    [Header("Measurement Settings")]
    [SerializeField] private int maxPressure = 200;
    [SerializeField] private float countSpeed = 0.02f;
    [SerializeField] private float heartbeatBlinkSpeed = 0.5f;

    private Coroutine measureRoutine;
    private Coroutine heartbeatRoutine;

    public int systolic;
    public int diastolic;
    public int heartRate;

    public bool playOnStart = false;

    public UnityEvent onCompleted;

    private void OnEnable()
    {
        if (playOnStart) StartMeasurement();
    }

    public void StartMeasurement()
    {
        if (measureRoutine != null)
            StopCoroutine(measureRoutine);

        if (heartbeatRoutine != null)
            StopCoroutine(heartbeatRoutine);

        GenerateNormalValues();
        measureRoutine = StartCoroutine(MeasureRoutine());
    }

    private void GenerateNormalValues()
    {
        systolic = 121;
        diastolic = 80;
        heartRate = 85;
    }

    private IEnumerator MeasureRoutine()
    {
        ClearResultUI();
        heartBeatIcon.SetActive(false);

        // Count up to max pressure
        for (int i = 0; i <= maxPressure; i++)
        {
            pressureText.text = i.ToString();
            yield return new WaitForSeconds(countSpeed);
        }

        // Start heartbeat only during deflation
        heartbeatRoutine = StartCoroutine(BlinkHeartBeat());

        // Count down only until diastolic
        for (int i = maxPressure; i >= diastolic; i--)
        {
            pressureText.text = i.ToString();
            yield return new WaitForSeconds(countSpeed);
        }

        if (heartbeatRoutine != null)
            StopCoroutine(heartbeatRoutine);

        heartBeatIcon.SetActive(true);

        ShowFinalResult();
    }

    private IEnumerator BlinkHeartBeat()
    {
        while (true)
        {
            heartBeatIcon.SetActive(!heartBeatIcon.activeSelf);
            yield return new WaitForSeconds(heartbeatBlinkSpeed);
        }
    }

    private void ShowFinalResult()
    {
        systolicText.text = "121";
        diastolicText.text = "80";
        heartRateText.text = heartRate.ToString();

        pressureText.text = $"{121}/{80}";
        onCompleted?.Invoke();
    }

    private void ClearResultUI()
    {
        pressureText.text = "0";
        systolicText.text = "--";
        diastolicText.text = "--";
        heartRateText.text = "--";
    }
}