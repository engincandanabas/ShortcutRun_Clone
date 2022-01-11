using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointAreaLeft : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public List<int> number1=new List<int>(); //toplama cikarma
    public List<int> number2=new List<int>(); //carpma bolme
    [SerializeField] public int islem=0; // 0-toplama,1-cikarma,2-carpma,3-bolme
    PickUpOnHand pickUpOnHand;
    NPC_PickupHand nPC_PickupHand,nPC_PickupHand1,nPC_PickupHand2,nPC_PickupHand3;
    //Sayi 1 ilk listeden toplama ve cıkarma icin eleman cekiyor.Sayi 2 carpma ve bolme icin eleman cekiyor
    int randomListSayi1,randomListSayi2;
    void Awake()
    {
        pickUpOnHand=GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpOnHand>();
        nPC_PickupHand = GameObject.FindGameObjectWithTag("NPC_").GetComponent<NPC_PickupHand>();
        nPC_PickupHand1 = GameObject.FindGameObjectWithTag("NPC_1").GetComponent<NPC_PickupHand>();
        nPC_PickupHand2 = GameObject.FindGameObjectWithTag("NPC_2").GetComponent<NPC_PickupHand>();
        nPC_PickupHand3 = GameObject.FindGameObjectWithTag("NPC_3").GetComponent<NPC_PickupHand>();
        //Sol taraf icin toplama veya cikarma islemlerinden herhangi birini random sececek.
        //Sag tarafta toplamaysa carpma,cikarmaysa bolme olacak.
        islem=Random.Range(0,2);
        //islem carpma ve bolme ise daha kucuk sayilar kullanilmali.
        for(int i=1;i<=25;i++)
        {   
            if(i<=3 )
            {
                number2.Add(i);
            }
            if(i%3==0 && i>1)
            {
                number1.Add(i);
            }
        }
        //Listeden random sayi cek
        randomListSayi1=number1[Random.Range(0,number1.Count)];
        randomListSayi2=number2[Random.Range(0,number2.Count)];
        Debug.Log(islem);
        if(islem==0)
        {
            numberText.text="+"+randomListSayi1;
        }
        else if(islem==1)
        {
            numberText.text="-"+randomListSayi1;
        }
        
    }
    void OnTriggerEnter(Collider col)
    {
        //Debug.LogError(col.name);
        if(col.gameObject.tag=="Player")
        {
            Debug.Log("Platforma Girdi");
            switch(islem)
            {
                case 0:
                    //O kadar tahta uret
                    for(int i=0;i<randomListSayi1;i++)
                    {
                       pickUpOnHand.PickObject();      
                    }
                    break;
                case 1:
                    //cıkarma
                    int mevcut=pickUpOnHand.PickedupObjectNum;
                    // elindeki tahta eksi duruma dusuyorsa eksiye dusurme elindeki tahtaları bitir
                    
                    if(mevcut-randomListSayi1<=0)
                    {
                        for(int i=0;i<mevcut;i++)
                        {
                            pickUpOnHand.DestroyObject();
                        }
                        break;
                    }
                    //elindeki tahtalar eksi duruma dusmuyorsa platform sayisi kadar dus
                    for(int i=0;i<randomListSayi1;i++)
                    {
                        pickUpOnHand.DestroyObject();
                    }
                    //Debug kontrol
                    Debug.Log("İlk Hali:"+mevcut+"Gidecek Adet:"+randomListSayi1+"Son Hali:"+pickUpOnHand.pickedStackObject);
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
            Debug.Log("Platforma Girdi");
            switch(islem)
            {
                case 0:
                    //O kadar tahta uret
                    for(int i=0;i<randomListSayi1;i++)
                    {
                       nPC_PickupHand3.PickObject();      
                    }
                    break;
                case 1:
                    //cıkarma
                    int mevcut=nPC_PickupHand3.PickedupObjectNum;
                    // elindeki tahta eksi duruma dusuyorsa eksiye dusurme elindeki tahtaları bitir
                    
                    if(mevcut-randomListSayi1<=0)
                    {
                        for(int i=0;i<mevcut;i++)
                        {
                            nPC_PickupHand3.DestroyObject();
                        }
                        break;
                    }
                    //elindeki tahtalar eksi duruma dusmuyorsa platform sayisi kadar dus
                    for(int i=0;i<randomListSayi1;i++)
                    {
                        nPC_PickupHand3.DestroyObject();
                    }
                    //Debug kontrol
                    Debug.Log("İlk Hali:"+mevcut+"Gidecek Adet:"+randomListSayi1+"Son Hali:"+pickUpOnHand.pickedStackObject);
                    break;
                
            }
        }
    }

}
