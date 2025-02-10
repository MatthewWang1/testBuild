using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour
{
    
    private int game_state;
    /*
        0: Reserved
        1: Reserved
        2: Reserved
        3: Walking Around
        4: Interacting with Etching
        5: 
        6: 
    */

    void Start()
    {
        game_state = 3;    
    }

    public void Set_Game_State(int new_state)
    {
        game_state = new_state;
    }

    public int Get_Game_State()
    {
        return game_state;
    }

}
