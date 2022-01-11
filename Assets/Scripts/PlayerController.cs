using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float touchXModifier,speed;
    private float _lastframefingerPosX,_moveFactorX,offsetY;
    public float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;
    Animator animator;
    PickUpOnHand pickUpOnHand;
    GameManager gameManager;
    public GameObject pickupPos;
    void Start()
    {
        pickUpOnHand=GetComponent<PickUpOnHand>();
        gameManager=GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        animator=GetComponent<Animator>();
        animator.SetBool("isRun",false);
         _rotation = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameManager.gameStatus==GameManager.GameStatus.start && PickUpOnHand.stopMoving)
        {
            pickupPos.transform.localPosition=new Vector3(-0.00899999961f,0.750999987f,0.915000021f);   
            if(pickUpOnHand.PickedupObjectNum>0)
            {
                animator.SetBool("HaveStack",true);
            }
            else{
                animator.SetBool("HaveStack",false);
                animator.SetBool("isRun",true);
            }
            animator.SetBool("isJump",false);
            this.transform.position+=transform.forward*speed*Time.deltaTime;
        }
        
        if(Input.touchCount==1)
        {
            Touch touch=Input.GetTouch(0);
            if(touch.phase==TouchPhase.Moved)
            {
                transform.Rotate(0,touch.deltaPosition.x*touchXModifier,0);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
             // rotating flag
            _isRotating = true;
         
            // store mouse
            _mouseReference = Input.mousePosition;    
        }
        if(Input.GetMouseButtonUp(0))
        {
            // rotating flag
            _isRotating = false;
        }
        if(_isRotating)
         {
             // offset
             _mouseOffset = (Input.mousePosition - _mouseReference);
             
             // apply rotation
             _rotation.y = (_mouseOffset.x) * _sensitivity;
             
             // rotate
             transform.Rotate(_rotation);
             
             // store mouse
             _mouseReference = Input.mousePosition;
         } 
    }
}
