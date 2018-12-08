// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Player" 
{
	//-----プレイヤーが設定できる変数-----
	Properties
	{
		_RampTex("RampTex",2D) = "white"{}
		_Color("Color", Color) = (1,1,1,1)
		_HDR("HDR",float) = 1
	}

	//-----シェーダープログラム-----
	SubShader
	{

		Pass
		{

			Tags{ "LightMode" = "ForwardBase" }

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _RampTex;
			half4 _Color;
			float _HDR;
			
			struct v2f
			{
				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD0;
				float3 normal : NORMAL;
			};

			v2f vert(float4 v:POSITION,float2 uv:TEXCOORD0,float3 normal:NORMAL)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v);
				o.uv = uv;
				o.normal = normal;
				return o;
			}
			
			half4 frag(v2f i) :COLOR
			{
				float3 lightDir = normalize(UnityWorldSpaceLightDir(i.pos));
				float d = dot(i.normal,lightDir) * 0.5f + 0.5f;
				half4 col = tex2D(_RampTex, float2(d,0.5)) * _Color * _HDR;
				return col;
			}

			ENDCG
		}
	}
}
