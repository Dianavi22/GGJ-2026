using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject.GetComponent<PlayerController>())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameManager.EndGame();
            }
        }
    }
}
