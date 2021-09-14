using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minsweeper : MonoBehaviour
{
    [SerializeField]
    public int _rows = 1;

    [SerializeField]
    public int _columns = 1;

    [SerializeField]
    private int _minCount = 1;

    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup = null;

    [SerializeField]
    private Cell _cellPrefab = null;

    [SerializeField]
    public bool _visibleFrag = false;

    [SerializeField] GameObject _gameOverPanel = default;

    public Cell[,] _cells;

    private void OnValidate()
    {
        if (_columns < _rows)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _columns;
        }
        else
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            _gridLayoutGroup.constraintCount = _rows;
        }
    }
    void Start()
    {
        GameStart();
        var pearent = _gridLayoutGroup.transform;
        _cells = new Cell[_rows, _columns];
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                var cell = Instantiate(_cellPrefab);
                cell.transform.SetParent(pearent);
                cell._x = r;
                cell._y = c;
                _cells[r, c] = cell;
            }
        }

        MineSet();
        MineCheck();
    }

    public void MineSet()
    {
        foreach (var item in _cells)
        {
            item.CellState = CellState.None;
        }
        if (_cells.Length > _minCount)
        {
            for (int i = 0; i < _minCount; i++)
            {
                var r = Random.Range(0, _rows);
                var c = Random.Range(0, _columns);
                var cell = _cells[r, c];
                if (cell.CellState == CellState.Mine || cell._ClickFrag)
                {
                    i--;
                }
                else
                {
                    cell.CellState = CellState.Mine;
                }
            }
        }
        else
        {
            Debug.LogError("ƒZƒ‹‚Ì”‚æ‚èƒ}ƒCƒ“‚Ì”‚ª‘½‚¢");
        }
    }

    public void MineCheck()
    {
        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _columns; c++)
            {
                if (_cells[r, c].CellState != CellState.Mine)
                {
                    int minCount = 0;
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if ((r + x) < 0 || (r + x) > _rows - 1 || (c + y) < 0 || (c + y) > _columns - 1 || (x == 0 && y == 0))
                            {
                                continue;
                            }
                            if (_cells[r + x, c + y].CellState == CellState.Mine)
                            {
                                minCount++;
                            }
                        }
                    }
                    _cells[r, c].CellState = (CellState)minCount;
                }
                else
                {
                    continue;
                }  
            }    
        }
    }
    public void Gameover()
    {
        _gameOverPanel.SetActive(true);
    }

    private void GameStart()
    {
        _gameOverPanel.SetActive(false);
    }

    public void ReStart()
    {
        foreach (var item in _cells)
        {
            item._view.enabled = false;
            var b = item.GetComponent<Button>();
            b.enabled = true;
        }
        MineSet();
        MineCheck();
        GameStart();
    }
}
