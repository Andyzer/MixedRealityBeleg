using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseSpace : MonoBehaviour
{
    public TMP_Text moneyText;

    public GameObject simpleTowerPrefab;
    GameObject boughtTower;

    void Update()
    {
        moneyText.SetText("Money $" + Money.Amount);
    }

    public void BuySimpleTower()
    {
        //enough money? Tower already bought?
        if (Money.Amount < 40 || boughtTower != null)
            return;
        
        //Spawn new Tower
        boughtTower = Instantiate(simpleTowerPrefab, Camera.main.ScreenToWorldPoint(new Vector3(0.5f, 0.5f)), Quaternion.identity);

        //Make tower transparent
        boughtTower.GetComponentInChildren<MeshRenderer>().material.color = new Color(1,1,1,0.5f);
        
        
    }
}
