using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayScene : MonoBehaviour
{

    public GameObject playerName;

    // Start is called before the first frame update
    void Start()
    {

        PlayerModel player = Database.LoadPlayer();
        
        if (player == null)
        {

            playerName.GetComponent<Text>().text = "NO SAVED GAME";
            

        }
        else
        {

            playerName.GetComponent<Text>().text = player.playerName;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}