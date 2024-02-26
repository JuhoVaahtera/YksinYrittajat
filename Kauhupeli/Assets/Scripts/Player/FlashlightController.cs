using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;

    private AudioSource toggleSound;

    private void Start()
    {
        flashlight = GetComponentInChildren<Light>();
        toggleSound = GetComponent<AudioSource>();
        flashlight.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            toggleSound.Play();
        }
    }
}
