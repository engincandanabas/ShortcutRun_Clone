using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cam;
    [SerializeField] GameObject[] gameUI,countDownUI;
    public TextMeshProUGUI playerNameText,counterText;
    public TMP_InputField enteryourName;
    [SerializeField] GameObject cameraLerpPos;
    [SerializeField] float lerpTime;
    bool checkCam=true;
    public int countdownTimer;

    [Header("Game Status")]
    public GameStatus gameStatus;
    public enum GameStatus{
        notStart,
        readyToStart,
        start,
        fail
    }
    
    void Awake()
    {
        gameStatus=GameStatus.notStart;
        for (int i = 0; i < countDownUI.Length; i++)
        {
            countDownUI[i].SetActive(false);
        }
        enteryourName.text="Player";
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus!=GameStatus.readyToStart)
        {
            playerNameText.text=enteryourName.text;
        }
        if(gameStatus==GameStatus.readyToStart)
        {
            cam.transform.position=Vector3.Lerp(cam.transform.position,cameraLerpPos.transform.position,lerpTime*Time.deltaTime);
        }
        
    }
    public void StartGame()
    {
        
        for (int i = 0; i < gameUI.Length; i++)
        {
            gameUI[i].SetActive(false);
        }
        for (int i = 0; i < countDownUI.Length; i++)
        {
            countDownUI[i].SetActive(true);
        }
        gameStatus=GameStatus.readyToStart;
        StartCoroutine(CountdownToStart());

    }

    IEnumerator CountdownToStart()
    {
        while(countdownTimer>0)
        {
            counterText.text=countdownTimer.ToString();
            yield return new WaitForSeconds(1);
            countdownTimer--;
        }
        counterText.text="GO!";
        gameStatus=GameStatus.start;
        yield return new WaitForSeconds(1);
        counterText.gameObject.SetActive(false);
    }
}
