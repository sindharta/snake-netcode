using Shin.Framework;
using UnityEngine;

[RequireComponent(typeof(SnakeMovementController))]
public class SnakeInputController : GameBehaviour {
    protected override void Awake() {
        base.Awake();
        m_movementController = GetComponent<SnakeMovementController>();
        m_movementController.SetNextDirection(m_startDirection);
    }

//----------------------------------------------------------------------------------------------------------------------
    void OnEnable() {
        m_numFixedUpdatePerUnit = (int)(m_timeToMoveOneUnit/ Time.fixedDeltaTime);
        
    }

//----------------------------------------------------------------------------------------------------------------------

    void FixedUpdate() {
        ++m_fixedUpdateCounter;
        if (m_fixedUpdateCounter >= m_numFixedUpdatePerUnit) {
            m_movementController.UpdateStep();
            m_fixedUpdateCounter = 0;
        } else {
            m_movementController.Move((float) m_fixedUpdateCounter / m_numFixedUpdatePerUnit);

        }
    }

//----------------------------------------------------------------------------------------------------------------------
    void Update() {
        SnakeDirection dir = m_movementController.GetNextDirection();
        if (Input.GetKey("up")) {
            dir = SnakeDirection.UP;
        } else if (Input.GetKey("down")) {
            dir = SnakeDirection.DOWN;
        } else if (Input.GetKey("left")) {
            dir= SnakeDirection.LEFT;
        } else if (Input.GetKey("right")) {
            dir= SnakeDirection.RIGHT;     
        }

        m_movementController.SetNextDirection(dir);
       
    }

//----------------------------------------------------------------------------------------------------------------------

    [SerializeField] private SnakeMovementController m_movementController;
    [SerializeField] readonly float m_timeToMoveOneUnit = 0.5f;
    [SerializeField] private SnakeDirection m_startDirection = SnakeDirection.NONE;

    private int m_numFixedUpdatePerUnit;
    private int m_fixedUpdateCounter;


}
