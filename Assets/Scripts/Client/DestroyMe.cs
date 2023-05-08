using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    float Timer;
    public float Deathtimer = 10;

    void Update()
    {
        Timer += Time.deltaTime;

        if (Timer >= Deathtimer)
        {
            Destroy(gameObject);
        }
    }
}
