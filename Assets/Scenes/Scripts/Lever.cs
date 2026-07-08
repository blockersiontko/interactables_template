using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    private MeshRenderer rend;

    public void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public void Interact()
    {
        Debug.Log("Jajeczko");
        var material = rend.material;

        rend.material.SetColor("_BaseColor", Color.red);
        rend.material.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(2f));

        rend.material.EnableKeyword("_EMISSION");
    }
}