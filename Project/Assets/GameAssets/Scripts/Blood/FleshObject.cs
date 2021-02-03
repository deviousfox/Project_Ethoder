using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshObject : MonoBehaviour
{
    public GameObject Decal;
    private Rigidbody rb;
    private FleshObjectType fleshType;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        fleshType = FleshObjectType.None;
        CalculeteNormal(collision);
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.green,1000f);
        }
        GameObject TempDecal = Instantiate(
            Decal, 
            collision.GetContact(Random.Range(0, collision.contactCount)).point + (collision.GetContact(Random.Range(0, collision.contactCount)).normal * 0.001f), 
            Quaternion.FromToRotation(Vector3.forward, -collision.GetContact(Random.Range(0, collision.contactCount)).normal)
            );
        TempDecal.transform.localScale = TempDecal.transform.localScale * (1 + collision.contactCount * 0.25f + (1 *Random.Range(0.7f,1.3f)));
        rb.velocity = rb.velocity / 2;
    }
    private void CalculeteNormal(Collision collision)
    {
        Vector3 normal = collision.GetContact(Random.Range(0, collision.contactCount)).normal;
        float angle = Vector3.Angle(Vector3.up, normal);
        print(angle);
    }
}
