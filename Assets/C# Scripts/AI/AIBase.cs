using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBase<TState> : MonoBehaviour where TState : Enum
{
    protected TState currentState;
    protected Coroutine stateRoutine;
    private bool hasStarted = false;

    public virtual void ChangeState(TState nextState)
    {
        if (!hasStarted || !EqualityComparer<TState>.Default.Equals(currentState, nextState))
        {
            if (stateRoutine != null)
                StopCoroutine(stateRoutine);

            currentState = nextState;
            stateRoutine = StartCoroutine(RunStateWrapper(nextState));
            hasStarted = true;

            Debug.Log($"[AIBase] ChangeState: {nextState}");
        }
    }

    private IEnumerator RunStateWrapper(TState state)
    {
        Debug.Log($"[AIBase] RunStateWrapper: {state}");
        IEnumerator routine = RunState(state);
        if (routine != null)
            yield return StartCoroutine(routine);
    }

    protected abstract IEnumerator RunState(TState state);
}