using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public float runMaxSpeed = 2;
    public float runAcceleration = 2;
    public Transform HoldPoint;

    public ActionableCollector collector;
    protected abstract Vector2 MoveVector { get; }

    public abstract void Raise(GameObject go);
    public abstract void Drop(GameObject go);
    public abstract void Throw(GameObject go);
    
    public abstract void Use();
}
