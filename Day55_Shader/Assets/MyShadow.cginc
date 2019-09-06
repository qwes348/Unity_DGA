#if !defined(MY_SHADOW_INCLUDED)
#define MY_SHADOW_INCLUDED

#include "UnityCG.cginc"

struct appdata	// == Vertex Data
{
    float4 position : POSITION;
	float3 normal : NORMAL;
};


float4 vert (appdata v) : SV_POSITION
{
	float4 position = UnityClipSpaceShadowCasterPos(v.position.xyz, v.normal);
	return UnityApplyLinearShadowBias(position);
}


fixed4 frag (v2f i) : SV_TARGET
{
	return 0;
}

#endif
