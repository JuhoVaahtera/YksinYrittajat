using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPickUp : MonoBehaviour
{
    public GameObject PickUpText;
    public GameObject PlayerHand;

    private bool isInRange = false;

    void Start()
    {
        PlayerHand.SetActive(false);
        PickUpText.SetActive(false);
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            PickUpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            PickUpText.SetActive(false);
        }
    }

    private void PickUpItem()
    {
        Destroy(gameObject);
        PlayerHand.SetActive(true);
        PickUpText.SetActive(false);
    }
}