using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{

    [SerializeField] private float interactRange = 3f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InteractWithWorld();
        }
    }

    private void InteractWithWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            Debug.Log("Hit: " + hit.collider.name);
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}