using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseSpace : MonoBehaviour
{
    public TMP_Text moneyText;

    void Update()
    {
        moneyText.SetText("Money $" + Money.Amount);
    }
}
