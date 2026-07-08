using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private MeshRenderer rend;
    [SerializeField] private int cRed, cGreen, cBlue;
    public Color32 colorful;

    public void Awake()
    {
        rend = GetComponent<MeshRenderer>();
    }

    public void Interact()
    {
        cRed = Random.Range(0, 255);
        cGreen = Random.Range(0, 255);
        cBlue = Random.Range(0, 255);


        colorful = new Color32((byte)cRed, (byte)cGreen, (byte)cBlue, 255);
        rend.material.SetColor("_BaseColor", colorful);
    }
}