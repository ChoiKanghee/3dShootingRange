using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    public GameObject explosionEffect;
    public float explosionRadius = 5f;
    public float explosionForce;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        BlowObject();
    }
    private void BlowObject()
    {
        Collider[] affectedObjects=Physics.OverlapSphere(transform.position,explosionRadius);
        foreach(Collider obj in affectedObjects)
        {
           if(obj.GetComponent<Rigidbody>()!=null)
            {
                obj.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, 
                    transform.position, explosionRadius, 1,
                    ForceMode.Impulse);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

