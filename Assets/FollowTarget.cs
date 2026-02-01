using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
  [SerializeField] private Transform _target;
  [SerializeField] private ScreenShake _ss;

    private Vector3 _offset;

  private void Awake() {
    _offset = transform.position;
  }

  private void LateUpdate() {
    transform.position = new Vector3(_target.position.x + _offset.x , _target.position.y + _offset.y, _target.position.z + _offset.z); 
  }
}
