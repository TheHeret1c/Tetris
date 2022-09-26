using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnTetromino : MonoBehaviour
{
    public GameObject[] Tetrominoes;
    public GameObject nextSpawn;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI TextRecord;
    public static int height = 20;
    public static int width = 10;
    public int score = 0;
    public int record = 0;
    public string check;

    void Start()
    {
        NewTetromino();
    }

    void Update()
    {
        
    }

    public void NewTetromino()
    {
        Quaternion rot = Quaternion.Euler(0, 0, transform.eulerAngles.z + (90 * Random.Range(0, 4)));
        if (nextSpawn == null)
        {
            Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, rot);
        }
        else
        {
            GameObject next = Instantiate(nextSpawn, new Vector3(5, 18, 0), rot);
            next.GetComponent<Move>().enabled = true;
            check = next.ToString();
            print($"Заспавнило enabled {check}");
            Destroy(nextSpawn.gameObject);
            nextSpawn = null;
        }
        nextSpawn = Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], new Vector3(11.5f, 15, 0), Quaternion.identity);
        nextSpawn.GetComponent<Move>().enabled = false;
    }

    public void CheckForLines()
    {
        int count = 0;
        for (int i = height - 1; i >= 0; i--)
        {
            if (FindObjectOfType<Move>().HasLine(i))
            {
                FindObjectOfType<Move>().DeleteLine(i);
                FindObjectOfType<Move>().RowDown(i);
                count++;
            }
        }
        if (count == 1)
        {
            score += 100;
        }
        else if (count == 2)
        {
            score += 300;
        }
        else if (count == 3)
        {
            score += 700;
        }
        else if (count == 4)
        {
            score += 1500;
        }
        print($"Счёт: {score}");
        currencyText.text = $"{score}";
        if (score > record)
        {
            record = score;
            TextRecord.text = $"{record}";
        }
    }
}
