using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public static float bottomY = -20f;

    [Header("Explosion Visual")]
    public GameObject explosionPrefab;

    [Header("Explosion Force")]
    public float radius = 5.0f;
    public float power = 10.0f;
    public float upwardsModifier = 3.0f;
    public ForceMode forceMode = ForceMode.Force;
    public LayerMask affectedLayers = ~0;

    private bool hasExploded = false;

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(gameObject);

            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleDestroyed();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;

        Debug.Log("Apple hit: " + collision.gameObject.name + " | tag: " + collision.gameObject.tag);

        GameObject hitObject = collision.gameObject;

        if (hitObject.CompareTag("Castle") || hitObject.transform.root.CompareTag("Castle"))
        {
            hasExploded = true;
            ExplodeCastle();
        }

        if (hitObject.CompareTag("Finish"))
        {
            Explode();
            Destroy(gameObject);
            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleDestroyed();
        }

    }

    public void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        SpawnExplosionEffect();
        ApplyExplosionForce();
        Destroy(gameObject);
    }

    public void ExplodeCastle()
    {
        SpawnExplosionEffect();
        ApplyExplosionForce();
        Destroy(gameObject);
    }

    private void SpawnExplosionEffect()
    {
        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                transform.position + Vector3.up * 0.5f,
                Quaternion.identity
            );
        }
    }

    private void ApplyExplosionForce()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius, affectedLayers);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.attachedRigidbody;
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upwardsModifier, forceMode);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}