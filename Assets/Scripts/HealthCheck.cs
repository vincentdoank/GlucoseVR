using TMPro;
using UnityEngine;

public class HealthCheck : MonoBehaviour
{
    [Header("Random Range")]
    [SerializeField] private Vector2Int heartRateRange = new Vector2Int(45, 130);
    [SerializeField] private Vector2Int glucoseRange = new Vector2Int(55, 180);

    [Header("UI Text")]
    [SerializeField] private TMP_Text bloodGlucoseText;
    [SerializeField] private TMP_Text heartRateText;
    [SerializeField] private TMP_Text glucoseTimeTestText;
    [SerializeField] private TMP_Text heartRateTimeTestText;
    [SerializeField] private TMP_Text glucoseStatusText;
    [SerializeField] private TMP_Text heartStatusText;

    public int CurrentHeartRate { get; private set; }
    public int CurrentGlucose { get; private set; }

    public string HeartRateStatus { get; private set; }
    public string GlucoseStatus { get; private set; }

    public void GenerateRandomHealthData()
    {
        bool isHeartLow = Random.value < 0.5f;
        bool isGlucoseLow = Random.value < 0.5f;

        // Generate around threshold ranges
        CurrentHeartRate = isHeartLow
            ? Random.Range(55, 60)   // 55–59
            : Random.Range(101, 111); // 101–110

        CurrentGlucose = isGlucoseLow
            ? Random.Range(65, 70)    // 65–69
            : Random.Range(140, 151); // 140–150

        HeartRateStatus = GetHeartRateStatus(CurrentHeartRate);
        GlucoseStatus = GetGlucoseStatus(CurrentGlucose);

        UpdateUI();

        Debug.Log($"Heart Rate: {CurrentHeartRate} BPM ({HeartRateStatus})");
        Debug.Log($"Glucose: {CurrentGlucose} mg/dL ({GlucoseStatus})");
    }

    private void UpdateUI()
    {
        if (heartRateText != null)
            heartRateText.text = $"{CurrentHeartRate} BPM ({HeartRateStatus})";

        if (bloodGlucoseText != null)
            bloodGlucoseText.text = $"{CurrentGlucose} mg/dL ({GlucoseStatus})";

        if (heartStatusText)
            heartStatusText.text = HeartRateStatus;
        if (glucoseStatusText)
            glucoseStatusText.text = GlucoseStatus;
    }

    public void SetHeartRateTime()
    {
        if (heartRateTimeTestText != null)
            heartRateTimeTestText.text = System.DateTime.Now.ToString("HH:mm:ss");
    }

    public void SetGlucoseTime()
    {
        if (glucoseTimeTestText != null)
            glucoseTimeTestText.text = System.DateTime.Now.ToString("HH:mm:ss");
    }

    private string GetHeartRateStatus(int bpm)
    {
        if (bpm < 60)
            return "Low";

        if (bpm <= 100)
            return "Normal";

        return "High";
    }

    private string GetGlucoseStatus(int glucose)
    {
        if (glucose < 70)
            return "Low";

        if (glucose < 140)
            return "Normal";

        return "High";
    }
}