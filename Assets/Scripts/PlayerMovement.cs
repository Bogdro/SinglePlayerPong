using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    //Skrypt odpowiedzialny za poruszanie się gracza

    private Rigidbody playerRb;
    //Początkowa prędkość gracza
    public int playerSpeed = 12;
    //Zmienne określające czy gracz może się poruszać w górę/w dół;
    public bool canMoveTop = true, canMoveBot = true;

    void Start ()
    {
        playerRb = GetComponent<Rigidbody>();	
	}
	
	void Update ()
    {
          if (Input.GetAxis("Vertical") > 0)
        {
            if (canMoveTop) playerRb.velocity = new Vector3(0f, Input.GetAxis("Vertical") * playerSpeed, 0f);
        }
        else
        {
            if (canMoveBot) playerRb.velocity = new Vector3(0f, Input.GetAxis("Vertical") * playerSpeed, 0f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Jeśli gracz naciśnie przycisk odpowiedzialny za poruszenie się w górę/w dół a objekt jest przy samej ścianie funkcja nie pozwoli graczowi poruszyć się dalej. 
        if (other.tag == "TopBound")
        {
            canMoveTop = false;
            playerRb.velocity = new Vector3(0f, 0f, 0f);
        }
        if (other.tag == "BotBound")
        {
            playerRb.velocity = new Vector3(0f, 0f, 0f);
            canMoveBot = false;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //Jeśli gracz nie znajduję się już przy którejś ze ścian funkcja pozwoli mu się swobodnie poruszać.
        if (other.tag == "TopBound") canMoveTop = true;
        if (other.tag == "BotBound") canMoveBot = true;
    }
}
