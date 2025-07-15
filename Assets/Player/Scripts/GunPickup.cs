using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPick : MonoBehaviour
{
    public Transform equipPosition;         // This should be your HoldPoint
    public float distance = 10f;            // Raycast distance
    private GameObject currentWeapon;       // Currently held weapon
    private GameObject wp;                  // Weapon being looked at
    private bool canGrab;                   // If player can pick up the weapon

    void Update()
    {
        CheckWeapons();

        if (canGrab && Input.GetKeyDown(KeyCode.E))
        {
            if (currentWeapon == null)
                PickUp();
        }

        if (currentWeapon != null && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    void CheckWeapons()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance))
        {
            if (hit.transform.CompareTag("CanGrab"))
            {
                Debug.Log("I can grab it!");
                canGrab = true;
                wp = hit.transform.gameObject;
            }
            else
            {
                canGrab = false;
            }
        }
        else
        {
            canGrab = false;
        }
    }

    void PickUp()
    {
        Debug.Log("Picking up weapon...");
        currentWeapon = wp;
        currentWeapon.transform.position = equipPosition.position;
        currentWeapon.transform.SetParent(equipPosition);
        currentWeapon.transform.localEulerAngles = new Vector3(0f, 180f, 0f);

        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = currentWeapon.GetComponent<Collider>();
        if (col != null) col.enabled = false;
    }

    void Drop()
    {
        if (currentWeapon != null)
        {
            currentWeapon.transform.SetParent(null);

            Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            Collider col = currentWeapon.GetComponent<Collider>();
            if (col != null) col.enabled = true;

            currentWeapon = null;
        }
    }
}
