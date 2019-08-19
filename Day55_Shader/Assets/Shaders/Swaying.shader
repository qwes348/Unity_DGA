Shader "Custom/Swaying"
{
    Properties
    {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert vertex:vert addshadow

        sampler2D _MainTex;

		void vert(inout appdata_full v)
		{
			v.vertex.y += sin(_Time.y * 2) * 0.02 * v.color.r;
		}

        struct Input
        {
            float2 uv_MainTex;
			float4 color:COLOR;
        };


        void surf (Input IN, inout SurfaceOutput o)
        {            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
			//o.Emission = IN.color.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
