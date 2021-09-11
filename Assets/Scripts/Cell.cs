using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None,
    Number,
    Open
}

public class Cell : MonoBehaviour
{
    [SerializeField] public Text m_view = null;
    [SerializeField] private CellState m_cellState = CellState.None;
    int m_num;
    [SerializeField] public Vector2Int m_positionCell;

    private void Awake()
    {
        var k = Random.Range(1, 76);
        m_num = k;
        Debug.Log(k);
    }
    public CellState CellState
    {
        get => m_cellState;
        set
        {
            m_cellState = value;
            OnCellStateChanged();
        }
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }

    private void OnCellStateChanged()
    {
        if (m_cellState == CellState.None)
        {
            m_view.text = "";
        }
        else if (m_cellState == CellState.Open)
        {
            m_view.text = "〇";
            m_view.color = Color.red;
        }
        else
        {
            //m_view.text = ((int)m_num).ToString();
            m_view.color = Color.blue;
        }
    }
}
