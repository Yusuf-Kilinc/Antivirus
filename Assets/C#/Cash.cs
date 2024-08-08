using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cash : MonoBehaviour
{
    public int CashMoney;
    public Text CashText;
    int maxmony = 1000000;
    
    // Start is called before the first frame update
    void Start()
    {
        CashMoney = 100;
    }

    // Update is called once per frame
    void Update()
    {
        CashMoney = Mathf.Clamp(CashMoney, 0, maxmony);
        CashText.text = CashMoney.ToString();
    }
}
