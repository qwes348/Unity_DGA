Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex ("NoiseTex", 2D) = "white" {}
		_Cut ("Alpha Cut", Range(0, 1)) = 0.1
		_OutlineColor ("Outline Color", Color) = (0, 0, 0, 0)
		_OutlineThickness ("Outline Thickness", Range(0, 1)) = 0.2
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
		sampler2D _NoiseTex;
		float _Cut;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_NoiseTex;
        };        

        void surf (Input IN, inout SurfaceOutput o)
        {            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 noise = tex2D (_NoiseTex, IN.uv_NoiseTex);
            o.Albedo = c.rgb;
			float alpha;
			if(noise.r >= _Cut)
			{
				alpha = 1;
			}
			else
			{
				alpha = 0;
			}

			float outline;
			if(noise.r >= _Cut * 1.2)
			{
				outline = 0;
			}
			else
			{
				outline = 1;
			}
			o.Emission = outline * float3(1, 0, 0);
            o.Alpha = alpha;
        }
        ENDCG		
    }
    FallBack "Diffuse"
}
