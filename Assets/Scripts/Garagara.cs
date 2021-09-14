using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Garagara : MonoBehaviour
{
    [SerializeField] private GaraCell m_cellPrefab = null;
    [SerializeField] private GridLayoutGroup m_gridLayoutGroup = null;
    [SerializeField] Bingo m_bingoPrefab;

    //ガラガラで出る全部の番号を入れるためのリスト
    List<int> m_maxNum = new List<int>();

    //実際にガラガラで出た番号を入れるためのリスト
    public List<int> m_openNum = new List<int>();
    //int m_bingoCount;
    //int m_closeCount;

    // Start is called before the first frame update
    void Start()
    {
        //１～７５の数字を追加
        for (int i = 1; i < 76; i++)
        {
            m_maxNum.Add(i);
        }
    }

    /// <summary>
    /// ボタンを押すと呼ばれる関数
    /// </summary>
    public void GaraGara()
    {
        var cell = Instantiate(m_cellPrefab);　//セルを生成する
        var parent = m_gridLayoutGroup.gameObject.transform; //セルを並べるために親を設定
        cell.transform.SetParent(parent);

        //出た番号をランダムにして、同じ番号が出ないようにする
        var r = Random.Range(1, m_maxNum.Count);
        cell.m_view.text = ((int)m_maxNum[r]).ToString();
        m_openNum.Add(m_maxNum[r]);
        m_maxNum.RemoveAt(r);
    }
}
