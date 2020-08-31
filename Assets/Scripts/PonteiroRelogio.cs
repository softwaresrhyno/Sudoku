using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonteiroRelogio : MonoBehaviour
{
    private float timer = 0.0f;
    private float anguloSegundo = 0.0f;
    private float segundo = 0.0f;

    void Start()
    {
        anguloSegundo = 360 / 60;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        segundo = (int) timer % 60;
        transform.rotation = Quaternion.Euler(0, 0, -(segundo * anguloSegundo));
    }
}
