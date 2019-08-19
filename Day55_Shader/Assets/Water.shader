Shader "Custom/Water"
{
    Properties
    {        
        _Bumpmap ("NormalMap", 2D) = "bump" {}       
		_Cube ("Cube", Cube) = "" {}
		_SPColor ("Specular Color", Color) = (1,1,1,1)
		_SPPower ("Specular Power", Range(50, 300)) = 150
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }        

        CGPROGRAM
        
        #pragma surface surf WaterSpecular alpha:fade vertex:vert 

        sampler2D _Bumpmap;
		samplerCUBE	_Cube;
		float4 _SPColor;
		float _SPPower;

		void vert(inout appdata_full v)
		{
			float movement;
			movement = sin(abs((v.texcoord.x*2-1) * 12) + _Time.y) * 0.1;	// x*2-1 => 0~1사이값을 -1~1사이값으로 바꾸기
			movement += sin(abs((v.texcoord.y*2-1) * 12) + _Time.y) * 0.1;	//	각각 상수는 Length, Timing, Height 파라미터로 빼면좋음

			v.vertex.y += movement / 2;
		}

        struct Input
        {
            float2 uv_Bumpmap;
			float3 worldRefl;
			float3 viewDir;
			INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            float3 normal1 = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap + _Time.x * 0.1));
			float3 normal2 = UnpackNormal(tex2D(_Bumpmap, IN.uv_Bumpmap - _Time.x * 0.1));
            o.Normal = (normal1 + normal2) / 2;
			float3 refColor = texCUBE(_Cube, WorldReflectionVector(IN, o.Normal));

			// rim term
			float rim = saturate(dot(o.Normal, IN.viewDir));
			rim = pow(1-rim, 1.5);

            o.Emission = 2 * rim * refColor;
            o.Alpha = saturate(rim+0.5);			
        }

		float4 LightingWaterSpecular(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten)
		{
			// specular term
			float3 H = normalize(lightDir + viewDir);
			float3 spec = saturate(dot(H, s.Normal));
			spec = pow(spec, _SPPower);

			float4 finalColor;
			finalColor.rgb = spec * _SPColor * 3;
			finalColor.a = s.Alpha + spec;
			return finalColor;
		}

        ENDCG
    }
    FallBack "Legacy Shaders/Transparent/Vertexlit"		// NoShadow
}
