Shader "Custom/AlphaTesting"
{
    Properties
    {
		_Color("Main Color", Color) = (1, 1, 1, 1)	// for shadow
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType" = "TransparentCutout" "Queue" = "AlphaTest" }			

        CGPROGRAM
        #pragma surface surf Lambert alphatest:_Cutoff

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
    FallBack "Legacy Shaders/Transparent/Cutout/VertexLit"			// no shadow
}

// 정리
// 1. opaque objects 먼저 그린다 : Z-buffering
// 2. Transparent objects를 뒤에서부터 그린다 : AlphaSorting
// 3. Transparent objects는 zwrite off 하고 그린다.
