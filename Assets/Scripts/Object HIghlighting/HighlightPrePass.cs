using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class HighlightPrePass : MonoBehaviour
{
    private RenderTexture prePass; //A Texture that holds the all highlighted objects that is filtered to black
    private RenderTexture blurred; //Same Texture as the PrePass except has a blurred material applied to it
  
    private Material blurMat; //The Blur Material

    private void Start()
    {
        //Creates the Render Textures
        prePass = new RenderTexture(Screen.width, Screen.height, 24);
        prePass.antiAliasing = QualitySettings.antiAliasing;
        blurred = new RenderTexture(Screen.width >> 1, Screen.height >> 1, 0); //Screen.Width >> 1 & Screen.Height >> 1 used to half the resolution

        //Applies a Shader to all Objects that are meant to be highlighted 
        Camera camera = GetComponent<Camera>();
        Shader glowShader = Shader.Find("Hidden/HighlightReplace");
        camera.targetTexture = prePass;
        camera.SetReplacementShader(glowShader, "Glowable");

        //Sets the two Textures within the final shader that will combine them together to create the end product
        Shader.SetGlobalTexture("_GlowPrePassTex", prePass);
        Shader.SetGlobalTexture("_GlowBlurredTex", blurred);

        //Creates the Blur Material
        blurMat = new Material(Shader.Find("Hidden/Blur"));
        blurMat.SetVector("_BlurSize", new Vector2(blurred.texelSize.x * 1.5f, blurred.texelSize.y * 1.5f));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);

        Graphics.SetRenderTarget(blurred);
        GL.Clear(false, true, Color.clear);

        Graphics.Blit(source, blurred);

        //Improves the Blur by repeatedly applying it over 4 times 
        for (int i = 0; i < 4; i++)
        {
            RenderTexture temp = RenderTexture.GetTemporary(blurred.width, blurred.height);
            Graphics.Blit(blurred, temp, blurMat, 0);
            Graphics.Blit(temp, blurred, blurMat, 1);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
