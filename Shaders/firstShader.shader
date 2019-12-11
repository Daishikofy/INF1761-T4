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
			struct Interpolators {
				float4 position : SV_POSITION;
				//float3 localPosition : TEXCOORD0;
				float2 uv : TEXCOORD0;
			};

			struct MyVertexData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			Interpolators MyVertexProgram(MyVertexData v)
			{
				Interpolators i;
				//i.localPosition = v.position.xyz;
				i.position = UnityObjectToClipPos(v.position);
				i.uv = v.uv;
			return i;
		}

			float4 MyFragmentProgram(Interpolators i ) : SV_TARGET
		{
			//i.localPosition += 0.5;
			return  float4(i.uv, 1,1);
		}
		ENDCG
		}
	}
}