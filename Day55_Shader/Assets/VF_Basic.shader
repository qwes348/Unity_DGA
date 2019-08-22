
Shader "VF/VF_Basic"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "PreviewType" = "Plane"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata	// struct == 구조체
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f	// vertex to frag
            {
                float2 uv : TEXCOORD0;                
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;		// ST: Scale and Translation => TO(Tiling and Offset)

            v2f vert (appdata v)
            {
                v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);	// or mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);		// 대문자로된 함수 = 매크로, 이경우에는 코드를 아래와 같이풀어주는 기능 전처리기라고함
				//o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;		// S = Tiling, T = Offset
                return o;
            }

            fixed4 frag (v2f i) : SV_Target		// Target = FrameBuffer
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);               
                return col;
            }
            ENDCG
        }
    }
}
