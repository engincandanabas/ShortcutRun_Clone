using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NPCNavmesh : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;
    [SerializeField] private float raycastOffset, maxDistance, speed, timerHit;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 targetPoint;
    NPC_PickupHand nPC_Pickup;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;
    bool controlNavMesh, controlRayCast,enabledFirstTime;
    GameManager gameManager;
    public GameObject npc;
    Vector3 direction;
    private bool checkTrigger;

    void Awake()
    {
        nPC_Pickup = GetComponent<NPC_PickupHand>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        navMeshAgent.enabled = false;
        controlNavMesh = false;
        controlRayCast = false;
        enabledFirstTime=false;
        checkTrigger=true;
        timerHit = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position=new Vector3(this.transform.position.x,0.9f,this.transform.position.z);
        if(this.transform.localEulerAngles.y<=340f && this.transform.localEulerAngles.y>=270f)
        {
            //Debug.LogWarning("Right");
            direction=transform.right;
        }
        if(this.transform.localEulerAngles.y>=20f && this.transform.localEulerAngles.y<=90f)
        {
            //Debug.LogWarning("Left");
            direction=-transform.right;
        }
        if((this.transform.localEulerAngles.y<20f && transform.localEulerAngles.y>0f) || (this.transform.localEulerAngles.y>340f && this.transform.localEulerAngles.y<360f) )
        {
            //Debug.LogWarning("Forward");
            direction=transform.forward;
        }
        if (gameManager.gameStatus == GameManager.GameStatus.start)
        {
            //Debug.LogWarning("Oyun Basladi");
            if(!enabledFirstTime)
            {
                navMeshAgent.enabled=true;
                enabledFirstTime=true;
            }
            RaycastHit hit;
            if (Physics.Raycast(transform.position + (Vector3.up * raycastOffset), direction, out hit, maxDistance, layerMask) && !controlRayCast)
            {
                //Debug.LogWarning("Hit Distance: "+(int)hit.distance+"Hit Distance*0.7: "+(int)hit.distance*0.95f+"Stacked Number: "+nPC_Pickup.PickedupObjectNum);
                if ((int)hit.distance * 0.90f <= nPC_Pickup.PickedupObjectNum)
                {
                    controlNavMesh = true;
                    controlRayCast = true;
                    targetPoint = new Vector3(hit.point.x, transform.localPosition.y, hit.point.z);
                    Debug.DrawLine(transform.position + (Vector3.forward * raycastOffset), hit.point, Color.red);
                }
                else{
                    controlRayCast=false;
                }
                Debug.DrawLine(transform.position + (Vector3.forward * raycastOffset), hit.point, Color.blue);
                //Debug.LogError("Hit Point" + hit.point);
                //Debug.LogError("Distance Hit:" + hit.distance);
                //Debug.LogWarning("Stack Number: "+NPC_PickupHand.stackHolderNumber);


            }
            if (!controlNavMesh)
            {
                navMeshAgent.enabled = true;
                navMeshAgent.destination = movePositionTransform.position;
            }
            if (controlNavMesh)
            {
                navMeshAgent.enabled = false;
                this.transform.LookAt(targetPoint);
                //Debug.LogWarning("Hit Distance: " + hit.distance + " Stacked Object: " + nPC_Pickup.PickedupObjectNum);
                this.transform.position = Vector3.MoveTowards(this.transform.position, targetPoint, Time.deltaTime * speed);
                if (transform.position == targetPoint)
                {
                    //Debug.LogError("Control Point");
                    navMeshAgent.enabled=true;
                    controlRayCast=false;
                    controlNavMesh = false;
                    this.gameObject.GetComponent<NPC_PickupHand>().isGrounded=true;
                    checkTrigger=true;
                    //Debug.LogWarning("Stack Number: "+NPC_PickupHand.stackHolderNumber);
                }
                if(checkTrigger)
                {
                    Debug.LogError("Trigger Tetiklendi");
                    checkTrigger=false;
                    this.gameObject.GetComponent<NPC_PickupHand>().isGrounded=false;
                    this.gameObject.GetComponent<NPC_PickupHand>().PutDownObjects();
                }

            }
        }

    }
    IEnumerator RaycastDisable()
    {
        yield return new WaitForSeconds(1);
        controlRayCast = false;
    }
}


