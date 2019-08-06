Shader "Custom/CustomBlinnPhong"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
		_GlossTex ("Gloss Tex", 2D) = "white" {}
		_SpecCol ("specular Color", Color) = (1, 1, 1, 1)
		_SpecPow ("Specular Power", Range(10, 200)) = 100
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf CustomBlinnPhong noambient

        // Use shader model 3.0 target, to get nicer looking lighting        

        sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _GlossTex;
		float4 _SpecCol;
		float _SpecPow;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_GlossTex;
        };     

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 m = tex2D (_GlossTex, IN.uv_GlossTex);
            o.Albedo = c.rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Gloss = m.a;
            o.Alpha = c.a;
        }

		float4 LightingCustomBlinnPhong(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			float4 final;

			// Lambert term: Diffuse
			float3 diffuseColor;
			float ndotl = saturate(dot(s.Normal, lightDir));			
			diffuseColor = ndotl * s.Albedo * _LightColor0.rgb * atten;
			
			// Specular term
			float3 specularColor;
			float3 H = normalize(lightDir + viewDir);  // 두벡터의 중간벡터를 구함
			float spec = saturate(dot(H, s.Normal));
			//spec = pow(spec, 100);
			spec = pow(spec, _SpecPow);
			specularColor = spec * _SpecCol.rgb * s.Gloss;
			final.rgb = diffuseColor.rgb + specularColor.rgb;
			final.a = s.Alpha;

			return final;
		}
        ENDCG
    }
    FallBack "Diffuse"
}
