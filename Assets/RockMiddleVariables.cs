using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RockMiddleVariables : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool CanDrag = false;
    PlayerMovement player;
    Transform playerGraphics;

    private void Start()
    {
        player = GameObject.Find("Character").GetComponent<PlayerMovement>();
        playerGraphics = GameObject.Find("Character_Explorer_Male_01").GetComponent<Transform>();
    }

    public void setCanDragTrue()
    {
        CanDrag = true;
    }

    public void setCanDragFalse()
    {
        CanDrag = false;
    }

    public void setStopPlayerMovementTrue()
    {
        player.StopMovement(true);
    }

    public void setStopPlayerMovementFalse()
    {
        player.StopMovement(false);
    }

    public void movePlayerGraphicsBack()
    {
        playerGraphics.DOMove(player.transform.position - playerGraphics.forward.normalized*0.5f, 1f);
    }

    public void movePlayerGraphicsForward()
    {
        playerGraphics.DOMove(player.transform.position + playerGraphics.forward.normalized, 1f);
    }

}
