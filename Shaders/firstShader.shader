// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/firstShader"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
	}

		SubShader{

			Pass {
			CGPROGRAM
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram
			#include "UnityCG.cginc"

			float4 _Color;

		float4 MyVertexProgram(float4 position : POSITION
				         , out float3 localPosition : TEXCOORD) : SV_POSITION
		{
			localPosition = position.xyz;
			return UnityObjectToClipPos(position);
		}

			float4 MyFragmentProgram(float4 position : SV_POSITION
				, float3 localPosition : TEXCOORD0) : SV_TARGET
		{
			localPosition += 0.5;
			return  float4(localPosition,1);
		}
		ENDCG
		}
	}
}