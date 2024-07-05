/*using UnityEngine;

public class ElevatorInteraction : MonoBehaviour, Player.IInteractable
{
    public ElevatorControl elevator;

    private void Start()
    {
        elevator = GetComponent<ElevatorControl>();
        if (elevator == null)
        {
            Debug.LogError("No ElevatorControl script found on the elevator GameObject.");
        }
    }

    public void Interact()
    {
        if (elevator != null)
        {
            elevator.Interact(); // Trigger elevator movement when interacted with
        }
        else
        {
            Debug.LogWarning("ElevatorControl script is not assigned.");
        }
    }
}*/
