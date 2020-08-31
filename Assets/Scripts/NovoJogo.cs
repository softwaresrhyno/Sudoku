using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NovoJogo : MonoBehaviour
{
    public void novoJogo()
    {
        SceneManager.LoadScene("sudoku");
    }
}
