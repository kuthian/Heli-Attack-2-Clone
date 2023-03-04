using UnityEngine;

public class CrateController : MonoBehaviour {

  private __CrateAction _action;

  private Transform _groundCheck;
  private LayerMask _groundLayer;
  internal Rigidbody2D _rb;
  public bool Grounded { get; set; }

  public Rigidbody2D Rigidbody => _rb;

  public void Start()
  {
    _action = GetComponent<__CrateAction>();
    _rb = GetComponent<Rigidbody2D>();
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