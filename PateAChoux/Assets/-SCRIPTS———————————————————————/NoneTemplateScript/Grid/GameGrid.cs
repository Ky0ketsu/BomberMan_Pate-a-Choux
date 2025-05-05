using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] Transform grid;
    GridRow[] gridRows;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        gridRows = new GridRow[grid.childCount];
        for (int i=0;i<grid.childCount;i++)
        {
            gridRows[i] = new GridRow(grid.GetChild(i).transform.childCount);
            for (int j=0; i<gridRows.Length; j++)
            {
                gridRows[i].gridColumns[j] = grid.GetChild(i).transform.GetChild(j).GetComponent<IFlamable>();
            }
        }
    }

    public void BurnCell(int row, int column, float duration, int propagation, Cardinal direction)
    {
        IFlamable targetCell = gridRows[row].gridColumns[column];
        if (targetCell == null) return;
        if (gridRows[row].gridColumns[column].BurnFor(duration) == true) // Si la case autorise la propagation
        {
            propagation--;
            if (propagation>=0)
            {
                switch (direction)
                {
                    case Cardinal.North: row--; break;
                    case Cardinal.South: row++; break;
                    case Cardinal.East: column++; break;
                    case Cardinal.West: column--; break;
                }
                BurnCell(row,column,duration,propagation,direction);
            }
        }
    }

} // FIN DU SCRIPT


[System.Serializable]
public class GridRow
{
    public IFlamable[] gridColumns;

    public GridRow(int quantity)
    {
        this.gridColumns = new IFlamable[quantity];
    }
}
