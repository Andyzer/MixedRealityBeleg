using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseSpace : MonoBehaviour
{
    public TMP_Text moneyText;

    public GameObject towerPrefab;
    GameObject boughtTower;

    void Update()
    {
        moneyText.SetText("Money $" + Money.Amount);
        
        if (boughtTower != null)
            MovePurchasedTower();
    }

    void MovePurchasedTower()
    {
        boughtTower.transform.position = Camera.main.transform.position +
                                         Camera.main.transform.forward * 5;
    }

    void CheckForWall()
    {
        Ray raycast = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        
        //TODO Remove next line
        Debug.DrawRay(raycast.origin, raycast.direction * 100, Color.red);

        if (Physics.Raycast(raycast, out hit))
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                //Put tower on wall
                boughtTower.transform.position = hit.collider.gameObject.transform.position +
                                                 new Vector3(0, 0.75f, 0);

                if (Input.GetMouseButtonDown(0))
                {
                    //take away wall tag
                    hit.collider.gameObject.tag = "Untagged";
                
                    Color color = boughtTower.GetComponent<MeshRenderer>().material.color;
                    color.a = 1f;
                    boughtTower.GetComponent<MeshRenderer>().material.color = color;
                
                    //enable tower script
                    boughtTower.GetComponent<Tower>().enabled = true;
                    boughtTower = null;    
                }
                
            }
        }
    }
    
    public void BuyTower()
    {
        //enough money? Tower already bought?
        if (Money.Amount < 40 || boughtTower != null)
            return;
        
        //Spawn new Tower
        boughtTower = Instantiate(towerPrefab, Camera.main.ScreenToWorldPoint(new Vector3(0.5f, 0.5f)), Quaternion.identity);

        //Make tower transparent
        
        Color color = boughtTower.GetComponent<MeshRenderer>().material.color;
       color.a = 0.5f;
       boughtTower.GetComponent<MeshRenderer>().material.color = color;
       
       //Take money away
       Money.Amount -= 40;
    }
}
