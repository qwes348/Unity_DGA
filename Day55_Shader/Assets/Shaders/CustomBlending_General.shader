Shader "Custom/CustomBlending_General"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend("SrcBled Mode", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DstBlend("DstBled Mode", Float) = 10
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }	// opaque 보다 뒤에그린다는 의미
		zwrite off
		Blend[_SrcBlend] [_DstBlend]
		// (A) * Source + (B) * Destination(Back buffer)
		// Source : alpha object
		// Destination(=Back buffer)

        CGPROGRAM
        #pragma surface surf Lambert keepalpha

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Legacy Shaders/Transparent/VertexLit"			// no shadow
}


// 정리
// 1. opaque objects 먼저 그린다 : Z-buffering
// 2. Transparent objects를 뒤에서부터 그린다 : AlphaSorting
// 3. Transparent objects는 zwrite off 하고 그린다.
