Shader "VF/VertexLights"
{
    Properties
    {
		_Tint ("TInt", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
		_Metalic ("Metalic", Range(0, 1)) = 0
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass    // Directional Light Pass
        {
			Tags { 
					"LightMode"="ForwardBase"	
					}

            CGPROGRAM
            #pragma target 3.0

            #pragma multi_compile _ VERTEXLIGHT_ON

            #pragma vertex vert
            #pragma fragment frag

			#include "MyVertexLighting.cginc"


            ENDCG
        }
        // /*
		Pass	// PointLight Pass
        {
			Tags { 
					"LightMode"="ForwardAdd"	// LightMode를 바꿔줘서 두번째라이트도 인식
				}
			Blend One One		// 두번째 라이트를 블렌드
			zwrite off

            CGPROGRAM
            #pragma target 3.0

            //#pragma multi_compile DIRECTIONAL DIRECTIONAL_COOKIE POINT POINT_COOKIE SPOT     // 두번째 Directional도 Point로 인식하는문제 해결
            #pragma multi_compile_fwdadd

            #pragma vertex vert
            #pragma fragment frag			

			// #define POINT

			#include "MyVertexLighting.cginc"


            ENDCG
        }
        // */
    }
}
