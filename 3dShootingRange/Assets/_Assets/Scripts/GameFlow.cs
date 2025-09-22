using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public void OnPlayerDied()
    {
        Time.timeScale = 0;      // dừng toàn bộ game
        print("Player died");    // in log ra console
    }
}
