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

    public ColorBlock boxInvalida;

    public string[] palavras;

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

        palavras = new string[9] { "Cachorro", "Cavalo", "Gato", "Raposa", "Leão", "Abacate", "Girafa", "Hipopótamo", "Funchal" };

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
                text.GetComponent<Text>().text = palavras[int.Parse(numero) - 1];
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
        for (int i = 0; i < palavras.Length; i++)
        {
            GameObject menuPalavras = Instantiate(palavra, new Vector3(0, 0, 0), Quaternion.identity);
            menuPalavras.transform.SetParent(GameObject.FindGameObjectWithTag("MenuPalavras").transform, false);

            palavrasVetor[i] = menuPalavras;
            GameObject text = palavrasVetor[i].transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = palavras[i];

        }
    }

    public void palavraInvalida(int x, int y)
    {
        Button botao = box[x, y].GetComponent<Button>();
        ColorBlock cor = botao.colors;
        cor.normalColor = boxInvalida.normalColor;
        cor.highlightedColor = boxInvalida.highlightedColor;
        cor.pressedColor = boxInvalida.pressedColor;
        cor.selectedColor = boxInvalida.selectedColor;
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

    public void apagaBox()
    {
        Text text = box[xSelecionado, ySelecionado].GetComponentInChildren<Text>();
        text.text = "";
        
        BoxSelecionada boxSelecionadaScript = GameObject.FindWithTag("Box").GetComponent<BoxSelecionada>();
        Color corQuadrante = boxSelecionadaScript.corQuadrante;

        Button botaoSelecionado = box[xSelecionado, ySelecionado].GetComponent<Button>();
        ColorBlock cor = botaoSelecionado.colors;
        cor.normalColor = corQuadrante;
        botaoSelecionado.colors = cor;
    }
}
