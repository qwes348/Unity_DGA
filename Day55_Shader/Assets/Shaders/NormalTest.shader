Shader "Custom/NormalTest"
{
    Properties
    {        
		_BumpMap ("NormalMap", 2D) = "bump" {}		
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert noambient
        
		sampler2D _BumpMap;		

        struct Input
        {            		
			float2 uv_BumpMap;
			float3 worldNormal;
			INTERNAL_DATA
        };
		

        void surf (Input IN, inout SurfaceOutput o)
        {   
			// in tangent space
			//o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			//o.Emission = o.Normal;



			// in world space
			//o.Emission = IN.worldNormal;


			// in world space with NormalMap
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			float3 worldNormal = WorldNormalVector(IN, o.Normal);
			o.Emission = worldNormal;


            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
