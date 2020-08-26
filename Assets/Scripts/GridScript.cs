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

                GameObject text = box[i,j].transform.GetChild(0).gameObject;
                text.GetComponent<Text>().text = (Random.Range(1, 10)).ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
