using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpOnHand : MonoBehaviour
{
    [Header("Stacked Object Counter")]
    [SerializeField] private GameObject pickupPos;
    public int PickedupObjectNum = 0;
    [Header("Destroy Stacked Object")]
    [SerializeField] private int numberOfPickupsToDestroy = 1;
    private List<GameObject> PickedupObjectNumList = new List<GameObject>();
    private bool disableWaitFirstTime = false, camFinisLerp;
    public bool isGrounded = true;
    public GameObject pickedStackObject;
    bool checkPutDownObject = true;
    public static bool stopMoving;
    private PlayerController playerController;
    private GameManager gameManager;
    private Animator animator;
    private Rigidbody rb;
    [SerializeField] private float lerpTime, stackCamModifier;
    [SerializeField] private int zForce, yForce;
    bool jumpControl;
    float elapsedTime;
    public TextMeshProUGUI stackedText;
    private float stackHolderNumber = 0;
    [Range(0, 1)]
    [SerializeField] private float pickupLerpTime;
    public GameObject cam;
    public GameObject[] bonusStates;
    public Vector3 bonusStatesV;

    public enum BonusControl
    {
        _1x,
        _2x,
        _3x,
        _4x,
        _5x,
        _6x,
        _7x,
        _8x,
        _9x,
        _10x
    };
    public BonusControl bonusControl;
    void Start()
    {
        jumpControl = false;
        stopMoving = true;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        checkPutDownObject = true;
        //HingJoint 

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Floor" && transform.position.y < -.5f)
        {
            Debug.Log("tirmanmaya uygun duruma getir");

            //tirmanmaya uygun duruma getir
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, -1.1f, transform.position.z);
            //ziplamayi kapat tirmanma anim. etkinlestir
            animator.SetBool("isJump", false);
            animator.SetBool("isClimb", true);
            Debug.Log("Zemine Girildi.Zemin Adi: " + col.name);
            isGrounded = true;
        }
        if (col.gameObject.tag == "Floor" && transform.position.y >= -.5f)
        {
            Debug.Log("tirmanma ihtiyacı yok,karakter direk platforma zipliyor");

            //tirmanma ihtiyacı yok
            //karakter direk platforma zipliyor
            if (gameManager.gameStatus == GameManager.GameStatus.start)
            {
                //ziplama animasyonunu durdurup kosmaya devam et
                animator.SetBool("isJump", false);
                animator.SetBool("isRun", true);
            }
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;

            Debug.Log("Zemine Girildi.Zemin Adi: " + col.name);
            isGrounded = true;
        }
        //Stack either hand or under character based on stackOrPickup bool
        if (col.gameObject.tag == "Pickup")
        {
            stackHolderNumber++;
            stackedText.gameObject.SetActive(true);
            stackedText.text = "+" + stackHolderNumber;
            Invoke("WaitForReset", .9f);
            PickedupObjectNum++;
            //Add object to list
            PickedupObjectNumList.Add(col.gameObject);
            col.gameObject.transform.SetParent(null);
            //Change tag to prevent unwanted collisions
            col.gameObject.tag = "Untagged";
            gameManager.cam.transform.localPosition += new Vector3(0, stackCamModifier, -stackCamModifier);

        }
        if (col.gameObject.CompareTag("1X"))
        {
            bonusControl = BonusControl._1x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("2X"))
        {
            bonusControl = BonusControl._2x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("3X"))
        {
            bonusControl = BonusControl._3x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("4X"))
        {
            bonusControl = BonusControl._4x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("5X"))
        {
            bonusControl = BonusControl._5x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("6X"))
        {
            bonusControl = BonusControl._6x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("7X"))
        {
            bonusControl = BonusControl._7x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("8X"))
        {
            bonusControl = BonusControl._8x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("9X"))
        {
            bonusControl = BonusControl._9x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
        }
        else if (col.gameObject.CompareTag("10X"))
        {
            bonusControl = BonusControl._10x;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
            stopMoving = true;
            isGrounded = true;
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
        if (col.gameObject.CompareTag("1X") || col.gameObject.CompareTag("2X") || col.gameObject.CompareTag("3X") || col.gameObject.CompareTag("4X") || col.gameObject.CompareTag("5X") || col.gameObject.CompareTag("6X") || col.gameObject.CompareTag("7X") || col.gameObject.CompareTag("8X") || col.gameObject.CompareTag("9X") || col.gameObject.CompareTag("10X"))
        {
            Debug.Log("Zeminden Cikildi.Zemin Adi: " + col.name);
            isGrounded = false;
            if (PickedupObjectNumList.Count == 0)
            {
                BonusCheckState();
            }
            else
            {
                if (checkPutDownObject)
                {
                    checkPutDownObject = false;
                    StartCoroutine(PutDownObjects());
                }
            }

        }


    }
    void WaitForReset()
    {
        stackHolderNumber = 0;
        stackedText.gameObject.SetActive(false);
    }
    IEnumerator PutDownObjects()
    {
        //ilk defa bu method cagriliyorsa kontrolu
        if (!disableWaitFirstTime)
            yield return new WaitForSeconds(.08f);
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
                gameManager.cam.transform.localPosition += new Vector3(0, -stackCamModifier, stackCamModifier);
                StartCoroutine(PutDownObjects());

            }
            catch (ArgumentOutOfRangeException ex)
            {
                //Jump and lose
                Jump();
                //check bonus
                //LoseGame();
            }

        }
    }
    public void PickObject()
    {
        PickedupObjectNum++;
        //Add object to list
        GameObject pickedObject = Instantiate(pickedStackObject, pickupPos.transform.position, Quaternion.identity);
        PickedupObjectNumList.Add(pickedObject.gameObject);
        pickedObject.gameObject.tag = "Untagged";
        pickedObject.transform.SetParent(null);
        gameManager.cam.transform.localPosition += new Vector3(0, stackCamModifier, -stackCamModifier);

    }
    public void DestroyObject()
    {
        GameObject obj = PickedupObjectNumList[PickedupObjectNumList.Count - 1];
        obj.transform.parent = null;
        PickedupObjectNumList.RemoveAt(PickedupObjectNumList.Count - 1);
        PickedupObjectNum--;
        gameManager.cam.transform.localPosition += new Vector3(0, -stackCamModifier, stackCamModifier);
        Destroy(obj.gameObject);
    }
    void LoseGame()
    {
        cam.gameObject.transform.SetParent(null);
        gameManager.gameStatus = GameManager.GameStatus.fail;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        //Eger asagi dogru dusmeye baslar bitir
        stopMoving = false;
        playerController.speed = 0;
        animator.SetBool("isRun", false);
    }
    void BonusCheckState()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        stopMoving = true;
        playerController.speed = 0;
        animator.SetBool("isVictory", true);
        cam.transform.SetParent(null);
        camFinisLerp=true;
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        if (bonusControl == BonusControl._1x)
        {
            this.transform.position = bonusStates[0].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._2x)
        {
            this.transform.position = bonusStates[1].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._3x)
        {
            this.transform.position = bonusStates[2].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._4x)
        {
            this.transform.position = bonusStates[3].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._5x)
        {
            this.transform.position = bonusStates[4].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._6x)
        {
            this.transform.position = bonusStates[5].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._7x)
        {
            this.transform.position = bonusStates[6].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._8x)
        {
            this.transform.position = bonusStates[7].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._9x)
        {
            this.transform.position = bonusStates[8].transform.position + bonusStatesV;
        }
        else if (bonusControl == BonusControl._10x)
        {
            this.transform.position = bonusStates[9].transform.position + bonusStatesV;
        }


    }
    void Jump()
    {
        //Zıplama kontrolunu etkinlestir
        //yer cekimini etkin hale getir
        //kuvvet ekle
        stopMoving = false;
        jumpControl = true;
        rb.useGravity = true;
        //igidbody.velocity=(playerController.GetComponent<Transform>().transform.forward*zForce)+(playerController.GetComponent<Transform>().transform.up*yForce);
        rb.AddForce((playerController.GetComponent<Transform>().transform.forward * zForce) + (playerController.GetComponent<Transform>().transform.up * yForce), ForceMode.VelocityChange);
        animator.SetBool("isJump", true);
        animator.SetBool("isRun", false);


    }
    void FixedUpdate()
    {
        if (jumpControl)
        {   //zipladi ve tutunamadı 
            //level fail
            if (FinishArea.isBonusStatus && playerController.GetComponent<Transform>().transform.position.y < -1f)
            {
                BonusCheckState();
            }
            else if (playerController.GetComponent<Transform>().transform.position.y < -5)
            {
                LoseGame();
            }
        }

        for (int i = 0; i < PickedupObjectNumList.Count; i++)
        {
            PickedupObjectNumList[i].transform.rotation = this.transform.rotation;
            if (i != 0)
            {
                PickedupObjectNumList[i].transform.position = Vector3.Lerp(PickedupObjectNumList[i].transform.position, PickedupObjectNumList[i - 1].transform.position + new Vector3(0, (PickedupObjectNumList[i - 1].gameObject.transform.localScale.y + 0.02f), 0), pickupLerpTime);
            }
            else
            {
                PickedupObjectNumList[i].transform.position = pickupPos.transform.position;
            }

        }
        if (camFinisLerp)
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, this.transform.position + new Vector3(0,5f, -10f), 0.2f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation,Quaternion.Euler(new Vector3(25, 0, 0)),0.2f);
        }

    }
    public void ClimbOver()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        animator.SetBool("isClimb", false);
        animator.SetBool("isRun", true);

        stopMoving = true;
    }

}
