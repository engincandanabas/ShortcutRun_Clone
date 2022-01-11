using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_PickupHand : MonoBehaviour
{
    [Header("Stacked Object Counter")]
    [SerializeField] private GameObject pickupPos;
    public int PickedupObjectNum = 0;
    [Header("Destroy Stacked Object")]
    [SerializeField] private int numberOfPickupsToDestroy = 1;
    private List<GameObject> PickedupObjectNumList = new List<GameObject>();
    private bool disableWaitFirstTime = false;
    public bool isGrounded = true;
    public GameObject pickedStackObject;
    bool checkPutDownObject = true;
    private Rigidbody rb;
    public static bool stopMoving;
    private float stackHolderNumber = 0;
    [SerializeField] private int zForce, yForce;
    NPCNavmesh nPCNavmesh;
    void Start()
    {
        nPCNavmesh = GetComponent<NPCNavmesh>();
        rb = GetComponent<Rigidbody>();
        checkPutDownObject = true;
        stopMoving = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Floor")
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        //Stack either hand or under character based on stackOrPickup bool
        if (col.gameObject.tag == "Pickup")
        {
            stackHolderNumber++;
            PickedupObjectNum++;
            //Add object to list
            PickedupObjectNumList.Add(col.gameObject);
            col.gameObject.transform.SetParent(null);
            //Change tag to prevent unwanted collisions
            col.gameObject.tag = "Untagged";


        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Floor")
            isGrounded = true;
        disableWaitFirstTime = true;
        checkPutDownObject = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Floor")
        {
            Debug.Log("Zeminden Cikildi.Zemin Adi: " + col.name);
            isGrounded = false;
            if (checkPutDownObject)
            {
                checkPutDownObject = false;
                StartCoroutine(PutDownObjects());
            }

        }


    }
    void FixedUpdate()
    {
        for (int i = 0; i < PickedupObjectNumList.Count; i++)
        {
            PickedupObjectNumList[i].transform.rotation = this.transform.rotation;
            if (i != 0)
            {
                PickedupObjectNumList[i].transform.position = Vector3.Lerp(PickedupObjectNumList[i].transform.position, PickedupObjectNumList[i - 1].transform.position + new Vector3(0, (PickedupObjectNumList[i - 1].gameObject.transform.localScale.y + 0.02f), 0), 0.8f);
            }
            else
            {
                PickedupObjectNumList[i].transform.position = pickupPos.transform.position;
            }

        }
    }
    void WaitForReset()
    {
        stackHolderNumber = 0;
        //stackedText.gameObject.SetActive(false);
    }
    IEnumerator PutDownObjects()
    {
        //ilk defa bu method cagriliyorsa kontrolu
        if (!disableWaitFirstTime)
            yield return new WaitForSeconds(.15f);
        disableWaitFirstTime = false;
        //zeminde degilse stackleri dagitmaya baslayacak
        if (!isGrounded)
        {
            try
            {
                GameObject obj = PickedupObjectNumList[PickedupObjectNumList.Count - 1];
                obj.transform.parent = null;
                obj.transform.position = new Vector3(pickupPos.transform.position.x, 0, pickupPos.transform.position.z);
                obj.gameObject.tag = "FloorStack";
                PickedupObjectNumList.RemoveAt(PickedupObjectNumList.Count - 1);
                PickedupObjectNum--;
                StartCoroutine(PutDownObjects());

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.Log(ex);
                //Jump and lose
                Jump();
                //LoseGame();
            }

        }
    }
    public void DestroyObject()
    {
        GameObject obj = PickedupObjectNumList[PickedupObjectNumList.Count - 1];
        obj.transform.parent = null;
        PickedupObjectNumList.RemoveAt(PickedupObjectNumList.Count - 1);
        PickedupObjectNum--;
        Destroy(obj.gameObject);
    }
    public void PickObject()
    {
        PickedupObjectNum++;
        //Add object to list
        GameObject pickedObject = Instantiate(pickedStackObject, pickupPos.transform.position, Quaternion.identity);
        PickedupObjectNumList.Add(pickedObject.gameObject);
        pickedObject.gameObject.tag = "Untagged";
        pickedObject.transform.SetParent(null);
    }
    void Jump()
    {
        //Zıplama kontrolunu etkinlestir
        //yer cekimini etkin hale getir
        //kuvvet ekle
        stopMoving = false;
        //jumpControl=true;
        rb.useGravity = true;
        //igidbody.velocity=(playerController.GetComponent<Transform>().transform.forward*zForce)+(playerController.GetComponent<Transform>().transform.up*yForce);
        rb.AddForce((nPCNavmesh.GetComponent<Transform>().transform.forward * zForce) + (nPCNavmesh.GetComponent<Transform>().transform.up * yForce), ForceMode.VelocityChange);
        //animator.SetBool("isJump",true);
        //animator.SetBool("isRun",false);


    }
}
