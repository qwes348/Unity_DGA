using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode] // 에디터모드에서 실행
public class GradientRawImage : MonoBehaviour
{
    public RawImage rawImage;
    public Color color1;
    public Color color2;

    // https://forum.unity.com/threads/how-does-onvalidate-work.616372/
    [SerializeField]        // Warning!! SerializeField를 안넣으면 null오류
    Texture2D backgroundTexture;

    private void Awake()
    {
        backgroundTexture = new Texture2D(1, 2); // (width, height)
        backgroundTexture.wrapMode = TextureWrapMode.Clamp;
        backgroundTexture.filterMode = FilterMode.Bilinear;
        SetColor(color1, color2);
    }

    private void SetColor(Color color1, Color color2)
    {
        backgroundTexture.SetPixels(new Color[] { color2, color1 });  // byte order 거꾸로 넣어야함
        backgroundTexture.Apply();
        rawImage.texture = backgroundTexture;
    }

    private void OnValidate()
    {
        SetColor(color1, color2);
    }
}
