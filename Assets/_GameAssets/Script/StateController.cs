using UnityEngine;

public class StateController : MonoBehaviour
{
    private PlayerState _currentPlayerState = PlayerState.IDLE;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangePlayerState(PlayerState.IDLE);
    }

    public void ChangePlayerState(PlayerState playerState)
    {
        if(_currentPlayerState == playerState) { return; }

        _currentPlayerState = playerState;
    }
    
    public PlayerState GetPlayerState()
    {
        if (_currentPlayerState == null) {
            Debug.Log("here");
        }
        return _currentPlayerState;
    }
}
