using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineBar1 : MonoBehaviour
{
    public float health, Animyavasligi;
    private float maxhealht, realscale;
    void Start()
    {
        maxhealht = 100f;
    }


    void Update()
    {
        health = Main.Stageperform;
        realscale = health / maxhealht;


        if (transform.localScale.x > realscale)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - (transform.localScale.y - realscale) / Animyavasligi, transform.localScale.z);
        }

        if (transform.localScale.x < realscale)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - (transform.localScale.y - realscale) / Animyavasligi, transform.localScale.z);
        }
    }
}