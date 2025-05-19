using UnityEngine;

public class Scr_Player_Animation : MonoBehaviour
{
    [SerializeField] private GameObject particule;

    public void SpawnAnimation()
    {
        Instantiate(particule, transform.position, transform.rotation, GameObject.Find("ParticuleParent").transform);
    }

}
