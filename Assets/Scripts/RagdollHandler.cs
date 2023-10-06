using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    private Rigidbody[] ragdollRb;
    private Collider[] ragdollColliders;

    public bool isRagdoll;

    // Start is called before the first frame update
    void Start()
    {
        ragdollRb = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        SetRagdollEnabled(isRagdoll);
    }

    public void SetRagdollEnabled(bool enabled)
    {
        foreach (var rb in ragdollRb)
        {
            rb.isKinematic = enabled;
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = !enabled;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            SetRagdollEnabled(false);
        }
    }
}
