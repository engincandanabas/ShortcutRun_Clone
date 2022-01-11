using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GetPointPlatform : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    public List<int> number1=new List<int>(); //toplama cikarma
    public List<int> number2=new List<int>(); //carpma bolme
    public int islem; // 0-toplama,1-cikarma,2-carpma,3-bolme
    public PickUpOnHand pickUpOnHand;
    //Sayi 1 ilk listeden toplama ve cıkarma icin eleman cekiyor.Sayi 2 carpma ve bolme icin eleman cekiyor
    int randomListSayi1,randomListSayi2;
    // Start is called before the first frame update
    void Start()
    {
        pickUpOnHand=GameObject.Find("Player").GetComponent<PickUpOnHand>();
        //islem carpma ve bolme ise daha kucuk sayilar kullanilmali.
        for(int i=1;i<=25;i++)
        {   
            if(i==3 || i==2)
            {
                number2.Add(i);
            }
            if(i%5==0)
            {
                number1.Add(i);
            }
        }
        //Listeden random sayi cek
        randomListSayi1=number1[Random.Range(0,number1.Count)];
        randomListSayi2=number1[Random.Range(0,number2.Count)];
        Debug.Log(islem);
        if(islem==0)
        {
            numberText.text="+"+randomListSayi1;
        }
        else if(islem==1)
        {
            numberText.text="-"+randomListSayi1;
        }
        else if(islem==2)
        {
            numberText.text="x"+randomListSayi2;
        }
        else if(islem==3)
        {
            numberText.text="%"+randomListSayi2;
        }

    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            int stackAmountCarpma=pickUpOnHand.PickedupObjectNum;
            int carpmaFark;
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
                case 2:
                    //carpma
                    int temp=stackAmountCarpma;
                    stackAmountCarpma=stackAmountCarpma*randomListSayi2;
                    carpmaFark=stackAmountCarpma-temp;
                    Debug.Log("İlk Hali:"+temp+"KatSayi:"+randomListSayi2+"Son Hali:"+stackAmountCarpma+"Fark:"+carpmaFark);
                    for(int i=0;i<carpmaFark;i++)
                    {
                       pickUpOnHand.PickObject();      
                    }
                    break;
                case 3:
                    //bolme
                    int temp1=pickUpOnHand.PickedupObjectNum;
                    int sonuc=pickUpOnHand.PickedupObjectNum/randomListSayi2;
                    int fark=pickUpOnHand.PickedupObjectNum-sonuc;
                    if(sonuc<0)
                    {
                        for(int i=0;i<pickUpOnHand.PickedupObjectNum;i++)
                        {
                            pickUpOnHand.DestroyObject();
                        }
                    }
                    else
                    {
                        
                        for(int i=0;i<fark;i++)
                        {
                            pickUpOnHand.DestroyObject();
                        }
                    }
                    Debug.Log("İlk Hali:"+temp1+"Son Hali:"+pickUpOnHand.PickedupObjectNum+"Fark:"+fark);
                    break;
            }
        }
    }
    
}
