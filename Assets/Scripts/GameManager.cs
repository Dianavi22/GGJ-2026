using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> possibleExits;
    [SerializeField] List<GameObject> possiblePlayerStart;
    [SerializeField] Transform player;

    public void Start()
    {
        // Random Exit
        possibleExits[Random.Range(0, possibleExits.Count)].gameObject.SetActive(true);

        if(player == null)
        {
            player = GameObject.Find("Player").transform;
        }
        player.position = possiblePlayerStart[Random.Range(0, possiblePlayerStart.Count)].gameObject.transform.position;
    }

    public void GameOverDead()
    {
        // End the party as the player reached the End or he Died.
        // Play Game Over 

        //if enemy that killed us is Crazy Fun
        SceneManager.LoadScene("GameOverSceneRed");

        //if enemy that killed us is Sad AF
        SceneManager.LoadScene("GameOverSceneBlue");

    }
    public void GameOverVictory()
    {
        // End the party as the player reached the End or he Died.
        // Play Game Over 
        SceneManager.LoadScene("GameOverSceneVictory");
    }
}
