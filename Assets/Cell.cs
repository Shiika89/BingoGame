﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1,
}

public class Cell : MonoBehaviour
{
    [SerializeField] Text m_view = null;

    [SerializeField] private CellState m_cellState = CellState.None;
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
        else if (m_cellState == CellState.Mine)
        {
            m_view.text = "X";
            m_view.color = Color.red;
        }
        else
        {
            m_view.text = ((int)m_cellState).ToString();
            m_view.color = Color.blue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
