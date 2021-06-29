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

        var cubes = new Cell[m_indexNumX, m_indexNumY];
        for (int i = 0; i < m_indexNumX; i++)
        {
            for (int x = 0; x < m_indexNumY; x++)
            {
                var cell = Instantiate(m_cellPrefab);
                var parent = m_gridLayoutGroup.gameObject.transform;
                cell.transform.SetParent(parent);
                cubes[i, x] = cell;
            }
        }

        for (var i = 0; i < 24; i++)
        {
            var r = Random.Range(0, m_indexNumX);
            var c = Random.Range(0, m_indexNumY);

            var cell = cubes[r, c];
            if (cell.CellState != CellState.Number && cubes[2, 2])
            {
                cell.CellState = CellState.Number; //選ばれた場所にMineがなければ生成
            }
            else
            {
                i--; //Mineがあった場合はやり直し
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
