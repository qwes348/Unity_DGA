Shader "Custom/Hologram2"
{
    Properties
    {
        _BumpMap ("NormalMap", 2D) = "bump" {}
		_HologramColor ("Hologram Color", Color) = (0,1,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

        CGPROGRAM
        #pragma surface surf Nolight noambient alpha:fade   

        sampler2D _BumpMap;
		float4 _HologramColor;

        struct Input
        {
            float2 uv_BumpMap;
			float3 viewDir;		//	viewDir : from vertex to camera,	lightDir : form vertex to light
			float3 worldPos;
        };
        
        void surf (Input IN, inout SurfaceOutput o)
        {            
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			
			o.Emission = _HologramColor;		// 소수부분만 살리고 정수부분은 버리는 frac함수
			float rim = saturate(dot(o.Normal, IN.viewDir));
			rim = pow(1-rim, 3) + pow(frac(IN.worldPos.g * 3 - _Time.y), 30);			
			o.Alpha = rim;			
        }

		float4 LightingNolight (SurfaceOutput s, float3 lightDir, float atten)
		{
			return float4(0,0,0,s.Alpha);
		}
        ENDCG
    }
    FallBack "Transparent/Diffuse"		// No shadow
}


/*
Shader "Custom/Hologram2"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_HologramColor ("Hologram Color", Color) = (0,1,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }

        CGPROGRAM
        #pragma surface surf Lambert noambient alpha:fade   

        sampler2D _MainTex;
		float4 _HologramColor;

        struct Input
        {
            float2 uv_MainTex;
			float3 viewDir;		//	viewDir : from vertex to camera,	lightDir : form vertex to light
			float3 worldPos;
        };
        
        void surf (Input IN, inout SurfaceOutput o)
        {            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);            		
			
			o.Emission = _HologramColor;		// 소수부분만 살리고 정수부분은 버리는 frac함수
			float rim = saturate(dot(o.Normal, IN.viewDir));
			rim = pow(1-rim, 3) + pow(frac(IN.worldPos.g * 3 - _Time.y), 30);			
			o.Alpha = rim;			
        }
        ENDCG
    }
    FallBack "Diffuse"
}
*/
