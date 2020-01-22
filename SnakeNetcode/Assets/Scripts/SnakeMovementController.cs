using Shin.Framework;
using UnityEngine;


public class SnakeMovementController : GameBehaviour
{
    public void CreatePart(int remaining) {
        if (remaining <= 0 || null!=m_nextSnakePart)
            return;

        GameObject go = GameObject.Instantiate(m_snakePartPrefab, m_transform.position, m_transform.rotation);
        m_nextSnakePart = go.GetComponent<SnakeMovementController>();
        m_nextSnakePart.CreatePart(remaining-1);
    }

//----------------------------------------------------------------------------------------------------------------------

    public void Move(float interpolation) {
        Vector3 pos = Vector3.Lerp(m_prevUnitPos, m_nextUnitPos, interpolation);
        m_transform.position = pos;

        if (null == m_nextSnakePart) 
            return;

        m_nextSnakePart.Move(interpolation);
    }

//----------------------------------------------------------------------------------------------------------------------
    public void UpdateStep() {
        m_prevUnitPos = m_transform.position  = m_nextUnitPos;
        switch (m_nextDirection) {
            case SnakeDirection.UP :  m_nextUnitPos = m_prevUnitPos + Vector3.up; break;
            case SnakeDirection.DOWN: m_nextUnitPos = m_prevUnitPos+ Vector3.down; break;
            case SnakeDirection.LEFT: m_nextUnitPos = m_prevUnitPos+ Vector3.left; break;
            case SnakeDirection.RIGHT:m_nextUnitPos = m_prevUnitPos+ Vector3.right; break;
            default: break;
        }

        //Pass current direction to the next snake part
        if (null != m_nextSnakePart) {
            m_nextSnakePart.SetNextDirection(m_curDirection);
            m_nextSnakePart.UpdateStep();
        }

        m_curDirection = m_nextDirection;
    }

//----------------------------------------------------------------------------------------------------------------------
    public void SetNextDirection(SnakeDirection nextDir) { m_nextDirection = nextDir;  }
    public SnakeDirection GetNextDirection() { return m_nextDirection;  }

//----------------------------------------------------------------------------------------------------------------------
    void Start() {
        UpdateStep();
    }

//----------------------------------------------------------------------------------------------------------------------

    [SerializeField] private SnakeDirection m_nextDirection;
    [SerializeField] private GameObject m_snakePartPrefab = null;

    private SnakeMovementController m_nextSnakePart = null;

    private SnakeDirection m_curDirection = SnakeDirection.NONE;
    private Vector3 m_nextUnitPos;
    private Vector3 m_prevUnitPos;


}
