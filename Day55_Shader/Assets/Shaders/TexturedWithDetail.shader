Shader "VF/TexturedWithDetail"
{
    Properties
    {
		_Tint ("TInt", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
		_DetailTex ("Detail Texture", 2D) = "gray" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float2 uvDetail : TEXCOORD1;
            };

            sampler2D _MainTex, _DetailTex;
            float4 _MainTex_ST, _DetailTex_ST;
			float4 _Tint;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uvDetail = TRANSFORM_TEX(v.uv, _DetailTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv) * _Tint;
				color *= tex2D(_DetailTex, i.uvDetail) * unity_ColorSpaceDouble;	// GammaSpace: 2 , LinearSpace: 4.59
                return color;
            }
            ENDCG
        }
    }
}

/*
TextureFile(sRGB)	=>		Shader		=>		Rendering(Display)
	Gamma			=>		Gamma		=>		Gamma
							0.5 * 2 = 1

	Gamma			=>		Linear		=>		Gamma
							0.5^2.2 = 0.217 * 2 = 0.44
								=> 0.217 * a = 1
								=> a = 4.59		// == ColorSpaceDouble

								4.59유도식
							0.5^2.2 * (1 / 0.5^2.2) => 1
*/