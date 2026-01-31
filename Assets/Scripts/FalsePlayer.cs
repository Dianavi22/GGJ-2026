using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FalsePlayer : MonoBehaviour
{

    [SerializeField] Transform _right;
    [SerializeField] Transform _left;
    private bool _currentTarget;

    private float _speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _currentTarget = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ReachedPoint())
        {
            _currentTarget = !_currentTarget;
        }
        float step = _speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, GetActiveTarget(), step);
    }

    private bool ReachedPoint()
    {
        return Vector3.Distance(transform.position, GetActiveTarget()) < 1;
    }

    private Vector3 GetActiveTarget()
    {
        return _currentTarget ? _right.position : _left.position;
    }
}
