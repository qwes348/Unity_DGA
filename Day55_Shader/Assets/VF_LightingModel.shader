Shader "VF/VF_LightingModel"
{
    Properties
    {
		_Tint ("TInt", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
		_SpecularTint ("Specular_Tint", Color) = (1,1,1,1)
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
			Tags { 
					"LightMode"="ForwardBase"	// _WorldSpaceLightPos0, _LightColor0 사용가능해짐
					}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma target 3.0

			#include "UnityStandardBRDF.cginc"
			#include "UnityStandardUtils.cginc"

            struct appdata	// == Vertex Data
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float3 normal : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _Tint;
			float4 _SpecularTint;
			float _Smoothness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);		// 월드포지션을 구하는부분
				o.normal = UnityObjectToWorldNormal(v.normal);			// Normal도 월드포지션으로 바꿔야함 안그러면 문제발생
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				i.normal = normalize(i.normal);			// Interpolation
				// Directional lights: (world space Dir, 0), Other lights: (world space pos, 1)
				// Directional lights는 위치상관x 방향중요 Other lights는 pos가 중요
				// 뒤에 0이오면 방향으로 인지, 1이오면 포지션으로 인지
				float3 lightDir = _WorldSpaceLightPos0.xyz;
				float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
				float3 lightColor = _LightColor0.rgb;
				float3 albedo = tex2D(_MainTex, i.uv).rgb * _Tint.rgb;

				float oneMinusReflectivity;
				albedo = EnergyConservationBetweenDiffuseAndSpecular(albedo, _SpecularTint.rgb, oneMinusReflectivity);

				float3 diffuse = albedo * lightColor * DotClamped(lightDir, i.normal);

				// Blinn-Phong model
				float3 halfVector = normalize(lightDir + viewDir);
				float3 specular = _SpecularTint * lightColor * pow(DotClamped(halfVector, i.normal), _Smoothness * 100);

				// Phong model
				//float3 reflectionDir = reflect(-lightDir, i.normal);
				//float3 specular = _SpecularTint * lightColor * pow(DotClamped(viewDir, reflectionDir), _Smoothness * 100);

                return fixed4(diffuse + specular, 1);
            }
            ENDCG
        }
    }
}
