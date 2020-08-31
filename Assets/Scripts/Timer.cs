using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer;
    public Text texto;

    void Start()
    {
        timer = 0;
        texto = GetComponent<Text>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        string minutos = Mathf.Floor(timer / 60).ToString("00");
        string segundos = Mathf.Floor(timer % 60).ToString("00");
        texto.text = minutos + ":" + segundos;
    }
}
