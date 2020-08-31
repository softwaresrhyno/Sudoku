using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InserirPalavraBox : MonoBehaviour, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        GameObject grid = GameObject.FindWithTag("Grid");
        GridScript gridScript = grid.GetComponent<GridScript>();

        int x = gridScript.xSelecionado;
        int y = gridScript.ySelecionado;

        Text palavra = gameObject.GetComponentInChildren<Text>();
        string textOriginal = gridScript.boxGameplay[x, y];
        Text text = gridScript.box[x, y].GetComponentInChildren<Text>();

        print(palavra.text + "==" + textOriginal);

        if (x != -1 && y != -1)
        {
            text.text = palavra.text;

            Button botao = gridScript.box[x, y].GetComponent<Button>();
            ColorBlock cor = botao.colors;
            cor.normalColor = Color.white;
            botao.colors = cor;
        }

        //if(palavra.text != textOriginal)
        //{
        //    gridScript.palavraInvalida(x, y);
            
        //}
    }
}
