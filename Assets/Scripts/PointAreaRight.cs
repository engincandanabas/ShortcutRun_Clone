using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointAreaRight : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public List<int> number1 = new List<int>(); //toplama cikarma
    public List<int> number2 = new List<int>(); //carpma bolme
    [SerializeField] int islem; // 0-toplama,1-cikarma,2-carpma,3-bolme
    private PickUpOnHand pickUpOnHand;
    NPC_PickupHand nPC_PickupHand,nPC_PickupHand1,nPC_PickupHand2,nPC_PickupHand3;
    public PointAreaLeft pointAreaLeft;
    //Sayi 1 ilk listeden toplama ve cıkarma icin eleman cekiyor.Sayi 2 carpma ve bolme icin eleman cekiyor
    int randomListSayi1, randomListSayi2;

    void Awake()
    {
        pickUpOnHand = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpOnHand>();
        nPC_PickupHand = GameObject.FindGameObjectWithTag("NPC_").GetComponent<NPC_PickupHand>();
        nPC_PickupHand1 = GameObject.FindGameObjectWithTag("NPC_1").GetComponent<NPC_PickupHand>();
        nPC_PickupHand2 = GameObject.FindGameObjectWithTag("NPC_2").GetComponent<NPC_PickupHand>();
        nPC_PickupHand3 = GameObject.FindGameObjectWithTag("NPC_3").GetComponent<NPC_PickupHand>();
    }
    void Start()
    {
        //Sol tarafa gore carpma veya bolme degerlerini aliyor
        if (pointAreaLeft.islem == 0)
        {
            islem = 2;
        }
        else if (pointAreaLeft.islem == 1)
        {
            islem = 3;
        }

        //islem carpma ve bolme ise daha kucuk sayilar kullanilmali.
        for (int i = 1; i <= 25; i++)
        {
            if (i <= 3 && i>1)
            {
                number2.Add(i);
            }
            if (i % 3 == 0 && i > 1)
            {
                number1.Add(i);
            }
        }
        //Listeden random sayi cek
        randomListSayi1 = number1[Random.Range(0, number1.Count)];
        randomListSayi2 = number2[Random.Range(0, number2.Count)];
        Debug.Log(islem);
        if (islem == 2)
        {
            numberText.text = "x" + randomListSayi2;
        }
        else if (islem == 3)
        {
            numberText.text = "%" + randomListSayi2;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            switch(islem)
            {
                case 2:
                    //carpma
                    int mevcut=pickUpOnHand.PickedupObjectNum;

                    for(int i=0;i<mevcut*(randomListSayi2-1);i++)
                    {
                       pickUpOnHand.PickObject();      
                    }
                    break;
                case 3:
                    //bolme
                    int mevcut2=pickUpOnHand.PickedupObjectNum;
                    
                    // elindeki tahta eksi duruma dusuyorsa eksiye dusurme elindeki tahtaları bitir
                    
                    /*if(mevcut2-randomListSayi1<=0)
                    {
                        for(int i=0;i<mevcut2;i++)
                        {
                            pickUpOnHand.DestroyObject();
                        }
                        break;
                    }*/
                    //elindeki tahtalar eksi duruma dusmuyorsa platform sayisi kadar dus
                    int sonuc=(int)mevcut2/randomListSayi2;
                    for(int i=0;i<(mevcut2-sonuc);i++)
                    {
                        pickUpOnHand.DestroyObject();
                        mevcut2--;
                    }
                    //Debug kontrol
                    Debug.LogError("İlk Hali:"+mevcut2+"Gidecek Adet:"+randomListSayi2+"Son Hali:"+pickUpOnHand.pickedStackObject);
                    break;
                
            }
        }
        if (col.gameObject.tag == "NPC_")
        {
            Debug.Log("Platforma Girdi");
            switch(islem)
            {
                case 0:
                    //O kadar tahta uret
                    for(int i=0;i<randomListSayi1;i++)
                    {
                       nPC_PickupHand.PickObject();      
                    }
                    break;
                case 1:
                    //cıkarma
                    int mevcut=nPC_PickupHand.PickedupObjectNum;
                    // elindeki tahta eksi duruma dusuyorsa eksiye dusurme elindeki tahtaları bitir
                    
                    if(mevcut-randomListSayi1<=0)
                    {
                        for(int i=0;i<mevcut;i++)
                        {
                            nPC_PickupHand.DestroyObject();
                        }
                        break;
                    }
                    //elindeki tahtalar eksi duruma dusmuyorsa platform sayisi kadar dus
                    for(int i=0;i<randomListSayi1;i++)
                    {
                        nPC_PickupHand.DestroyObject();
                    }
                    //Debug kontrol
                    Debug.Log("İlk Hali:"+mevcut+"Gidecek Adet:"+randomListSayi1+"Son Hali:"+pickUpOnHand.pickedStackObject);
                    break;
                
            }
        }
        if (col.gameObject.tag == "NPC_1")
        {
            Debug.Log("Platforma Girdi");
            switch(islem)
            {
                case 0:
                    //O kadar tahta uret
                    for(int i=0;i<randomListSayi1;i++)
                    {
                       nPC_PickupHand1.PickObject();      
                    }
                    break;
                case 1:
                    //cıkarma
                    int mevcut=nPC_PickupHand1.PickedupObjectNum;
                    // elindeki tahta eksi duruma dusuyorsa eksiye dusurme elindeki tahtaları bitir
                    
                    if(mevcut-randomListSayi1<=0)
                    {
                        for(int i=0;i<mevcut;i++)
                        {
                            nPC_PickupHand1.DestroyObject();
                        }
                        break;
                    }
                    //elindeki tahtalar eksi duruma dusmuyorsa platform sayisi kadar dus
                    for(int i=0;i<randomListSayi1;i++)
                    {
                        nPC_PickupHand1.DestroyObject();
                    }
                    //Debug kontrol
                    Debug.Log("İlk Hali:"+mevcut+"Gidecek Adet:"+randomListSayi1+"Son Hali:"+pickUpOnHand.pickedStackObject);
                    break;
                
            }
        }
        if (col.gameObject.tag == "NPC_2")
        {
            Debug.Log("Platforma Girdi");
            switch(islem)
            {
                case 0:
                    //O kadar tahta uret
                    for(int i=0;i<randomListSayi1;i++)
                    {
                       nPC_PickupHand2.PickObject();      
                    }
                    break;
                case 1:
                    //cıkarma
                    int mevcut=nPC_PickupHand2.PickedupObjectNum;
                    // elindeki tahta eksi duruma dusuyorsa eksiye dusurme elindeki tahtaları bitir
                    
                    if(mevcut-randomListSayi1<=0)
                    {
                        for(int i=0;i<mevcut;i++)
                        {
                            nPC_PickupHand2.DestroyObject();
                        }
                        break;
                    }
                    //elindeki tahtalar eksi duruma dusmuyorsa platform sayisi kadar dus
                    for(int i=0;i<randomListSayi1;i++)
                    {
                        nPC_PickupHand2.DestroyObject();
                    }
                    //Debug kontrol
                    Debug.Log("İlk Hali:"+mevcut+"Gidecek Adet:"+randomListSayi1+"Son Hali:"+pickUpOnHand.pickedStackObject);
                    break;
                
            }
        }
        if (col.gameObject.tag == "NPC_3")
        {
            int stackAmountCarpma = pickUpOnHand.PickedupObjectNum;
            int carpmaFark;
            switch (islem)
            {

                case 2:
                    //carpma
                    int temp = stackAmountCarpma;
                    stackAmountCarpma = stackAmountCarpma * randomListSayi2;
                    carpmaFark = stackAmountCarpma - temp;
                    Debug.Log("İlk Hali:" + temp + "KatSayi:" + randomListSayi2 + "Son Hali:" + stackAmountCarpma + "Fark:" + carpmaFark);
                    for (int i = 0; i < carpmaFark; i++)
                    {
                        nPC_PickupHand.PickObject();
                    }
                    break;
                case 3:
                    //bolme
                    int temp1 = nPC_PickupHand.PickedupObjectNum;
                    int sonuc = nPC_PickupHand.PickedupObjectNum / randomListSayi2;
                    int fark = nPC_PickupHand.PickedupObjectNum - sonuc;
                    if (sonuc < 0)
                    {
                        for (int i = 0; i < nPC_PickupHand.PickedupObjectNum; i++)
                        {
                            nPC_PickupHand.DestroyObject();
                        }
                    }
                    else
                    {

                        for (int i = 0; i < fark; i++)
                        {
                            nPC_PickupHand.DestroyObject();
                        }
                    }
                    Debug.Log("İlk Hali:" + temp1 + "Son Hali:" + nPC_PickupHand.PickedupObjectNum + "Fark:" + fark);
                    break;
            }
        }
    }
}
