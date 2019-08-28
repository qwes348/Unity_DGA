Shader "VF/MultipleLights"
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

        Pass
        {
			Tags { 
					"LightMode"="ForwardBase"	
					}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma target 3.0

			#include "MyLighting.cginc"


            ENDCG
        }

		Pass	// 2pass
        {
			Tags { 
					"LightMode"="ForwardAdd"	// LightMode를 바꿔줘서 두번째라이트도 인식
				}
			Blend One One		// 두번째 라이트를 블렌드
			zwrite off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma target 3.0

			#define POINT

			#include "MyLighting.cginc"


            ENDCG
        }
    }
}
