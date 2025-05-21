
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Scr_Bomb_Frozen : MonoBehaviour
{
    [Header("Déplacement")]
    public float slideSpeed = 0.2f;
    public int maxSlideDistance = 5;
    public LayerMask mask;
    public Vector3 offset;

    [Header("Sol")]
    public LayerMask groundMask;
    public float groundCheckDistance = 0.3f;

    private bool canPush = false;
    private Coroutine pushCoroutine;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(WaitUntilGrounded());
    }

    private IEnumerator WaitUntilGrounded()
    {
        // Attendre que la bombe touche le sol
        while (!IsOnGround())
            yield return null;

        // Attendre qu'elle soit immobile
        while (rb != null && rb.velocity.magnitude > 0.05f)
            yield return null;

        //AlignToGrid();
        canPush = true;
    }

    private bool IsOnGround()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance, groundMask);
    }

    private void AlignToGrid()
    {
        float x = Mathf.Round(transform.position.x / 2f) * 2f;
        float z = Mathf.Round(transform.position.z / 2f) * 2f;
        transform.position = new Vector3(x, transform.position.y, z) + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canPush) return;

        PlayerMove player = other.GetComponentInParent<PlayerMove>();
        if (player != null)
        {
            Vector3 dir = (transform.position - other.transform.position).normalized;
            dir = SnapToCardinal(dir);
            TryPush(dir);
        }
    }

    private Vector3 SnapToCardinal(Vector3 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            return new Vector3(Mathf.Sign(dir.x), 0, 0);
        else
            return new Vector3(0, 0, Mathf.Sign(dir.z));
    }

    private void TryPush(Vector3 direction)
    {
        if (pushCoroutine != null)
            StopCoroutine(pushCoroutine);

        pushCoroutine = StartCoroutine(SlideGrid(direction));
    }

    private IEnumerator SlideGrid(Vector3 direction)
    {
        canPush = false;

        for (int i = 1; i <= maxSlideDistance; i++)
        {
            Vector3 nextPos = transform.position + direction * 2f;

            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, 2f, mask))
                break;

            Tween t = transform.DOMove(nextPos, slideSpeed).SetEase(Ease.Linear);
            yield return t.WaitForCompletion();
        }

        AlignToGrid();
        yield return new WaitForSeconds(1f);
        canPush = true;
    }

    private void OnDestroy()
    {
        if (pushCoroutine != null)
            StopCoroutine(pushCoroutine);
    }
}
