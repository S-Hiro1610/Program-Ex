using System.Collections;
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
    [SerializeField]
    public Text _view = null;

    [SerializeField]
    private CellState _cellState = CellState.None;

    /// <summary>
    /// 最初の一回目を表すフラグ
    /// </summary>
    private static bool _firstFrag = true;

    /// <summary>
    /// 自分自身がクリックされたことを表すフラグ
    /// </summary>
    public bool _ClickFrag = false;

    public int _x, _y = 0;
    Minsweeper ms;
    public CellState CellState
    {
        get => _cellState;
        set 
        {
            _cellState = value;
            OnCellStateChanged();
        } 
    }

    private void OnValidate()
    {
        OnCellStateChanged();
    }
    private void Start()
    {
        ms = GetComponentInParent<Minsweeper>();
        if (!ms._visibleFrag)
        {
            _view.enabled = false;
        }
        
    }

    private void OnCellStateChanged()
    {
        if (_view == null){ return; }
        if (_cellState == CellState.None)
        {
            _view.text = "";
        }
        else if (_cellState == CellState.Mine)
        {
            _view.text = "X";
            _view.color = Color.red;
        }
        else
        {
            _view.text = ((int)_cellState).ToString();
            _view.color = Color.blue;
        }
    }
    public void Open()
    {
        _ClickFrag = true;
        if (_firstFrag && this._cellState == CellState.Mine)
        {
            ms.MineSet();
            ms.MineCheck();
        }
        var b = GetComponent<Button>();
        b.enabled = false;
        _view.enabled = true;
        _firstFrag = false;
        if (this.CellState == CellState.Mine)
        {
            ms.Gameover();
        }
        if (this.CellState == 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if ((_x + x) < 0 || (_x + x) > ms._rows - 1 || (_y + y) < 0 || (_y + y) > ms._columns - 1 || (x == 0 && y == 0))
                    {
                        continue;
                    }
                    if (ms._cells[_x + x, _y + y]._ClickFrag)
                    {
                        continue;
                    }
                    if (ms._cells[_x + x, _y + y].CellState != CellState.Mine)
                    {
                        Cell cell = ms._cells[_x + x, _y + y];
                        cell.Open();
                    }
                }
            }
        }   
    }
}
