using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState {
        None,
        Build,
        Play,
        Paused
    }

    private GameState gameState;
    
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Build;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
