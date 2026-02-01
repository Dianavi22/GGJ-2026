using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  public Masks ActiveMask = Masks.NONE;
  [SerializeField] private float _speed;
  [SerializeField] private float _rotationSpeed;
  [SerializeField] private float _interpolationSpeed;

  private Rigidbody _rigidbody;

  private bool _lookingBehind;

  private void Awake()
  {
    _rigidbody = GetComponent<Rigidbody>();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      _rigidbody.MoveRotation(Quaternion.LookRotation(-transform.forward, Vector3.up));
    }

    if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
    {

      _rigidbody.MoveRotation(Quaternion.LookRotation(-transform.forward, Vector3.up));
    }
    if (Input.GetKeyDown(KeyCode.E))
    {
      switch (ActiveMask)
      {
        case Masks.BLUE:
          ChangeMask(Masks.RED);
          break;
        case Masks.RED:
          ChangeMask(Masks.NONE);
          break;
        case Masks.NONE:
          ChangeMask(Masks.BLUE);
          break;

      }
    }

    if (Input.GetKeyDown(KeyCode.Space))
    {
      _rigidbody.MoveRotation(Quaternion.LookRotation(-transform.forward, Vector3.up));
    }
  }

  private void FixedUpdate()
  {
    Vector3 movementDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    Vector3 moveVect = _speed * Time.fixedDeltaTime * movementDir;

    _rigidbody.MovePosition(_rigidbody.position + moveVect);
    if (movementDir != Vector3.zero) Rotate(movementDir);
  }

  private void ChangeMask(Masks mask)
  {
    ActiveMask = mask;
    Debug.Log(ActiveMask);
  }

  private void Rotate(Vector3 direction)
  {
    if (_lookingBehind)
    {
      direction = -direction;
    }
    ;

    direction += transform.position;
    Quaternion targetRotation = Quaternion.LookRotation(direction - transform.position);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _interpolationSpeed);
  }

  public enum Masks
  {
    BLUE,
    RED,
    NONE
  }
}
