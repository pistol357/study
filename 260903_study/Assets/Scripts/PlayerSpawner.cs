using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Player;
    private List<GameObject> _players = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // 생성. Instantitate
            GameObject p = Instantiate(Player);
            _players.Add(p);
        }

        if (Input.GetKeyDown(KeyCode.D) && _players.Count != 0)
        {
            // 파괴. Destroy
            GameObject p = _players[_players.Count - 1];
            _players.RemoveAt(_players.Count - 1);
            Destroy(p);
        }
    }
}