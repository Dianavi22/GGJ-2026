using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  [SerializeField] private float _speed;
  [SerializeField] private float _rotationSpeed;
  [SerializeField] private float _interpolationSpeed;

  private Rigidbody _rigidbody;

  private void Awake() {
    _rigidbody = GetComponent<Rigidbody>();
  }

  private void Update() {
    if(Input.GetKeyDown(KeyCode.Space)) {
      _rigidbody.MoveRotation(Quaternion.LookRotation(-transform.forward, Vector3.up));
    }
  }

  private void FixedUpdate() {
    float hInput = Input.GetAxis("Horizontal");
    float vInput = Input.GetAxis("Vertical");

    Vector3 moveVect = transform.forward * _speed * Time.fixedDeltaTime * vInput;
    _rigidbody.MovePosition(_rigidbody.position + moveVect);

    float rotAngle = hInput * _rotationSpeed * Time.fixedDeltaTime;
    Quaternion qRot = Quaternion.AngleAxis(rotAngle, transform.up);
    Quaternion qRotUpRight = Quaternion.FromToRotation(transform.up, Vector3.up);
    Quaternion qOrientationUpRightTarget = qRotUpRight * _rigidbody.rotation;
    Quaternion qNewUpRightOrientation = Quaternion.Slerp(_rigidbody.rotation, qOrientationUpRightTarget, Time.fixedDeltaTime * _interpolationSpeed);
    _rigidbody.MoveRotation(qRot * qNewUpRightOrientation);
    _rigidbody.velocity = Vector3.zero;
    _rigidbody.angularVelocity = Vector3.zero;
  }
}
