﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoxSelecionada : MonoBehaviour, ISelectHandler
{
    public Color corQuadrante;

    public ColorBlock corPadrao;

    private GridScript gridScript;
    private ColorBlock corBoxInvalida;

    public void OnSelect(BaseEventData eventData)
    {
        GameObject text = this.gameObject.transform.GetChild(0).gameObject;
        int indice = transform.GetSiblingIndex();
        int x = indice / 9;
        int y = indice % 9;
        //Debug.Log(indice + " - Índice | x = " + x + " | y = " + y);

        //GameObject grid = GameObject.FindWithTag("Grid");
        //GridScript gridScript = grid.GetComponent<GridScript>();

        gridScript.xSelecionado = x;
        gridScript.ySelecionado = y;

        resetarCoresGrid();

        // Colorir Linha e Coluna
        for (int i=0; i < gridScript.box.GetLength(0); i++)
        {
            Button botao = gridScript.box[i, y].GetComponent<Button>();
            ColorBlock cor = botao.colors;
            if (cor.normalColor != corBoxInvalida.normalColor)
            {
                cor.normalColor = corPadrao.pressedColor;
                botao.colors = cor;
            }

            botao = gridScript.box[x, i].GetComponent<Button>();
            cor = botao.colors;
            if (cor.normalColor != corBoxInvalida.normalColor)
            {
                cor.normalColor = corPadrao.pressedColor;
                botao.colors = cor;
            }
        }

        colorirQuadrante(x, y);
    }

    void Awake()
    {
        Button box = gameObject.GetComponent<Button>();
        corPadrao = box.colors;

        GameObject grid = GameObject.FindWithTag("Grid");
        gridScript = grid.GetComponent<GridScript>();

        corBoxInvalida = gridScript.boxInvalida;
    }

    void Update()
    {
        
    }

    public void resetarCoresGrid()
    {

        for (int i = 0; i < gridScript.box.GetLength(0); i++)
        {
            for (int j = 0; j < gridScript.box.GetLength(1); j++)
            {
                Button botao = gridScript.box[i, j].GetComponent<Button>();
                ColorBlock corBox = botao.colors;

                if (corBox.normalColor != corBoxInvalida.normalColor)
                {
                    botao.colors = corPadrao;
                }
            }
        }
    }

    public void colorirQuadrante(int x, int y)
    {
        int iIni, iFim, jIni, jFim;

        if (x >= 0 && x <= 2)
        {
            iIni = 0;
            iFim = 2;
        }
        else if (x >= 3 && x <= 5)
        {
            iIni = 3;
            iFim = 5;
        }
        else
        {
            iIni = 6;
            iFim = 8;
        }

        if (y >= 0 && y <= 2)
        {
            jIni = 0;
            jFim = 2;
        }
        else if (y >= 3 && y <= 5)
        {
            jIni = 3;
            jFim = 5;
        }
        else
        {
            jIni = 6;
            jFim = 8;
        }

        for (int i = iIni; i <= iFim; i++)
        {
            for (int j = jIni; j <= jFim; j++)
            {
                Button botao = gridScript.box[i, j].GetComponent<Button>();
                ColorBlock cor = botao.colors;
                if (cor.normalColor != corBoxInvalida.normalColor)
                {
                    cor.normalColor = corQuadrante;
                    botao.colors = cor;
                }
            }
        }
    }
}
