using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BugSpawner1 : MonoBehaviour
{
    public GameObject Bug;
    public int position, type;
    public float spawntime1, spawntime2;
    public static int Counter;
    void Start()
    {
        spawntime1 = 0.8f;
        spawntime2 = 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Counter == 0 && Main.Stage == true)
        {
            StartCoroutine(BugSpawners());
        }
    }
    private void Update()
    {
        if(spawntime1 > 0.15f)
        {
            spawntime1 -= Time.deltaTime * 0.01f;
        }
        if(spawntime2 > 0.3f)
        {
            spawntime2 -= Time.deltaTime * 0.01f;
        }
    }

    IEnumerator BugSpawners()
    {
        position = Random.Range(0, 4);
        type = Random.Range(0, 4);
        Counter = 1;
        yield return new WaitForSeconds(Random.Range(spawntime1, spawntime2));
        switch (position)
        {
            case 3:
                Instantiate(Bug, new Vector2(Random.Range(-8f, 8f), 6), Quaternion.Euler(0, 0, 0));
                break;
            case 2:
                Instantiate(Bug, new Vector2(Random.Range(-8f, 8f), -6), Quaternion.Euler(0, 0, 180));
                break;
            case 1:
                Instantiate(Bug, new Vector2(-10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, 90));
                break;
            case 0:
                Instantiate(Bug, new Vector2(10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, -90));
                break;
        }
        Counter = 0;
    }
}
