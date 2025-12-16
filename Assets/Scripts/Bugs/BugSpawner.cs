using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BugSpawner : MonoBehaviour
{
    public GameObject Bug, Bug1, Bug2, Bug3;
    public int position, type;
    public float spawntime1, spawntime2;
    public static int Counter;
    void Start()
    {
        spawntime1 = 0.7f;
        spawntime2 = 0.9f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Counter == 0 && Main.Stage == false)
        {
            StartCoroutine(BugSpawners());
        }
    }
    private void Update()
    {
        if(spawntime1 > 0.2f)
        {
            spawntime1 -= Time.deltaTime * 0.01f;
        }
        if(spawntime2 > 0.35f)
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
                switch (type)
                {
                    case 3:
                        Instantiate(Bug, new Vector2(Random.Range(-8f, 8f), 6), Quaternion.Euler(0, 0, 0));
                        break;
                    case 2:
                        Instantiate(Bug1, new Vector2(Random.Range(-8f, 8f), 6), Quaternion.Euler(0, 0, 0));
                        break;
                    case 1:
                        Instantiate(Bug2, new Vector2(Random.Range(-8f, 8f), 6), Quaternion.Euler(0, 0, 0));
                        break;
                    case 0:
                        Instantiate(Bug3, new Vector2(Random.Range(-8f, 8f), 6), Quaternion.Euler(0, 0, 0));
                        break;
                }
                
                break;
            case 2:
                switch (type)
                {
                    case 3:
                        Instantiate(Bug, new Vector2(Random.Range(-8f, 8f), -6), Quaternion.Euler(0, 0, 180));
                        break;
                    case 2:
                        Instantiate(Bug1, new Vector2(Random.Range(-8f, 8f), -6), Quaternion.Euler(0, 0, 180));
                        break;
                    case 1:
                        Instantiate(Bug2, new Vector2(Random.Range(-8f, 8f), -6), Quaternion.Euler(0, 0, 180));
                        break;
                    case 0:
                        Instantiate(Bug3, new Vector2(Random.Range(-8f, 8f), -6), Quaternion.Euler(0, 0, 180));
                        break;
                }
                
                break;
            case 1:
                
                switch (type)
                {
                    case 3:
                        Instantiate(Bug, new Vector2(-10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, 90));
                        break;
                    case 2:
                        Instantiate(Bug1, new Vector2(-10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, 90));
                        break;
                    case 1:
                        Instantiate(Bug2, new Vector2(-10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, 90));
                        break;
                    case 0:
                        Instantiate(Bug3, new Vector2(-10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, 90));
                        break;
                }
                break;
            case 0:
                switch (type)
                {
                    case 3:
                        Instantiate(Bug, new Vector2(10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, -90));
                        break;
                    case 2:
                        Instantiate(Bug1, new Vector2(10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, -90));
                        break;
                    case 1:
                        Instantiate(Bug2, new Vector2(10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, -90));
                        break;
                    case 0:
                        Instantiate(Bug3, new Vector2(10, Random.Range(-4f, 4f)), Quaternion.Euler(0, 0, -90));
                        break;
                }
                
                break;
        }
        Counter = 0;
    }
}
