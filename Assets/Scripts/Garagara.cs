using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garagara : MonoBehaviour
{
    [SerializeField] private GaraCell m_cellPrefab = null;
    [SerializeField] private GridLayoutGroup m_gridLayoutGroup = null;
    [SerializeField] Bingo m_bingoPrefab;
    List<int> m_maxNum = new List<int>();
    public List<int> m_openNum = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 76; i++)
        {
            m_maxNum.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GaraGara()
    {
        var cell = Instantiate(m_cellPrefab);
        var parent = m_gridLayoutGroup.gameObject.transform;
        cell.transform.SetParent(parent);

        var r = Random.Range(1, m_maxNum.Count);
        cell.m_view.text = ((int)m_maxNum[r]).ToString();
        m_openNum.Add(m_maxNum[r]);
        m_maxNum.RemoveAt(r);
    }
}
