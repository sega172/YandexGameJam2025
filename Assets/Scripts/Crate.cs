using UnityEngine;

public class Crate : MonoBehaviour, IInteractable
{
    public void Interact(Controller controller)
    {
        controller.Raise(gameObject);
        GetComponent<Collider2D>().enabled = false;
    }
}
