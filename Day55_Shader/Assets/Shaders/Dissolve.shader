Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NoiseTex ("NoiseTex", 2D) = "white" {}
		_Cut ("Alpha Cut", Range(0, 1)) = 0.1
		[HDR] _OutColor("OutlineColor", Color) = (1,1,1,1)
		_OutThickness("Outline Thickness", Range(1, 1.5)) = 1.15
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
		//ZTest Less		// default is LEqual, Z Test 통과(즉 그리는)조건: 거리가 같거나 작을 경우 그린다.

		CGPROGRAM        
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;
		sampler2D _NoiseTex;
		float _Cut;
		float4 _OutColor;
		float _OutThickness;

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
			if(noise.r >= _Cut * _OutThickness)
			{
				outline = 0;
			}
			else
			{
				outline = 1;
			}
			//o.Emission = float3(IN.uv_MainTex, 0); 
			o.Emission = outline * _OutColor.rgb;
            o.Alpha = alpha;
        }
        ENDCG		
    }
    FallBack "Diffuse"
}
