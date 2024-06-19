using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SelectableObject : MonoBehaviour
{
    private Renderer[] renderers;
    private List<Material> materials = new List<Material>(); //A List of Materials of the attached object to apply the highlight too

    [Header("Selectable Parameters")]
    [SerializeField] private Color glowColor = new Color(255, 116, 0, 255); //The Highlight Color of the attached Object
    [SerializeField] private float lerpFactor = 10.0f; //The lerping speed at which the highlight will appear

    private Color currentColor;
    private Color targetColor; //Either GlowColor or Black

    [SerializeField] protected float selectableDistance = 3; //At what distance the highlighting and selectability will appear 
    [SerializeField] protected bool withinDistance;
    [SerializeField] protected bool shouldSelect = true; //If the player can select the object
    protected Transform player;

    protected virtual void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            materials.AddRange(renderer.materials);
        }

        player = GameObject.Find("First Person Character").transform;
    }

    protected void OnMouseOver()
    {
        //Is the Player within distance? No? then render to Black (Invisible Highlighting). Else render the glow color
        if (withinDistance)
        {   
            targetColor = glowColor;

            //If E is pressed, Run the Select Function which is overidden by needed classes
            if (Input.GetKeyDown(KeyCode.E))
                Select();
        }
        else
        {
            targetColor = Color.black;
        }
    }

    protected void OnMouseExit()
    {
        targetColor = Color.black;
    }

    //Overidden by inheriting classes to specify what happens when a selectable object is activated
    protected virtual void Select()
    { 
    }

    protected virtual void Update()
    {
        withinDistance = false;
        if (shouldSelect)
        {
            Vector3 distance = player.position - transform.position;
            if (distance.magnitude <= selectableDistance)
                withinDistance = true;
        }

        currentColor = Color.Lerp(currentColor, targetColor, Time.deltaTime * lerpFactor);

        for (int i = 0; i < materials.Count; i++)
        {
            materials[i].SetColor("_GlowColor", currentColor);
        }
    }

    public bool IsWithinDistance() { return withinDistance; }

    public void EnableSelection() { shouldSelect = true; }
    public void DisableSelection() { shouldSelect = false; }
}
