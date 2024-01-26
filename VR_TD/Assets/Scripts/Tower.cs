using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform target;
    
    public float range = 3.0f;

    // Update is called once per frame
    void Update()
    {
        FindNextTarget();
        if (target != null)
        {
            AimAtTarget();
        }
    }
    
    void FindNextTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, 1 << 8); // Adjust layer mask as per your setup

        if (enemies.Length > 0)
        {
            float shortestDistance = Mathf.Infinity;
            foreach (Collider enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    target = enemy.transform;
                }
            }
        }
        else
        {
            target = null;
        }
    }

    void AimAtTarget()
    {
        Vector3 lookPosition = target.position - transform.position;
        lookPosition.y = 0;

        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = rotation;
    }


    
}
