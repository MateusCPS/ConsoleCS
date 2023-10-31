using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Render1 : MonoBehaviour
{
    public ComputeShader cs;
    public int textureResolution = 128;


    RenderTexture renderTexture;
    public Vector4 cube;

    int kernel;

    Vector2 halfSize;
    // Start is called before the first frame update
    void Start()
    {
        halfSize = new Vector2(textureResolution/2, textureResolution/2);
        // largura, altura e profundidade
        renderTexture = new RenderTexture(textureResolution, textureResolution, 16);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        kernel = cs.FindKernel("CSMain");
        cs.SetTexture(kernel, "Result", renderTexture);
        cs.SetInt("resolution", textureResolution);
        cs.SetVector("cube", cube);
        

        this.GetComponent<Renderer>().material.SetTexture("_MainTex", renderTexture);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            cs.Dispatch(kernel, textureResolution/16, textureResolution/16, 1);
    }

}
