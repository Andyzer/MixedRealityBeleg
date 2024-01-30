using UnityEngine;

public class Tower : MonoBehaviour
{
    public float fireRate = 1.0f;
    public float range = 3.0f;
    public GameObject bulletPrefab;
    public Transform barrelExit;

    public Transform target;
    float fireCounter = 0;
    
    // Update is called once per frame
    void Update()
    {
        FindNextTarget();
        if (target != null)
        {
            AimAtTarget();
            Shoot();
        }
    }
    
    void FindNextTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, 1 << LayerMask.NameToLayer("Enemy")); // Adjust layer mask as per your setup

        if (enemies.Length > 0)
        {
            target = enemies[0].gameObject.transform;
            foreach (Collider enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < Vector3.Distance(transform.position, target.position)) 
                {
                    target = enemy.gameObject.transform;
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

    void Shoot()
    {
        //See if we can shoot 
        if (fireCounter <= 0 && target != null)
        {
            GameObject newBullet = Instantiate(bulletPrefab, barrelExit.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().target = target;
            // Increment the fire counter
            fireCounter = fireRate;
        }
        else
        {
            fireCounter -= Time.deltaTime;
        }
    }


    
}
