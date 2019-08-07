Shader "Custom/Toon2Pass"
{
    Properties
    {       
        _MainTex ("Albedo (RGB)", 2D) = "white" {}    
		_BumpMap ("NormalMap", 2D) = "bump" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		//cull back		// 뒷면을 안그림 == default
		cull front	// 뒷면만 그림
		// 1st Pass  // outline전용
        CGPROGRAM
        #pragma surface surf Nolight vertex:vert noambient noshadow //addshadow : 바뀐 vert기준으로 그림자 생성

        sampler2D _MainTex;

		void vert(inout appdata_full v)
		{
			//v.vertex.xyz += v.normal.xyz * 0.01 * sin(_Time.y);
			v.vertex.xyz += v.normal.xyz * 0.002;  // vertex사이즈를 0.2% 늘림
		}

        struct Input
        {
			float4 color:COLOR;		// dummy
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
			
        }

		float4 LightingNolight(SurfaceOutput s, float3 lightDir, float atten)
		{
			return float4(0,0,0,1);
		}
        ENDCG


		cull back  // 앞면만그림
		// 2nd Pass		
		CGPROGRAM
        #pragma surface surf Toon noambient

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
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }

		float4 LightingToon(SurfaceOutput s, float3 lightDir, float atten)
		{
			float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

			// 음영을 디지털식으로 3단계로만 나눔 기준은 ndotl
			if(ndotl > 0.7)
			{
				ndotl = 1;
			}
			else// if(ndotl > 0.4)
			{
				ndotl = 0.3;
			}
			/*else
			{
				ndotl = 0;
			}*/
			
			/*
			ndotl = ndotl * 5;
			ndotl = ceil(ndotl) / 5;  // ceil 소숫점을 올림해줌   // 0.2, 0.4, 0.6, 0.8, 1 이렇게 딱 5개만나옴
			*/
			float4 final;
			final.rgb = s.Albedo * ndotl * _LightColor0.rgb;
			final.a = s.Alpha;

			return final;
		}

		ENDCG
    }
    FallBack "Diffuse"
}
