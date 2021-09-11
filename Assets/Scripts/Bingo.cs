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
                    cubes[y, x].m_view.text = ((int)m_cellNum[x * 5 + y]).ToString();
                }                
            }
        }

        cubes[2, 2].CellState = CellState.Open;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
