Shader "VF/VF_GenerateNormal"
{
    Properties
    {
		_Tint ("TInt", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
		[NoScaleOffset]_HeightMap ("HeightsMap", 2D) = "gray" {}
		_Metalic ("Metalic", Range(0, 1)) = 0
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

			#include "UnityPBSLighting.cginc"

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
			float _Metalic;
			float _Smoothness;

			sampler2D _HeightMap;
			float4 _HeightMap_TexelSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);		// 월드포지션을 구하는부분
				o.normal = UnityObjectToWorldNormal(v.normal);			// Normal도 월드포지션으로 바꿔야함 안그러면 문제발생
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

			void InitializeFragmentNormal(inout v2f i)
			{
				/*// Finite Difference
				float2 delta = float2(_HeightMap_TexelSize.x, 0);				
				float h1 = tex2D(_HeightMap, i.uv);
				float h2 = tex2D(_HeightMap, i.uv + delta);
				*/
				
				// Central Difference
				float2 du = float2(_HeightMap_TexelSize.x * 0.5, 0);				
				float u1 = tex2D(_HeightMap, i.uv - du);
				float u2 = tex2D(_HeightMap, i.uv + du);
				// float3 tu = float3(1, u2-u1, 0);

				float2 dv = float2(0, _HeightMap_TexelSize.y * 0.5);				
				float v1 = tex2D(_HeightMap, i.uv - dv);
				float v2 = tex2D(_HeightMap, i.uv + dv);
				// float3 tv = float3(0, v2-v1, 1);
				
				// i.normal = cross(tv, tu);
				i.normal = float3(u1-u2, 0.5, v1-v2);
				i.normal = normalize(i.normal);				
			}

            fixed4 frag (v2f i) : SV_Target
            {
				InitializeFragmentNormal(i);
				
				// Directional lights: (world space Dir, 0), Other lights: (world space pos, 1)
				// Directional lights는 위치상관x 방향중요 Other lights는 pos가 중요
				// 뒤에 0이오면 방향으로 인지, 1이오면 포지션으로 인지
				float3 lightDir = _WorldSpaceLightPos0.xyz;
				float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
				float3 lightColor = _LightColor0.rgb;
				float3 albedo = tex2D(_MainTex, i.uv).rgb * _Tint.rgb;
				
				float3 specularTint;
				float oneMinusReflectivity;
				albedo = DiffuseAndSpecularFromMetallic(albedo, _Metalic, specularTint, oneMinusReflectivity);
				
				UnityLight light;
				light.color = lightColor;
				light.dir = lightDir;
				light.ndotl = DotClamped(i.normal, lightDir);

				UnityIndirect indirectLight;
				indirectLight.diffuse = 0;
				indirectLight.specular = 0;

                return UNITY_BRDF_PBS(
					albedo, specularTint,
					oneMinusReflectivity, _Smoothness,
					i.normal, viewDir,
					light, indirectLight
				);
            }
            ENDCG
        }
    }
}
