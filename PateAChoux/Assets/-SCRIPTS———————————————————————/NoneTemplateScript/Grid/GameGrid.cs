using UnityEngine;

public class GameGrid : MonoBehaviour
{
    [SerializeField] Transform grid;
    GridRow[] gridRows;
    [SerializeField] Vector4[] checkpoint;

    void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        gridRows = new GridRow[grid.childCount];
        for (int i=0;i<grid.childCount;i++)
        {
            gridRows[i] = new GridRow();
            gridRows[i].gridColumns = new IFlamable[grid.GetChild(i).transform.childCount];
            for (int j=0; j<gridRows.Length; j++)
            {
                gridRows[i].gridColumns[j] = grid.GetChild(i).transform.GetChild(j).GetComponent<IFlamable>();
            }
        }
        SpawnCheckPoint();
    }

    void SpawnCheckPoint()
    {
        for (int i = 0; i < checkpoint.Length; i++)
        {
            var currentCase = grid.GetChild(Mathf.RoundToInt(checkpoint[i].x)).transform.GetChild(Mathf.RoundToInt(checkpoint[i].y)).GetComponentInChildren<Scr_CheckPoint>();

            currentCase.ActiveCol();

            currentCase.SetDir(Mathf.RoundToInt(checkpoint[i].z), Mathf.RoundToInt(checkpoint[i].w));
            currentCase.SetNextCheckPoint(grid.GetChild(Mathf.RoundToInt(checkpoint[i].x)).transform.GetChild(Mathf.RoundToInt(checkpoint[i].y)).gameObject);
        }
    }


    public void BurnCell(int row, int col, float duration)
    {
        BurnCell(row,col,duration,0,Cardinal.North);
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
}
