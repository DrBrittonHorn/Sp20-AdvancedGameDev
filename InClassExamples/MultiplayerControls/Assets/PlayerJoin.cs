using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour
{
    public Material player1Mat;
    public Material player2Mat;
    public Transform player1StartLoc;
    public Transform player2StartLoc;
    private int playerNum = 1;

    void OnPlayerJoined(PlayerInput p_Input)
    {
        Debug.Log("hi: " + playerNum);
        GameObject newPlayer = p_Input.gameObject;
        switch(playerNum)
        {
            case 1:
                newPlayer.GetComponent<Renderer>().material = player1Mat;
                newPlayer.transform.position = player1StartLoc.position;
                newPlayer.GetComponentInChildren<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 1);
                break;
            case 2:
                newPlayer.GetComponent<Renderer>().material = player2Mat;
                newPlayer.transform.position = player2StartLoc.position;
                newPlayer.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0.0f, 0.5f, 1);
                break;
            default:
                Debug.Log("Invalid Player: " + playerNum);
                break;
        }
        playerNum++;
    }
}
