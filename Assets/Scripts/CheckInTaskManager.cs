using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CheckTask
{
    public string taskId;
    public string taskName;
    public bool isCompleted;
}

public class CheckInTaskManager : MonoBehaviour
{
    [Header("Tasks")]
    [SerializeField] private List<CheckTask> tasks = new();

    [Header("UI")]
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text mirrorProgressText;
    [SerializeField] private Transform taskListContainer;
    [SerializeField] private Transform mirrorTaskListContainer;
    [SerializeField] private Toggle taskTogglePrefab;

    private Dictionary<string, Toggle> taskToggleMap = new();
    private Dictionary<string, Toggle> mirrorTaskToggleMap = new();
    private int completedCount;

    private void Start()
    {
        GenerateTaskUI();
        RefreshUI();
    }

    private void GenerateTaskUI()
    {
        foreach (Transform child in taskListContainer)
        {
            Destroy(child.gameObject);
        }

        taskToggleMap.Clear();

        foreach (var task in tasks)
        {
            Toggle toggle = Instantiate(taskTogglePrefab, taskListContainer);
            Toggle mToggle = Instantiate(taskTogglePrefab, mirrorTaskListContainer);

            toggle.isOn = mToggle.isOn = task.isCompleted;
            toggle.interactable = mToggle.interactable = false;

            TMP_Text label = toggle.GetComponentInChildren<TMP_Text>();
            TMP_Text mLabel = mToggle.GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.text = task.taskName;
            if (mLabel != null)
                mLabel.text = task.taskName;

            taskToggleMap.Add(task.taskId, toggle);
            mirrorTaskToggleMap.Add(task.taskId, mToggle);
        }
    }

    public void CompleteTask(string taskId)
    {
        CheckTask task = tasks.Find(t => t.taskId == taskId);

        if (task == null || task.isCompleted)
            return;

        task.isCompleted = true;
        completedCount++;

        RefreshUI();
    }

    private void RefreshUI()
    {
        if (progressText != null)
            progressText.text = $"SCORE: {completedCount}/{tasks.Count}";
        if(mirrorProgressText != null)
            mirrorProgressText.text = $"SCORE: {completedCount}/{tasks.Count}";

        foreach (var task in tasks)
        {
            if (taskToggleMap.TryGetValue(task.taskId, out Toggle toggle))
            {
                toggle.isOn = task.isCompleted;
            }

            if (mirrorTaskToggleMap.TryGetValue(task.taskId, out Toggle mToggle))
            {
                mToggle.isOn = task.isCompleted;
            }
        }
    }

    public void ResetTasks()
    {
        completedCount = 0;

        foreach (var task in tasks)
        {
            task.isCompleted = false;
        }

        RefreshUI();
    }
}