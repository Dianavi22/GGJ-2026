using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> PossibleExits;

    public void StartGame()
    {
        // Random Exit
        PossibleExits[Random.Range(0, PossibleExits.Count)].gameObject.SetActive(true);
    }

    public void EndGame()
    {
        // End the party as the player reached the End or he Died.
        // Play Game Over 
    }
}
