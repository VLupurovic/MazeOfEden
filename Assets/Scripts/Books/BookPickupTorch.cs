using UnityEngine;

public class BookPickupTorch : BookPickupBase
{

    public BookVisibility bookVisibility;

    protected override bool canBePicked()
    {
        Debug.Log("Entering torch");
        if (!base.canBePicked())
            return false;

        Debug.Log($"Checking visibility {bookVisibility.IsVisible()}");
        if (bookVisibility != null && bookVisibility.IsVisible())
        {
            Debug.Log("Can be pickd up");
            return true;

        }
        else
        {
            Debug.Log("Cannot be picked up");
            return false;

        }
    }
}
