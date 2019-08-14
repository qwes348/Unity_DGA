Shader "Custom/2PassAlphaBlending"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

		// 1st Pass
		zwrite on
		ColorMask 0		// default is RGBA

        CGPROGRAM        
        #pragma surface surf Nolight noambient noforwardadd nolightmap novertexlights noshadow
        

        struct Input
        {
            float4 color:COLOR;		// dummy
        };        

        void surf (Input IN, inout SurfaceOutput o)
        {            
        }

		float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten)
		{
			return float4(0, 0, 0, 0);
		}
        ENDCG		
		// 2nd Pass
		zwrite off
		// ZTest Less		// default is LEqual, Z Test 통과(즉 그리는)조건: 거리가 같거나 작을 경우 그린다.

		CGPROGRAM        
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };        

        void surf (Input IN, inout SurfaceOutput o)
        {            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = 0.3;
        }
        ENDCG		
    }
    FallBack "Diffuse"
}
