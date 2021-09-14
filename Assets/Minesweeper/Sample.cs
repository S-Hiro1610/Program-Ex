using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sample : MonoBehaviour
{
    [SerializeField]GameObject[,] goArray = new GameObject[5,5];
    int selectedIndexX = 0;
    int selectedIndexY = 0;
    void Start()
    {
        for (int i = 0 ; i < goArray.GetLength(0); i++)
        {
            for (int k = 0; k < goArray.GetLength(1); k++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                goArray[i,k] = cube;
                cube.transform.position = new Vector3(-4f + (i * 2),-3f + (k * 2), 0);
                var r = cube.GetComponent<Renderer>();
                r.material.color = (i == 0 ? Color.red : Color.white);
            }
        }

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndexX--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndexX++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndexY++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndexY--;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }

        UpdateItem();
        CapSelectedIndex();

    }

    private void CapSelectedIndex()
    {
        selectedIndexX =
                    selectedIndexX < 0 ? 0 :
                    selectedIndexX >= goArray.GetLength(0) ? goArray.GetLength(0) - 1 :
                    selectedIndexX;
        selectedIndexY =
                    selectedIndexY < 0 ? 0 :
                    selectedIndexY >= goArray.GetLength(1) ? goArray.GetLength(1) - 1 :
                    selectedIndexY;
    }

    private void UpdateItem()
    {
        for (int i = 0; i < goArray.GetLength(0); i++)
        {
            for (int k = 0; k < goArray.GetLength(1); k++)
            {
                var renderer = goArray[i,k].GetComponent<Renderer>();
                renderer.material.color = (i == selectedIndexX && k == selectedIndexY ? Color.red : Color.white);
            }
        }
    }
}
