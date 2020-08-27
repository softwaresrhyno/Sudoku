using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour
{
    public GameObject boxOriginal;
    public GameObject[,] box = new GameObject[9,9];

    void Start()
    {
        for (int i = 0; i < box.GetLength(0); i++) {
            for (int j = 0; j < box.GetLength(1); j++) {
                GameObject boxGrid = Instantiate(boxOriginal, new Vector3(0, 0, 0), Quaternion.identity);
                boxGrid.transform.SetParent(GameObject.FindGameObjectWithTag("Grid").transform, false);

                box[i,j] = boxGrid;

                // GameObject text = box[i,j].transform.GetChild(0).gameObject;
                // text.GetComponent<Text>().text = (Random.Range(1, 10)).ToString();
            }
        }

        for (int i = 0; i < box.GetLength(0); i++) {
            ArrayList vetAux = new ArrayList(9) {1,2,3,4,5,6,7,8,9};

            for (int j = 0; j < box.GetLength(1); j++) {
                bool possuiNumeroLinha = true;
                bool possuiNumeroColuna = true;


                
                // while (verifica)
                // {
                    int numIndice = (Random.Range(0, vetAux.Count));
                    string numero = vetAux[numIndice].ToString();

                    possuiNumeroLinha = verificaLinha(i,numero);
                    possuiNumeroColuna = verificaColuna(j,numero);  

                    Debug.Log("Linha: " + possuiNumeroLinha + " - Coluna: " + possuiNumeroColuna + "- Número: " + numero);

                    if(possuiNumeroLinha == false && possuiNumeroColuna == false) {
                        GameObject text = box[i,j].transform.GetChild(0).gameObject;
                        text.GetComponent<Text>().text = numero;
                        vetAux.RemoveAt(numIndice);
                    } else {
                        GameObject text = box[i,j].transform.GetChild(0).gameObject;
                        text.GetComponent<Text>().text = "99";


                        /////////// TESTE BRUNO /////////

                        // string numeroRepetido;

                        // if(!possuiNumeroLinha) {
                        //     numeroRepetido = getNumRow(i,numero);
                        // } else if(!possuiNumeroColuna) {
                        //     numeroRepetido = getNumCol(j,numero);
                        // }

                        // // vetAux = vetAux.Where(val => val != numeroRepetido);
                        
                        // int indice = IntParseFast(numeroRepetido) - 1;
                        // vetAux.RemoveAt(indice);

                        // numIndice = (Random.Range(0, vetAux.Count));
                        // numero = vetAux[numIndice].ToString();

                        // text.GetComponent<Text>().text = numero;
                    }

                // }

            }
        }
    }

    bool verificaLinha(int row, string numero) {
        for(int i = 0; i < box.GetLength(0); i++) {
            GameObject boxChild = box[row,i].transform.GetChild(0).gameObject;
            string boxText = boxChild.GetComponent<Text>().text;
            
            if(boxText == numero) {
                return true;
            }
        }
        return false;
    }

    bool verificaColuna(int col, string numero) {
        for (int j = 0; j < box.GetLength(1); j++) {
            GameObject boxChild = box[j,col].transform.GetChild(0).gameObject;
            string boxText = boxChild.GetComponent<Text>().text;

            if(boxText == numero) {
                return true;
            }
        }
        return false;
    }

    // string getNumRow(int row, string numero) {
    //     for(int i = 0; i < box.GetLength(0); i++) {
    //         GameObject boxChild = box[row,i].transform.GetChild(0).gameObject;
    //         string boxText = boxChild.GetComponent<Text>().text;

    //         if(boxText == numero) {
    //             return boxText;
    //         }
    //     }
    // }

    // string getNumCol(int col, string numero) {
    //     for (int j = 0; j < box.GetLength(1); j++) {
    //         GameObject boxChild = box[j,col].transform.GetChild(0).gameObject;
    //         string boxText = boxChild.GetComponent<Text>().text;

    //         if(boxText == numero) {
    //             return boxText;
    //         }
    //     }
    // }

    // int IntParseFast(string value)
    //     {
    //     int result = 0;
    //     for (int i = 0; i < value.Length; i++)
    //     {
    //         char letter = value[i];
    //         result = 10 * result + (letter - 48);
    //     }
    //     return result;
    //  }

    // Update is called once per frame
    void Update()
    {
 
    }
}
