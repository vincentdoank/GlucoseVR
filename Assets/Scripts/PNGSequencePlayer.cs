using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PNGSequencePlayer : MonoBehaviour
{
    [Header("Target UI")]
    [SerializeField] private Image targetImage;

    [Header("Frames")]
    [SerializeField] private Sprite[] frames;

    [Header("Playback")]
    [SerializeField] private float fps = 24f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool playOnStart = true;

    private Coroutine playRoutine;
    private bool isPlaying;

    private void Start()
    {
        if (playOnStart)
            Play();
    }

    public void Play()
    {
        if (frames == null || frames.Length == 0)
            return;

        Stop();

        isPlaying = true;
        playRoutine = StartCoroutine(PlaySequence());
    }

    public void Stop()
    {
        if (playRoutine != null)
            StopCoroutine(playRoutine);

        isPlaying = false;
    }

    public void Pause()
    {
        isPlaying = false;
    }

    public void Resume()
    {
        if (!isPlaying)
            isPlaying = true;
    }

    private IEnumerator PlaySequence()
    {
        float delay = 1f / fps;
        int index = 0;

        while (true)
        {
            if (isPlaying)
            {
                targetImage.sprite = frames[index];
                index++;

                if (index >= frames.Length)
                {
                    if (loop)
                        index = 0;
                    else
                    {
                        Stop();
                        yield break;
                    }
                }
            }

            yield return new WaitForSeconds(delay);
        }
    }
}