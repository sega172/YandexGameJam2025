using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(Collider2D))]
public class ActionableCollector : MonoBehaviour
{

    List<GameObject> interactables = new();

    /// <summary>
    /// может вернуть null
    /// </summary>
    public GameObject NearestGO => GetNearestGO();

    /// <summary>
    /// может вернуть null
    /// </summary>
    public IInteractable NearestInteractable => GetNearestInteractable();

    private void OnTriggerEnter2D(Collider2D collision) => AddOrSkip(collision.gameObject);
    private void OnTriggerExit2D(Collider2D collision) => RemoveOrSkip(collision.gameObject);

    private void AddOrSkip(GameObject go)
    {
        IInteractable interactable = go.GetComponent<IInteractable>();
        if (interactable != null)
            interactables.Add(go);
    }

    private void RemoveOrSkip(GameObject go) 
    {
        IInteractable interactable = go.GetComponent<IInteractable>();
        if (interactable != null)
            interactables.Remove(go);
    }

    /// <summary>
    /// может вернуть null
    /// </summary>
    /// <returns>ближайший к игроку интерактивный объект</returns>
    private GameObject GetNearestGO()
    {
        float minSqrDistance = float.MaxValue;
        GameObject nearest = null;
        foreach (GameObject interactabe in interactables)
        {
            float sqrDistance = (gameObject.transform.position - interactabe.transform.position).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                nearest = interactabe;
            }
        }
        return nearest;
    }

    private IInteractable GetNearestInteractable()
    {
        float minSqrDistance = float.MaxValue;
        IInteractable nearest = null;
        foreach (GameObject interactabe in interactables)
        {
            if (interactabe == null)
            {
                interactables.Remove(interactabe);
                continue;
            }

            float sqrDistance = (gameObject.transform.position - interactabe.transform.position).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                nearest = interactabe.GetComponent<IInteractable>();
            }
        }
        return nearest;
    }



}
