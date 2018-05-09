using UnityEngine;

public class StateIdle : IState {

    public StateIdle() {
        
    }
    public void Enter() {

    }

    public void Execute() {
        Debug.Log("Im idling and i know it");
    }

    public void Exit() {
        
    }
}