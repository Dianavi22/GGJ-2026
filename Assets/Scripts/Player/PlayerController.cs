using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Masks ActiveMask = Masks.NEUTRAL;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _interpolationSpeed;

    public bool isNeutral = true;
    public bool isRed = false;
    public bool isBlue = false;


    private float _maxSpeed;
    private Rigidbody _rigidbody;
    private bool _lookingBehind;

    public float _timer = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _maxSpeed = _speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.MoveRotation(Quaternion.LookRotation(-transform.forward, Vector3.up));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine("PutYourMaskOn", Masks.BLUE);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("PutYourMaskOn", Masks.RED);
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
        Debug.Log("ChangeMask" + mask);
        ActiveMask = mask;
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

    private IEnumerator PutYourMaskOn(Masks choosedMask)
    {
        _timer = 0;
        _speed = _speed / 2;
        while (_timer <= 1)
        {
            _timer += Time.deltaTime;
            
            yield return null;
            if (_timer >= 1)
            {
                ChangeMask(choosedMask);
                StopCoroutine("RollBackToNeutral");
                StartCoroutine("RollBackToNeutral");
                _speed = _maxSpeed;
            }
        }
    }


    private IEnumerator RollBackToNeutral() {
        yield return new WaitForSeconds(3);
        ChangeMask(Masks.NEUTRAL);
    }

    public enum Masks
  {
    BLUE,
    RED,
    NEUTRAL
  }
}
