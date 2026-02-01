using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameManager gameManager;

    public bool _playerAtTheDoor = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _playerAtTheDoor)
        {
            gameManager.GameOverVictory();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.GetComponent<PlayerController>())
        {
            _playerAtTheDoor = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        _playerAtTheDoor = false;
    }



}
