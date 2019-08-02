Shader "Custom/CustomLambert"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap("Normal Map", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf CustomLambert noambient

        // Use shader model 3.0 target, to get nicer looking lighting        

        sampler2D _MainTex;
		sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
        };     

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Alpha = c.a;
        }

		float4 LightingCustomLambert(SurfaceOutput s, float3 lightDir, float atten)
		{
			// return float4(1, 0, 0, 1);

			// Lambert
			//float ndotl = saturate(dot(s.Normal, lightDir));    // saturate 는 clamp01과 비슷함 // max(0, cos(@))
			//return ndotl;

			// Half-Lambert by Valve
			//float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;    // saturate 는 clamp01과 비슷함
			//return pow(ndotl, 3);

			// Lambert full version : Lagacy shader > Bumped Diffuse 와 동일
			/*
			float ndotl = saturate(dot(s.Normal, lightDir));
			float4 final;
			final.rgb = ndotl * s.Albedo * _LightColor0.rgb * atten;
			final.a = s.Alpha;
			return final;
			*/

			// Half-Lambert full version
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;
			float4 final;
			final.rgb = pow(ndotl, 3) * s.Albedo * _LightColor0.rgb * atten;
			final.a = s.Alpha;
			return final;
		}
        ENDCG
    }
    FallBack "Diffuse"
}

// atten
// - self shadow
// - receive shadow
// - attenuation(빛의 감쇠): directional light은 영향이 없음(거리 개념이 없기때문에), Point ligh(가능)
