using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GridScript : MonoBehaviour
{
    public GameObject boxOriginal;
    public GameObject[,] box = new GameObject[9, 9];
    public string[,] boxGameplay = new string[9, 9];
    public GameObject palavra;
    public GameObject[] palavrasVetor = new GameObject[9];

    public bool comecaComPalavras;

    public int qtdNumerosApagar;

    public int xSelecionado;
    public int ySelecionado;

    public string[] animais;

    void Start()
    {
        //validaGrid();

        for (int i = 0; i < box.GetLength(0); i++)
        {
            for (int j = 0; j < box.GetLength(1); j++)
            {
                GameObject boxGrid = Instantiate(boxOriginal, new Vector3(0, 0, 0), Quaternion.identity);
                boxGrid.transform.SetParent(GameObject.FindGameObjectWithTag("Grid").transform, false);

                box[i, j] = boxGrid;
            }
        }

        ajustaTamanhoCelula();

        gerarSudoku();
        embaralhar();

        animais = new string[9] { "Cachorro", "Cavalo", "Gato", "Raposa", "Leão", "Abacate", "Girafa", "Hipopótamo", "Funchal" };
        instanciaPalavras();
        
        if (qtdNumerosApagar >= box.Length)
        {
            qtdNumerosApagar = 0;
        }

        if (comecaComPalavras)
        {
            TrocaNumeroPorPalavra();
        }

        for (int i = 0; i < box.GetLength(0); i++)
        {
            for (int j = 0; j < box.GetLength(1); j++)
            {
                Text text = box[i, j].GetComponentInChildren<Text>();
                boxGameplay[i, j] = text.text;
            }
        }

        apagaNumeros();

        xSelecionado = -1;
        ySelecionado = -1;
    }


    void Update()
    {
        for (int i = 0; i < box.GetLength(0); i++)
        {
            for (int j = 0; j < box.GetLength(1); j++)
            {
                string textOriginal = boxGameplay[i, j];
                Text text = box[i, j].GetComponentInChildren<Text>();

                if ((text.text != textOriginal) && text.text != "")
                {
                    palavraInvalida(i, j);
                }
            }
        }
    }

    public void gerarSudoku()
    {
        int n = 3;
        int x = (Random.Range(0, 1000));
        for (int i = 0; i < n; i++, x++) {
            for (int j = 0; j < n; j++, x+=n) {
                for (int k = 0; k < n * n; k++, x++) {
                    GameObject text = box[n * i + j, k].transform.GetChild(0).gameObject;
                    int numero = (x % (n * n)) + 1;
                    text.GetComponent<Text>().text = numero.ToString();
                }
            }
        }
    }

    public void embaralhar()
    {
        ArrayList A = new ArrayList(3) { 0, 1, 2 };
        ArrayList B = new ArrayList(3) { 3, 4, 5 };
        ArrayList C = new ArrayList(3) { 6, 7, 8 };


        TrocaColunas(A, (int)A[0], (int)A[A.Count - 1]);
        TrocaColunas(B, (int)B[0], (int)B[B.Count - 1]);
        TrocaColunas(C, (int)C[0], (int)C[C.Count - 1]);

        TrocaLinhas(A, (int)A[0], (int)A[A.Count - 1]);
        TrocaLinhas(B, (int)B[0], (int)B[B.Count - 1]);
        TrocaLinhas(C, (int)C[0], (int)C[C.Count - 1]);

        TrocaColunasQuadrante();
        TrocaLinhasQuadrante();
    }

    public void TrocaColunas(ArrayList colunas, int ini, int fim)
    {
        for (int s = 0; s < 10; s++)
        {
            for (int t = 0; t < colunas.Count; t++)
            {
                int tmp = (int)colunas[t];
                int r = Random.Range(t, colunas.Count);
                colunas[t] = colunas[r];
                colunas[r] = tmp;
            }
        }

        for (int i = 0; i < box.GetLength(1); i++)
        {
            for (int j = ini, cont = 0; j < fim; j++, cont++)
            {
                GameObject text = box[i,j].transform.GetChild(0).gameObject;
                string aux = text.GetComponent<Text>().text;

                GameObject novo = box[i, (int)colunas[cont]].transform.GetChild(0).gameObject;
                string colunaNova = novo.GetComponent<Text>().text;

                text.GetComponent<Text>().text = colunaNova;
                novo.GetComponent<Text>().text = aux;
            }
        }
    }

    public void TrocaLinhas(ArrayList linhas, int ini, int fim)
    {
        for (int s = 0; s < 10; s++)
        {
            for (int t = 0; t < linhas.Count; t++)
            {
                int tmp = (int)linhas[t];
                int r = Random.Range(t, linhas.Count);
                linhas[t] = linhas[r];
                linhas[r] = tmp;
            }
        }

        for (int i = 0; i < box.GetLength(0); i++)
        {
            for (int j = ini, cont = 0; j < fim; j++, cont++)
            {
                GameObject text = box[j, i].transform.GetChild(0).gameObject;
                string aux = text.GetComponent<Text>().text;

                GameObject novo = box[(int)linhas[cont], i].transform.GetChild(0).gameObject;
                string linhaNova = novo.GetComponent<Text>().text;

                text.GetComponent<Text>().text = linhaNova;
                novo.GetComponent<Text>().text = aux;
            }
        }
    }

    public void TrocaColunasQuadrante()
    {
        for(int i = 0; i < box.GetLength(0); i++)
        {
            for(int j = 0; j < 3; j++)
            {
                GameObject text = box[i, j].transform.GetChild(0).gameObject;
                string aux = text.GetComponent<Text>().text;

                GameObject novo = box[i, j + 6].transform.GetChild(0).gameObject;
                string linhaNova = novo.GetComponent<Text>().text;

                text.GetComponent<Text>().text = linhaNova;
                novo.GetComponent<Text>().text = aux;
            }
        }
    }

    public void TrocaLinhasQuadrante()
    {
        for (int i = 0; i < box.GetLength(1); i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject text = box[j, i].transform.GetChild(0).gameObject;
                string aux = text.GetComponent<Text>().text;

                GameObject novo = box[j + 6, i].transform.GetChild(0).gameObject;
                string linhaNova = novo.GetComponent<Text>().text;

                text.GetComponent<Text>().text = linhaNova;
                novo.GetComponent<Text>().text = aux;
            }
        }
    }

    public void TrocaNumeroPorPalavra()
    {
        for (int i = 0; i < box.GetLength(0); i++)
        {
            for (int j = 0; j < box.GetLength(1); j++)
            {
                GameObject text = box[i, j].transform.GetChild(0).gameObject;
                string numero = text.GetComponent<Text>().text;
                text.GetComponent<Text>().text = animais[int.Parse(numero) - 1];
                text.GetComponent<Text>().fontSize = 26;
                //text.GetComponent<Text>().resizeTextForBestFit = true;
            }
        }
    }

    public void ajustaTamanhoCelula()
    {
        GridLayoutGroup gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
        var rectTransform = GetComponent<RectTransform>();
        float tamanhoGridX = rectTransform.sizeDelta.x;
        float tamanhoGridY = rectTransform.sizeDelta.y;

        Vector2 tamanhoBox = new Vector2(tamanhoGridX / box.GetLength(0), tamanhoGridY / box.GetLength(1));
        gridLayoutGroup.cellSize = tamanhoBox;
    }

    public void instanciaPalavras()
    {
        for (int i = 0; i < animais.Length; i++)
        {
            GameObject menuPalavras = Instantiate(palavra, new Vector3(0, 0, 0), Quaternion.identity);
            menuPalavras.transform.SetParent(GameObject.FindGameObjectWithTag("MenuPalavras").transform, false);

            palavrasVetor[i] = menuPalavras;
            GameObject text = palavrasVetor[i].transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = animais[i];

        }
    }

    public void palavraInvalida(int x, int y)
    {
        Button botao = box[x, y].GetComponent<Button>();
        ColorBlock cor = botao.colors;
        cor.normalColor = Color.red;
        botao.colors = cor;
    }

    public void apagaNumeros()
    {
        for(int i = 0; i < qtdNumerosApagar; i++)
        {
            int x = Random.Range(0, box.GetLength(0));
            int y = Random.Range(0, box.GetLength(1));

            GameObject text = box[x, y].transform.GetChild(0).gameObject;

            if(text.GetComponent<Text>().text == "")
            {
                i--;
            } else {
                text.GetComponent<Text>().text = "";
            }

        }
    }

    void validaGrid()
    {

        for (int i = 0; i < box.GetLength(0); i++)
        {
            for (int j = 0; j < box.GetLength(1); j++)
            {
                GameObject boxGrid = Instantiate(boxOriginal, new Vector3(0, 0, 0), Quaternion.identity);
                boxGrid.transform.SetParent(GameObject.FindGameObjectWithTag("Grid").transform, false);

                box[i, j] = boxGrid;

                // GameObject text = box[i,j].transform.GetChild(0).gameObject;
                // text.GetComponent<Text>().text = (Random.Range(1, 10)).ToString();
            }
        }

        for (int i = 0; i < box.GetLength(0); i++)
        {
            ArrayList vetAux = new ArrayList(9) { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int j = 0; j < box.GetLength(1) && vetAux.Count > 0; j++)
            {
                bool possuiNumeroLinha = true;
                bool possuiNumeroColuna = true;
                bool possuiNumeroQuadrante = true;

                int numIndice = (Random.Range(0, vetAux.Count));
                string numero = vetAux[numIndice].ToString();

                possuiNumeroLinha = verificaLinha(i, numero);
                possuiNumeroColuna = verificaColuna(j, numero);
                possuiNumeroQuadrante = verificaQuadrante(i, j, numero);

                Debug.Log("Row: " + possuiNumeroLinha + " - Col: " + possuiNumeroColuna + " - Quad: " + possuiNumeroQuadrante + " - Nº: " + numero + " [" + (i + 1) + "]" + "[" + (j + 1) + "]");

                if (possuiNumeroLinha == false && possuiNumeroColuna == false && possuiNumeroQuadrante == false)
                {
                    GameObject text = box[i, j].transform.GetChild(0).gameObject;
                    text.GetComponent<Text>().text = numero;
                    vetAux.RemoveAt(numIndice);
                }
                else
                {
                    //ArrayList vetAuxRemove = new ArrayList(vetAux.Count);
                    //for (int l = 0; l < vetAux.Count; l++)
                    //{
                    //    vetAuxRemove.Add(vetAux[l]);
                    //}

                    //vetAuxRemove.RemoveAt(numIndice);

                    //do
                    //{
                    //    numIndice = (Random.Range(0, vetAuxRemove.Count));
                    //    numero = vetAuxRemove[numIndice].ToString();

                    //    possuiNumeroLinha = verificaLinha(i, numero);
                    //    possuiNumeroColuna = verificaColuna(j, numero);
                    //    possuiNumeroQuadrante = verificaQuadrante(i, j, numero);

                    //    if (possuiNumeroLinha || possuiNumeroColuna || possuiNumeroQuadrante)
                    //    {
                    //        vetAuxRemove.RemoveAt(numIndice);
                    //    }

                    //} while (possuiNumeroLinha || possuiNumeroColuna || possuiNumeroQuadrante);

                    //GameObject text = box[i, j].transform.GetChild(0).gameObject;
                    //text = box[i, j].transform.GetChild(0).gameObject;
                    //text.GetComponent<Text>().text = numero;

                    //vetAux.RemoveAt(numIndice);

                    //for (int k = 0; k < vetAuxRemove.Count; k++)
                    //{
                    //    numIndice = (Random.Range(0, vetAuxRemove.Count));
                    //    numero = vetAuxRemove[numIndice].ToString();

                    //    possuiNumeroLinha = verificaLinha(i, numero);
                    //    possuiNumeroColuna = verificaColuna(j, numero);
                    //    possuiNumeroQuadrante = verificaQuadrante(i, j, numero);

                    //    if (possuiNumeroLinha == false && possuiNumeroColuna == false && possuiNumeroQuadrante == false)
                    //    {
                    //        GameObject text = box[i, j].transform.GetChild(0).gameObject;
                    //        text.GetComponent<Text>().text = numero;
                    //        vetAux.RemoveAt(numIndice);
                    //    }
                    //    else
                    //    {
                    //        vetAuxRemove.RemoveAt(numIndice);
                    //    }
                    //}

                    // ----------------------------------------------------------------------------


                    GameObject text = box[i, j].transform.GetChild(0).gameObject;
                    text.GetComponent<Text>().text = "99";

                    for (int k = 0; k < vetAux.Count; k++)
                    {
                        if (vetAux[k].ToString() == numero)
                        {
                            ArrayList vetAuxRemove = new ArrayList(vetAux.Count);

                            for (int l = 0; l < vetAux.Count; l++)
                            {
                                vetAuxRemove.Add(vetAux[l]);
                            }

                            Debug.Log("Tamanho Vetor: " + vetAux.Count);

                            numIndice = (Random.Range(0, vetAuxRemove.Count));
                            numero = vetAuxRemove[numIndice].ToString();

                            possuiNumeroColuna = verificaColuna(j, numero);
                            while (possuiNumeroColuna)
                            {
                                numIndice = (Random.Range(0, vetAuxRemove.Count));
                                numero = vetAuxRemove[numIndice].ToString();
                                possuiNumeroColuna = verificaColuna(j, numero);
                                if (possuiNumeroColuna)
                                {
                                    vetAuxRemove.RemoveAt(numIndice);
                                }
                            }

                            possuiNumeroQuadrante = verificaQuadrante(i, j, numero);
                            while (possuiNumeroQuadrante)
                            {
                                numIndice = (Random.Range(0, vetAuxRemove.Count));
                                numero = vetAuxRemove[numIndice].ToString();
                                possuiNumeroQuadrante = verificaQuadrante(i, j, numero);
                                if (possuiNumeroQuadrante)
                                {
                                    vetAuxRemove.RemoveAt(numIndice);
                                }
                            }

                            text = box[i, j].transform.GetChild(0).gameObject;
                            text.GetComponent<Text>().text = numero;

                            for(int m = 0; m < vetAux.Count; m++)
                            {
                                if (vetAux[m].ToString() == numero)
                                {
                                    vetAux.RemoveAt(m);
                                }
                            }

                            k = vetAux.Count;
                        }
                    }
                }
            }
        }
    }

    bool verificaLinha(int row, string numero)
    {
        for (int i = 0; i < box.GetLength(0); i++)
        {
            GameObject boxChild = box[row, i].transform.GetChild(0).gameObject;
            string boxText = boxChild.GetComponent<Text>().text;

            if (boxText == numero)
            {
                return true;
            }
        }
        return false;
    }

    bool verificaColuna(int col, string numero)
    {
        for (int j = 0; j < box.GetLength(1); j++)
        {
            GameObject boxChild = box[j, col].transform.GetChild(0).gameObject;
            string boxText = boxChild.GetComponent<Text>().text;

            if (boxText == numero)
            {
                return true;
            }
        }
        return false;
    }

    bool verificaQuadrante(int row, int col, string numero)
    {
        int iIni, iFim, jIni, jFim;

        if (row >= 0 && row <= 2)
        {
            iIni = 0;
            iFim = 2;
        }
        else if (row >= 3 && row <= 5)
        {
            iIni = 3;
            iFim = 5;
        }
        else
        {
            iIni = 6;
            iFim = 8;
        }

        if (col >= 0 && col <= 2)
        {
            jIni = 0;
            jFim = 2;
        }
        else if (col >= 3 && col <= 5)
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
                GameObject boxChild = box[i, j].transform.GetChild(0).gameObject;
                string boxText = boxChild.GetComponent<Text>().text;

                if (boxText == numero)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
