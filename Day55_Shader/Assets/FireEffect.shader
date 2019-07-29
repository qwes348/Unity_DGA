Shader "Custom/FireEffect"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MainTex2 ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		// 여기서 Queue란 그리는 순서를 얘기하는것이며 Transparent 큐가 제일 나중에 그려진다
        LOD 200

        CGPROGRAM        
        #pragma surface surf Standard alpha:fade
		// #pragma == 컴파일러에게 알려주는(지시하는) 일을함 

        sampler2D _MainTex;
		sampler2D _MainTex2;

        struct Input
        {
            float2 uv_MainTex;	// float2 == 2d float 벡터
			float2 uv_MainTex2;
        };

        //half _Glossiness;	// float의 절반 바이트 즉 2바이트        
        //fixed4 _Color;		// fixed == half의 반 즉 1바이트

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			fixed4 d = tex2D (_MainTex2, float2(IN.uv_MainTex2.x, IN.uv_MainTex2.y - _Time.y)); // offset값을 계속 줄여줘서 불이 일렁이는효과 구현
            o.Albedo = c.rgb + d.rgb;
            o.Alpha = c.a * d.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
