using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bingo : MonoBehaviour
{
    [SerializeField] private Cell m_cellPrefab = null;
    [SerializeField] private GridLayoutGroup m_gridLayoutGroup = null;
    [SerializeField] int m_indexNumX = 5;
    [SerializeField] int m_indexNumY = 5;
    [SerializeField] Garagara m_ggPrefab;
    private Cell[,] cubes;
    List<int> m_maxNum = new List<int>();
    int[] m_cellNum;

    private void Awake()
    {
        m_cellNum = new int[m_indexNumX * m_indexNumY];

        for (int i = 1; i < 76; i++)
        {
            m_maxNum.Add(i);
        }

        for (int k = 0; k < m_cellNum.Length; k++)
        {
            var r = Random.Range(1, m_maxNum.Count);
            m_cellNum[k] = m_maxNum[r];
            m_maxNum.RemoveAt(r);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_indexNumX < m_indexNumY)
        {
            m_gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            m_gridLayoutGroup.constraintCount = m_indexNumX;
        }
        else
        {
            m_gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            m_gridLayoutGroup.constraintCount = m_indexNumY;
        }

        cubes = new Cell[m_indexNumX, m_indexNumY];

        for (int x = 0; x < m_indexNumX; x++)
        {
            for (int y = 0; y < m_indexNumY; y++)
            {
                var cell = Instantiate(m_cellPrefab);
                cell.m_positionCell = new Vector2Int(y, x);
                var parent = m_gridLayoutGroup.gameObject.transform;
                cell.transform.SetParent(parent);
                cubes[y, x] = cell;
                
                if (y != 2 || x != 2)
                {
                    cubes[y, x].CellState = CellState.Number;
                    cubes[y, x].m_mynum = m_cellNum[x * 5 + y];
                    cubes[y, x].m_view.text = ((int)m_cellNum[x * 5 + y]).ToString();
                }                
            }
        }

        cubes[2, 2].CellState = CellState.Open;
    }

    // Update is called once per frame
    void Update()
    {
        CellCheck();
        GetBingoCount(cubes);
    }

    public void CellCheck()
    {
        for (int x = 0; x < m_indexNumX; x++)
        {
            for (int y = 0; y < m_indexNumY; y++)
            {
                foreach (var item in m_ggPrefab.m_openNum)
                {
                    if (cubes[y, x].m_mynum == item)
                    {
                        cubes[y, x].CellState = CellState.Open;
                    }
                }
                
            }
        }
    }

    private static int GetBingoCount(Cell[,] cubes)
    {
        int bingo = 0;
        for (int i = 0; i < 5; i++)
        {
            if (cubes[i, 0].CellState == CellState.Open && cubes[i, 1].CellState == CellState.Open && cubes[i, 2].CellState == CellState.Open && cubes[i, 3].CellState == CellState.Open && cubes[i, 4].CellState == CellState.Open)
            {
                bingo++;
            }
            if (cubes[0, i].CellState == CellState.Open && cubes[1, i].CellState == CellState.Open && cubes[2, i].CellState == CellState.Open && cubes[3, i].CellState == CellState.Open && cubes[4, i].CellState == CellState.Open)
            {
                bingo++;
            }
        }
        if (cubes[0, 0].CellState == CellState.Open && cubes[1, 1].CellState == CellState.Open && cubes[2, 2].CellState == CellState.Open && cubes[3, 3].CellState == CellState.Open && cubes[4, 4].CellState == CellState.Open)
        {
            bingo++;
        }
        if (cubes[0, 4].CellState == CellState.Open && cubes[1, 3].CellState == CellState.Open && cubes[2, 2].CellState == CellState.Open && cubes[3, 1].CellState == CellState.Open && cubes[4, 0].CellState == CellState.Open)
        {
            bingo++;
        }
        Debug.Log(bingo);
        return bingo;
    }

    //周囲のセルを調べて存在すれば配列に入れる関数
    public Cell[] SearchCell(int r, int c)
    {
        var list = new List<Cell>();

        var left = c - 1;
        var right = c + 1;
        var top = r - 1;
        var bottom = r + 1;

        if (top >= 0)
        {
            if (left >= 0)
            {
                list.Add(cubes[top, left]);
            }
            list.Add(cubes[top, c]);
            if (right < m_indexNumX)
            {
                list.Add(cubes[top, right]);
            }
        }
        if (left >= 0)
        {
            list.Add(cubes[r, left]);
        }
        if (right < m_indexNumX)
        {
            list.Add(cubes[r, right]);
        }
        if (bottom < m_indexNumY)
        {
            if (left >= 0)
            {
                list.Add(cubes[bottom, left]);
            }
            list.Add(cubes[bottom, c]);
            if (right < m_indexNumX)
            {
                list.Add(cubes[bottom, right]);
            }
        }

        return list.ToArray();
    }
}
