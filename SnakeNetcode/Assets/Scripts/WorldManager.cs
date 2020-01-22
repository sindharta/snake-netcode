using Shin.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SingletonBehaviour<WorldManager> {

    public void AddSnake() {
        GameObject go = Instantiate(m_snakeHeadPrefab, Vector3.zero, Quaternion.identity);
        SnakeMovementController snakeMovementController = go.GetComponent<SnakeMovementController>();
        m_snakes.Add(snakeMovementController);
        snakeMovementController.CreatePart(m_initialSnakeSize-1);
        snakeMovementController.SetNextDirection(SnakeDirection.UP);

    }

//----------------------------------------------------------------------------------------------------------------------    
    void Start() {
        m_snakes = new HashSet<SnakeMovementController>();
        AddSnake();
    }


//----------------------------------------------------------------------------------------------------------------------    
    private HashSet<SnakeMovementController> m_snakes;

    [SerializeField] GameObject m_snakeHeadPrefab = null;
    [SerializeField] int m_initialSnakeSize = 3;
    

}
