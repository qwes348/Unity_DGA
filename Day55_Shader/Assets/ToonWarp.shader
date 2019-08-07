﻿Shader "Custom/ToonWarp"
{
    Properties
    {       
        _MainTex ("Albedo (RGB)", 2D) = "white" {}    
		_BumpMap ("NormalMap", 2D) = "bump" {}
		_RampTex ("RampTex", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }	

		CGPROGRAM
        #pragma surface surf Warp noambient

        sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _RampTex;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;			
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

		float4 LightingWarp(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			float4 final;

			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5; // Half Lambert
			float4 ramp = tex2D(_RampTex, float2(ndotl, 0.5));  // _RampTex의 x축 ndotl, y축 50% 위치의 색 사실 y축은 상관없음

			float rim = abs(dot(s.Normal, viewDir));  // outLine
			if(rim > 0.2)
			{
				rim = 1;
			}
			else
			{
				rim = -1;
			}
			/*
			final.rgb = s.Albedo * ramp * _LightColor0.rgb * rim;
			final.a = s.Alpha;
			*/
			
			return ramp * rim;			
		}

		ENDCG
    }
    FallBack "Diffuse"
}
