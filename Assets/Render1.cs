using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render1 : MonoBehaviour
{
    public ComputeShader cs;
    public int textureResolution = 128;


    RenderTexture renderTexture;

    int kernel;
    // Start is called before the first frame update
    void Start()
    {
        // largura, altura e profundidade
        renderTexture = new RenderTexture(textureResolution, textureResolution, 16);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        kernel = cs.FindKernel("CSMain");
        cs.SetTexture(kernel, "Result", renderTexture);
        cs.SetInt("resolution", textureResolution);

        this.GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            cs.Dispatch(kernel, textureResolution/8, textureResolution/8, 1);
    }
}
