using UnityEngine;

public class CrateController : MonoBehaviour {

  private __CrateAction _action;

  private Transform _groundCheck;
  private LayerMask _groundLayer;
  public bool Grounded { get; set; }
  public Rigidbody2D Rigidbody { get; set; }

  public void Awake()
  {
    _action = GetComponent<__CrateAction>();
    Rigidbody = GetComponent<Rigidbody2D>();
    _groundCheck = transform.Find("GroundCheck");
    _groundLayer = LayerMask.GetMask("Ground");
  }

  private void Update()
  {
    if (Grounded) return;
    Grounded = Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Player"))
    {
      _action.Do(other.gameObject);
      Destroy(gameObject);
    } 
  }
}