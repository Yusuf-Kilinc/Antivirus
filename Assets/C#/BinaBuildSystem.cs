using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaBuildSystem : MonoBehaviour
{
    public List<GameObject> Cevre; // Liste içerisindeki objeler
    public int i; // Aktif edilecek objenin indeksi
    public int Maliyet;
    public Cash cashMoney;
    void Update()
    {
      //  SetActiveObject(i);

        Maliyet = i * 500;


    }

    //void SetActiveObject(int index)
    //{
    //    // Liste içinde dönerek her bir objeyi kontrol et
    //    for (int j = 0; j < Cevre.Count; j++)
    //    {
    //        // j eþitse index'e, objeyi aktif et; deðilse, objeyi deaktif et
    //        if (j == index)
    //        {
    //            Cevre[j].SetActive(true);
    //        }
    //        else
    //        {
    //            Cevre[j].SetActive(false);
    //        }
    //    }
    //}



    public void Levellendir()
    {
        if (cashMoney.CashMoney >= Maliyet)
        {
            i += 1;
        }
    }


}
