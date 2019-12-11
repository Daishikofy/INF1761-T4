Shader "Custom/firstShader"
{
	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
		
		_MainTex("Texture", 2D) = "white" {}
		_AlphaCutoff("Alpha Cutoff", Range(0, 1)) = 0.5
		_DetailTex("Detail Texture", 2D) = "white" {}

		[NoScaleOffset] _FlowMap("Flow (RG)", 2D) = "black" {}
		_AnimSpeed("AnimSpeed", Range(0, 1)) = 0.5
	}

		SubShader{

			Pass {

		Tags {
				"LightMode" = "ForwardBase"
			}
		Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram
			#pragma shader_feature _OCCLUSION_MAP
			#pragma shader_feature _EMISSION_MAP
			#pragma shader_feature _ _RENDERING_CUTOUT _RENDERING_FADE
			#include "UnityStandardBRDF.cginc"

			float4 _Color;
			sampler2D _MainTex, _DetailTex, _FlowMap;;
			float4 _MainTex_ST, _DetaiTex_ST;
			float _AlphaCutoff;
			float _AnimSpeed;

			struct Interpolators {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;				
			};

			struct MyVertexData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			float2 FlowUV(float2 uv, float2 flowVector, float time) {
				float progress = frac(time*0.2);
				return uv + flowVector * progress * _AnimSpeed;
			}

			Interpolators MyVertexProgram(MyVertexData v)
			{
				Interpolators i;
				i.position = UnityObjectToClipPos(v.position);
				i.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;			
				i.normal = UnityObjectToWorldNormal(v.normal);
			return i;
		}

			float GetAlpha(Interpolators i) {
				return _Color.a * tex2D(_MainTex, i.uv.xy).a;
			}

			float4 MyFragmentProgram(Interpolators i ) : SV_TARGET
		{
			float2 flowVector = tex2D(_FlowMap, i.uv).rg * 2 - 1;
			i.uv = FlowUV(i.uv, flowVector, _Time.y);

			float3 lightDir = _WorldSpaceLightPos0.xyz;
			float4 normal = float4(i.normal * 0.5 + 0.5, 1);
			i.normal = normalize(i.normal);
			float3 light = DotClamped(lightDir, i.normal) * _LightColor0.rgb;

			float4 albedo = tex2D(_MainTex, i.uv) * _Color;
			albedo *= tex2D(_DetailTex, i.uv * 10) * _Color * 2;
			float alpha = GetAlpha(i);
			albedo.a = alpha - _AlphaCutoff;
			clip(alpha - _AlphaCutoff);

			//albedo.rgb = float3(flowVector, 0);

			return albedo * float4(light, 1);
		}
		ENDCG
		}
	}
}