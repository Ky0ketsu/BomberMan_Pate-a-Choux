using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class GameGrid : MonoBehaviour
{
    public static GameGrid access;
    [SerializeField] Transform grid;
    [HideInInspector]public GridRow[] gridRows;
    AudioSource MortSubiteAudioSource;


    void Start()
    {
        EVENTS.OnMortSubite += StartMortSubite;
        GenerateGrid();
        access = this;
        MortSubiteAudioSource= GetComponent<AudioSource>();
    }

    void OnDestroy()
    {
        EVENTS.OnMortSubite -= StartMortSubite;
    }


    void GenerateGrid()
    {
        gridRows = new GridRow[grid.childCount];
        for (int i=0;i<grid.childCount;i++)
        {
            gridRows[i] = new GridRow();
            gridRows[i].gridColumns = new Dalle[grid.GetChild(i).transform.childCount];
            for (int j=0; j < gridRows[i].gridColumns.Length; j++)
            {
                gridRows[i].gridColumns[j] = grid.GetChild(i).transform.GetChild(j).GetComponent<Dalle>();
                gridRows[i].gridColumns[j].SetCoordinates(i, j);
            }
        }
    }


    [SerializeField]float delayBetweenBlock;
    bool suddenDeathHappened = false;

    public void StartMortSubite()
    {
        if (suddenDeathHappened) return;
        suddenDeathHappened = true;
        StartCoroutine(MortSubiteRoutine());
    }

    IEnumerator MortSubiteRoutine()
    {
        int nextColumn = 0;
        int nextRow = 10;
        Dalle currentBlock = gridRows[nextRow].gridColumns[nextColumn];

        while (currentBlock.suddenDeathHappened==false)
        {
            currentBlock.MortSubite();
            //C'est ici qu'on jouera le son
            MortSubiteAudioSource.Play();


            float chrono = delayBetweenBlock;
            while (chrono > 0)
            {
                if (GAME.MANAGER.CurrentState == State.gameplay) chrono -= Time.deltaTime;
                yield return null;
            }

            switch (currentBlock.GetDirectionMortSubite())
            {
                case Cardinal.North: nextRow++; break;
                case Cardinal.South: nextRow--; break;
                case Cardinal.West: nextColumn--; break;
                case Cardinal.East: nextColumn++; break;
            }
            //Debug.Log(nextRow + " " + nextColumn);
            currentBlock = gridRows[nextRow].gridColumns[nextColumn];
        }
    }

    public void BurnCell(int row, int col, float duration)
    {
        BurnCell(row,col,duration,0,Cardinal.North);
    }

    public void BurnCell(int row, int column, float duration, int propagation, Cardinal direction)
    {
        Dalle targetCell = gridRows[row].gridColumns[column];
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
    public Dalle[] gridColumns;
}
