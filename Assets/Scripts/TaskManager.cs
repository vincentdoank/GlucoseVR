using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("Current Task")]
    [SerializeField] private string currentTaskId;

    [Header("UI")]
    [SerializeField] private GameObject taskCompletePanel;
    [SerializeField] private TMP_Text taskText;

    [Header("Events")]
    public UnityEvent onTaskCompleted;

    private bool isCompleted;

    private void Start()
    {
        if (taskCompletePanel != null)
            taskCompletePanel.SetActive(false);

        UpdateTaskText();
    }

    public void SetTask(string taskId)
    {
        currentTaskId = taskId;
        isCompleted = false;

        if (taskCompletePanel != null)
            taskCompletePanel.SetActive(false);

        UpdateTaskText();
    }

    public void CheckAction(string actionId)
    {
        if (isCompleted) return;

        Debug.Log($"Action Received: {actionId}");

        if (actionId == currentTaskId)
        {
            CompleteTask();
        }
    }

    private void CompleteTask()
    {
        isCompleted = true;

        Debug.Log("Task Completed!");

        if (taskCompletePanel != null)
            taskCompletePanel.SetActive(true);

        onTaskCompleted?.Invoke();
    }

    private void UpdateTaskText()
    {
        if (taskText != null)
            taskText.text = $"Task: {currentTaskId}";
    }
}