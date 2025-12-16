using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float zaman;
    void Start()
    {
        zaman = 0;
    }

    // Update is called once per frame
    void Update()
    {
     
        zaman += Time.deltaTime;
        
        if(zaman > 40)
        {
            SceneManager.LoadScene(1);
        }
    }
    public void Go()
    {
        SceneManager.LoadScene(1);
    }
}
