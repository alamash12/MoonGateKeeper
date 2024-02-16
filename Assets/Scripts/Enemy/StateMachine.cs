using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private T ownerEntity;
    private State<T> currentState;

    public void Setup(T owner, State<T> entryState)
    {
        ownerEntity = owner;
        currentState = null;

        ChangeState(entryState);
    }

    /// <summary>
    /// Update���� �� ������ �ش� ���¿����� �ൿ�� ȣ���ϱ� ���� �Լ�.
    /// </summary>
    public void Execute()
    {
        if (currentState != null)
        {
            currentState.Execute(ownerEntity);
        }
    }

    /// <summary>
    /// State�� newState�� ��ü�ϴ� �Լ�.
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(State<T> newState)
    {
        if (newState == null)
        {
            return;
        }

        if (currentState != null)
        {
            currentState.Exit(ownerEntity);
        }

        currentState = newState;
        currentState.Enter(ownerEntity);
    }
}
