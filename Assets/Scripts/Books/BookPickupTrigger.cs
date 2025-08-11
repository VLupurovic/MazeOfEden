using UnityEngine;

public class BookPickupDoor: BookPickupBase
{
    public TriggerDoorController doorController;

    protected override void pickUp()
    {
        base.pickUp();
        doorController.OnBookCollected();
    }
}
