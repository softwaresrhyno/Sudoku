using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sudoku : MonoBehaviour
{
    public int[,] texto = new int[9, 9];

    string[] animais = new string[9] { "Cachorro", "Cavalo", "Gato", "Raposa", "Leão", "Abacate", "Girafa", "Hipopotamo", "Funchal" };

    public float tempo = 10 * 60;

    // Start is called before the first frame update
    void Start()
    {
        for (int contX = 0; contX < 9; contX++)
            for (int contY = 0; contY < 9; contY++)
            {
                texto[contX, contY] = (Random.Range(0, 9 * 10) % 9);
            }
    }

    // Update is called once per frame
    void Update()
    {
        tempo -= Time.deltaTime;
    }

    bool Selecionado = false;
    int SelectX;
    int SelectY;
    private void OnGUI()
    {
        if (tempo > 0)
        {
            float sizeBox = Screen.height / 9;
            Vector2 Middle = new Vector2(Screen.width / 2 - sizeBox * 9 / 2, Screen.height / 2 - sizeBox * 9 / 2);
            for (int contX = 0; contX < 9; contX++)
                for (int contY = 0; contY < 9; contY++)
                {
                    if (Selecionado && SelectX == contX && SelectY == contY)
                        GUI.color = Color.yellow;
                    if (GUI.Button(new Rect(Middle.x + sizeBox * contX, Middle.y + sizeBox * contY, sizeBox, sizeBox), animais[texto[contX, contY]].ToString()) && !Selecionado)
                    {
                        Selecionado = true;
                        SelectX = contX;
                        SelectY = contY;
                    }
                    GUI.color = Color.white;
                }

            if (Selecionado)
            {
                for (int cont = 0; cont < 9; cont++)
                {
                    if (GUI.Button(new Rect(0, sizeBox * cont, sizeBox, sizeBox), animais[cont]))
                    {
                        Selecionado = false;
                        texto[SelectX, SelectY] = cont;
                        Verificacao();
                    }
                }
            }

            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            int TempoInt = tempo < 0 ? 0 : (int)tempo;
            int segundos = (TempoInt % 60);
            GUI.Label(new Rect(0, 0, 100, 100), (TempoInt / 60) + ":" + (segundos < 10 ? "0" : "") + segundos);
        }
        else
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Cabo seu tempo bobo");
        }

        //GUI.Box(new Rect(Input.mousePosition.x - 25, (Screen.height - Input.mousePosition.y) - 25, 50, 50), "");

    }


    void Verificacao()
    {

    }

}