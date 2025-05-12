using System.Collections;
using UnityEngine;

public class Dalle : MonoBehaviour
{
    float burning;
    [SerializeField] LayerMask burnableLayers;
    Collider[] allocColliders = new Collider[10];
    [SerializeField] GameObject flames;
    [SerializeField] GameObject fx_StartBurn;
    [SerializeField] bool propagateBurn = true;
    [SerializeField] public Cardinal directionMortSubite = Cardinal.East;
    public int column, row;
    [SerializeField] bool unbreakable = false;
    [SerializeField] public Transform slotBomb;
    [SerializeField] private GameObject blockUnbreakable;

    public int GetRow()
    {
        return row;
    }

    public int GetColumn() { return column; }

    public void SetCoordinates(int myrow, int mycolumn)
    {
        column = mycolumn;
        row = myrow;

    }

    private void Start()
    {
        if(unbreakable) blockUnbreakable.SetActive(true);
        else blockUnbreakable.SetActive(false);
    }

    public Cardinal GetDirectionMortSubite()
    { return directionMortSubite; }



    public bool BurnFor(float duration)
    {
        if (burning<=0) StartBurn();
        if (duration>burning) burning = duration;
        return propagateBurn;
    }


    void StartBurn()
    {
        burning = 0;
        if (flames) flames.SetActive(true);
        if (fx_StartBurn) Instantiate(fx_StartBurn,transform.position,transform.rotation);
        StartCoroutine(BurnRoutine());
    }

    public void StopBurn()
    {
        burning = 0;
        if (flames) flames.SetActive(false);
    }

    IEnumerator BurnRoutine()
    {
        while (burning>0)
        {
            Physics.OverlapBoxNonAlloc(transform.position,Vector3.one*0.45f,allocColliders,transform.rotation,burnableLayers);
            for (int i=0;i<allocColliders.Length;i++) allocColliders[i]?.GetComponent<iDamageable>().TakeDamage(1);
            yield return null;
        }
        StopBurn();
    }


    public bool suddenDeathHappened = false;

    public void MortSubite()
    {
        if (suddenDeathHappened) return;
        suddenDeathHappened = true;
        GetComponent<Scr_Block_Falling>().fallBlock();
    }
    
} // FIN DU SCRIPT
