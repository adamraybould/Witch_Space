using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HighlightComposite : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] private float intensity = 2;

    private Material compositeMat;

    private void Start()
    {
        compositeMat = new Material(Shader.Find("Hidden/HighlightPrePass"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
        if (compositeMat != null)
        {
            compositeMat.SetFloat("_Intensity", intensity);
            Graphics.Blit(source, destination, compositeMat, 0);
        }
    }
}
