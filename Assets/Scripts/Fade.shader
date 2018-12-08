// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Fade"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color",Color) = (1,1,1,1)
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float _Timer;
			sampler2D _MainTex;

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 position : SV_POSITION;
			};

			v2f vert(float4 v : POSITION,float2 uv :TEXCOORD0)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v);
				o.uv = uv;
				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 black = 1 - fixed4(_Time.yyy / 2, 1);
				col *= black;
				return col;
			}
			ENDCG
		}
		
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform float _Timer;
			sampler2D _MainTex;

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 position : SV_POSITION;
			};

			v2f vert(float4 v : POSITION,float2 uv :TEXCOORD0)
			{
				v2f o;
				o.position = UnityObjectToClipPos(v);
				o.uv = uv;
				return o;
			}

			fixed4 frag(v2f i) : COLOR
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 black = fixed4(_Time.yyy / 2, 1);
				col *= black;
				return col;
			}
			ENDCG
		}
	}
}