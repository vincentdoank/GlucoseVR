using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ToGameScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
