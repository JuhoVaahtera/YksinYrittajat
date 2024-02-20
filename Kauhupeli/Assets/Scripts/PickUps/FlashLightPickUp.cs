using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightPickUp : MonoBehaviour
{
    public GameObject PickUpText;
    public GameObject PlayerHand;
    void Start()
    {
        PlayerHand.SetActive(false);
        PickUpText.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {

            PickUpText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E)) 
            {
                Destroy(this.gameObject);

                PlayerHand.SetActive(true);

                PickUpText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpText.SetActive(false);
    }
}
