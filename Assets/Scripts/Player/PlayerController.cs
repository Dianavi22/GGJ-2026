using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
  [SerializeField] private float _speed;
  [SerializeField] private float _rotationSpeed;
  [SerializeField] private float _interpolationSpeed;

  private Rigidbody _rigidbody;
  private bool _lookingBehind;

  private void Awake() {
    _rigidbody = GetComponent<Rigidbody>();
  }


  private void Update() {
    float space = Input.GetAxisRaw("Jump");

    if(space == 1 && !_lookingBehind) {
      transform.RotateAround(transform.position, Vector3.up, Mathf.Lerp(0, -180, Time.deltaTime)); 
      _lookingBehind = !_lookingBehind;
    } else if (space == 0 && _lookingBehind) {

      transform.RotateAround(transform.position, Vector3.up, 180); 
      _lookingBehind = !_lookingBehind;
    }
  }

  private void FixedUpdate() {
    Vector3 movementDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

    Vector3 moveVect = _speed * Time.fixedDeltaTime * movementDir;
    _rigidbody.MovePosition(_rigidbody.position + moveVect);

    if (movementDir != Vector3.zero && !_lookingBehind) Rotate(movementDir);
  }


  private void Rotate(Vector3 direction) {
    direction += transform.position;

    Quaternion targetRotation = Quaternion.LookRotation(direction - transform.position);
    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _interpolationSpeed);
  }
}
