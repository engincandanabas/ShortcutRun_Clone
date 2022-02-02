using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    public GameObject finishPlatforms;
    public PlayerController playerController;
    private bool control=false;
    public static bool isBonusStatus;
    

    void Start()
    {
        isBonusStatus=false;
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(!control)
            {
                //Game won.Go for bonus
                isBonusStatus=true;
                finishPlatforms.SetActive(true);
            }
            else
            {
                //Game losed
                playerController.speed=0;
                //lose anim

            }
        }
        else if(col.gameObject.CompareTag("NPC_") || col.gameObject.CompareTag("NPC_1") || col.gameObject.CompareTag("NPC_2") || col.gameObject.CompareTag("NPC_3"))
        {
            control=true;
        }
    }
}
