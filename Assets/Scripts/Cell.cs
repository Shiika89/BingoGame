using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None,
    Number,
    Open,
    Close
}

public class Cell : MonoBehaviour
{
    [SerializeField] Text m_view = null;
    [SerializeField] private CellState m_cellState = CellState.None;
    GameObject m_bingoObject;
    Bingo m_bingo;

    private void Start()
    {
        m_bingoObject = GameObject.Find("Bingo");
        m_bingo = m_bingoObject.GetComponent<Bingo>();
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
            m_view.text = m_bingo.m_cellNumber.ToString();
            m_view.color = Color.blue;
        }
    }
}
