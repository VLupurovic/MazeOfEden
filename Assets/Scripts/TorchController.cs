using UnityEngine;

public class TorchController : MonoBehaviour
{
    public GameObject torchLight; // torch light - dete playera

    private bool hasTorch = false;
    private bool torchOn = false;

    void Start()
    {
        if (torchLight != null)
            torchLight.SetActive(false);  // na početku torch svetlo isključeno
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
