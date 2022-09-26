using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    private float previousTime;
    public float fallTime = 0.8f;
    public Vector3 rotationPoint;
    public static int height = 20;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];


    void Start()
    {
        
    }

    void Update()
    {
        if (Time.timeScale == 1f)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMove())
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                }
            }

            if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGrid();
                    FindObjectOfType<SpawnTetromino>().CheckForLines();
                    this.enabled = false;
                    FindObjectOfType<SpawnTetromino>().NewTetromino();
                }
                previousTime = Time.time;
            }
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedY > height - 1 )
            {
                FindObjectOfType<pause>().GameOver();
                break;
            }
            else
            {
                grid[roundedX, roundedY] = children;
            }
        }
        CheckEndGame();
    }

    public bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
            print($"ячейка {i} {j}");
        }
    }

    public void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public void ClearGrid()
    {
        for (int i = 0; i <= height-1; i++)
        {
            for (int j = 0; j <= width-1; j++)
            {
                if (grid[j, i] == null)
                {
                    grid[j, i] = null;
                    print($"ячейка пуста {i} {j}");
                }
                else
                {
                    Destroy(grid[j, i].gameObject);
                    grid[j, i] = null;
                    print($"ячейка  {i} {j} должна быть удалена");
                }
            }
        }
    }
    public void CheckEndGame()
    {
        for(int j = 0; j < width; j++)
        {
            
            if (grid[j, height - 1] != null)
            {
                FindObjectOfType<SpawnTetromino>().score = 0;
                FindObjectOfType<SpawnTetromino>().currencyText.text = "0";
                FindObjectOfType<pause>().GameOver();
            }
        }
    }
}
