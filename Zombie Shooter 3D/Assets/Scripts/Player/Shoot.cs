using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField]
    private GameObject _bloodSplatter; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckShoot();
       
       


    }

    void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);

            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity,1<<0))
            {
                Debug.Log("Hit: " + hitInfo.collider.name);

                Health health = hitInfo.collider.GetComponent<Health>();

                if(health != null)
                {
                   

                    GameObject blood = Instantiate(_bloodSplatter, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

                    Destroy(blood, 0.75f);

                    health.Damage(50);

                }

            }

        }

    }
}
