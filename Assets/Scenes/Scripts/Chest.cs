using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour, IInteractable
{

    private bool _Slopidelko = false;

    public void Interact()
    {
        if (!_Slopidelko)
        {
            transform.Rotate(90, 0, 0);
            transform.position += new Vector3(0, 0.4f, 0.5f);
            _Slopidelko = true;
        }
    }
}
