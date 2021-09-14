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
    [SerializeField] GameObject m_bingoText;
    private Cell[,] cubes;
    List<int> m_maxNum = new List<int>(); //全部の番号を入れるリスト
    int[] m_cellNum; //実際に振り分ける番号を入れておくための配列
    //int m_count;
    //int m_bingoCount;
    //int m_closeCount;

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
                
                // 真ん中以外をNumberに変えて番号を振り分ける
                if (y != 2 || x != 2)
                {
                    cubes[y, x].CellState = CellState.Number;
                    cubes[y, x].m_mynum = m_cellNum[x * 5 + y];
                    cubes[y, x].m_view.text = ((int)m_cellNum[x * 5 + y]).ToString();
                }                
            }
        }
        //真ん中をOpenに変更
        cubes[2, 2].CellState = CellState.Open;
    }

    // Update is called once per frame
    void Update()
    {
        CellCheck();
        GetBingoCount(cubes);        
    }

    /// <summary>
    /// ガラガラを回して出た数字と同じ数字のセルをOpenに変更する関数
    /// </summary>
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

    /// <summary>
    /// ビンゴしているかどうかを判定する関数
    /// </summary>
    /// <param name="cubes"></param>
    /// <returns></returns>
    private int GetBingoCount(Cell[,] cubes)
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

        if (bingo == 1)
        {
            //ビンゴしたらテキストをアクティブに
            m_bingoText.SetActive(true);
        }

        return bingo;
    }

    //周囲のセルを調べて存在すれば配列に入れる関数
    //public Cell[] SearchCell(int r, int c)
    //{
    //    var list = new List<Cell>();

    //    var left = c - 1;
    //    var right = c + 1;
    //    var top = r - 1;
    //    var bottom = r + 1;

    //    if (top >= 0)
    //    {
    //        if (left >= 0)
    //        {
    //            list.Add(cubes[top, left]);
    //        }
    //        list.Add(cubes[top, c]);
    //        if (right < m_indexNumX)
    //        {
    //            list.Add(cubes[top, right]);
    //        }
    //    }
    //    if (left >= 0)
    //    {
    //        list.Add(cubes[r, left]);
    //    }
    //    if (right < m_indexNumX)
    //    {
    //        list.Add(cubes[r, right]);
    //    }
    //    if (bottom < m_indexNumY)
    //    {
    //        if (left >= 0)
    //        {
    //            list.Add(cubes[bottom, left]);
    //        }
    //        list.Add(cubes[bottom, c]);
    //        if (right < m_indexNumX)
    //        {
    //            list.Add(cubes[bottom, right]);
    //        }
    //    }

    //    return list.ToArray();
    //}

    /// <summary>
    /// 横の列のセルを調べる関数
    /// </summary>
    //public Cell[] SearchSideLine(int x, int y)
    //{
    //    var list = new List<Cell>();

    //    var right = x + 1;

    //    if (right < m_indexNumX && m_count < 5)
    //    {
    //        list.Add(cubes[right, y]);
    //        m_count++;
    //        SearchSideLine(m_count, y);
    //    }
    //    else
    //    {
    //        m_count = 0;
    //    }

    //    return list.ToArray();
    //}

    /// <summary>
    /// 縦の列のセルを調べる関数
    /// </summary>
    //private Cell[] SearchVerticalLine(int x, int y)
    //{
    //    var list = new List<Cell>();

    //    var bottom = x + 1;

    //    if (bottom < m_indexNumX)
    //    {
    //        list.Add(cubes[x, bottom]);
    //    }

    //    return list.ToArray();
    //}

    /// <summary>
    /// 左上から斜めのセルを調べる関数
    /// </summary>
    //private Cell[] SearchLeftTopNanameLine(int x, int y)
    //{
    //    var list = new List<Cell>();

    //    var right = y + 1;
    //    var bottom = x + 1;

    //    if (bottom < m_indexNumY)
    //    {            
    //        if (right < m_indexNumX)
    //        {
    //            list.Add(cubes[bottom, right]);
    //        }
    //    }

    //    return list.ToArray();
    //}

    /// <summary>
    /// 右上から斜めのセルを調べる関数
    /// </summary>
    //private Cell[] SearchRightTopNanameLine(int x, int y)
    //{
    //    var list = new List<Cell>();

    //    var left = y - 1;
    //    var bottom = x + 1;

    //    if (bottom < m_indexNumY)
    //    {
    //        if (left >= 0)
    //        {
    //            list.Add(cubes[bottom, left]);
    //        }
    //    }

    //    return list.ToArray();
    //}

    //public void BingoSearch()
    //{
    //    for (int i = 0; i < 5; i++)
    //    {
    //        int num = 0;

    //        foreach (var item in SearchSideLine(num, i))
    //        {
    //            if (item.CellState == CellState.Open)
    //            {
    //                m_bingoCount++;
    //            }
    //            else
    //            {
    //                m_closeCount++;
    //            }

    //            if (m_bingoCount == 5)
    //            {
    //                Debug.Log("BINGO!!");
    //            }

    //            if (m_closeCount < 2 && m_bingoCount + m_closeCount < 5)
    //            {
    //                SearchSideLine(num + 1, i);
    //            }
                
    //        }
    //        m_bingoCount = 0;
    //        m_closeCount = 0;
    //    }
    //}
}
