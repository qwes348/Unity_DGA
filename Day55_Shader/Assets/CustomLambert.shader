Shader "Custom/CustomLambert"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
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

		float4 LightingCustomLambert(SurfaceOutput s, float3 lightDir, float atten)
		{
			// return float4(1, 0, 0, 1);

			// Lambert
			//float ndotl = saturate(dot(s.Normal, lightDir));    // saturate 는 clamp01과 비슷함
			//return ndotl;

			// Half-Lambert by Valve
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;    // saturate 는 clamp01과 비슷함
			return pow(ndotl, 3);
		}
        ENDCG
    }
    FallBack "Diffuse"
}
