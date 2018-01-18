Shader "Unlit/BarierShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		_HexColor("HexColor", Color) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 100

		Pass
		{
			Blend One One
			ZWrite Off
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD2;
				float3 objectPos : TEXCOORD3;
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _RimValue;
			float4 _Color;
			float4 _HexColor;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.viewDir = normalize(UnityWorldSpaceViewDir(mul(unity_ObjectToWorld, v.vertex)));
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.objectPos = v.vertex.xyz;
				return o;
			}
			

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 mainTex = tex2D(_MainTex, i.uv);
				mainTex.r *= saturate(frac(-_Time.x * 15 + abs(i.objectPos.y)) * 1.5 - 1) * 10;

				float rim = 1 - abs(dot(i.normal, normalize(i.viewDir))) * 4;
				float glow = max(rim, i.objectPos.y - 5);
				fixed4 glowColor = _HexColor;

				fixed4 hexes = mainTex.r * _HexColor + mainTex.g * _HexColor + glowColor * glow;
				fixed4 col = _Color * _Color.a + hexes;
				return col;
			}
			ENDCG
		}
	}
}
