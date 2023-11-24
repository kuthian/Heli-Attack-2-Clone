using UnityEngine;

public class CrateController : MonoBehaviour
{
    private __CrateAction action;

    private Transform groundCheck;
    private LayerMask groundLayer;
    public bool Grounded { get; set; }
    public Rigidbody2D Rigidbody { get; set; }

    public void Awake()
    {
        action = GetComponent<__CrateAction>();
        Rigidbody = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        if (Grounded) return;
        Grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            action.Do(other.gameObject);
            Destroy(gameObject);
        }
    }
}