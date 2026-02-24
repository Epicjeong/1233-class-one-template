using UnityEditor;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    private EnemyState _currentState;

    // Update is called once per frame
    private void Update()
    {
        _currentState?.Tick();
    }

    public void Initialize(EnemyState InitialState)
    {
        _currentState = InitialState;
        _currentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        if (newState == null) return;

        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
