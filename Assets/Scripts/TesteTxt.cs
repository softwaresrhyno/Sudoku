using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteTxt : MonoBehaviour
{
    public TextAsset asset;
    //public  ArrayList palavras = new ArrayList();
    //public List<string[,]> palavras = new List<string[,]>();
    public string[,] palavras;

    void Awake()
    {
        var inputString = asset.ToString();
        var linhas = inputString.Split("\n"[0]);
        string[,] vetorTexto = new string[linhas.Length, linhas.Length];

        int maiorTamanho = 0;

        for (var i = 0; i < linhas.Length; i++)
        {
            var pt = linhas[i].Split(","[0]);

            if(pt.Length > maiorTamanho)
            {
                maiorTamanho = pt.Length;
            }
        }

        palavras = new string[linhas.Length, maiorTamanho];

        for (var i = 0; i < linhas.Length; i++)
        {
            var pt = linhas[i].Split(","[0]);

            for(int j = 0; j < pt.Length; j++)
            {
                palavras[i,j] = pt[j].Trim();
                //Debug.Log(palavras[i, j] + " = [" + i + "][" + j + "]");
            }

        }
    }
}
