using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
     public bool canDealDamage;
    private List<GameObject> hasDealtDamage;

    [SerializeField] private float punchRange = 1.0f; // Range of the punch
    [SerializeField] private float punchDamage = 10.0f; // Damage dealt by the punch
    [SerializeField] private LayerMask targetLayerMask; // Layer mask for detecting targets

    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
    }

    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, punchRange, targetLayerMask))
            {
                if (!hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    print("Damage dealt to " + hit.transform.name);
                    // Assuming the target has a method to take damage
                    hit.transform.GetComponent<Enemy>().TakeDamage(punchDamage);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
        }
    }

    public void StartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }

    public void EndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * punchRange);
    }
}