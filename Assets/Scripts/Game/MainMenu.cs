using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Startbutton()
    {
        BugSpawner.Counter = 0;
        BugSpawner1.Counter = 0;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(2);
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
