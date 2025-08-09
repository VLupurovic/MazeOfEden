using UnityEngine;

public class TorchController : MonoBehaviour
{
    public GameObject torchLight; // torch light - players child

    private bool hasTorch = false;
    private bool torchOn = false;

    void Start()
    {
        if (torchLight != null)
            torchLight.SetActive(false);  // torch light off at the start
    }

    void Update()
    {
        if (hasTorch && Input.GetKeyDown(KeyCode.T))
        {
            torchOn = !torchOn;
            if (torchLight != null)
                torchLight.SetActive(torchOn);
        }
    }

    public void PickupTorch()
    {
        hasTorch = true;
        Debug.Log("Torch picked up!");
    }

    public bool IsTorchOn => torchOn;

}
