Shader "TNWX/Debug"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100
		Pass{
			Cull Off
			CGPROGRAM
				#pragma vertex vert 
				#pragma fragment frag 
				#include "UnityCG.cginc"
				sampler2D _MainTex;

				struct Data{
					float4 vertex : POSITION;
					float3 normal : NORMAL;
					float2 uv1 : TEXCOORD1;
					float2 uv2 : TEXCOORD2;
					float2 uv3 : TEXCOORD3;
					//float2 uv4 : TEXCOORD4;
				};

				struct V2F{
					float4 pos : SV_POSITION;
					float3 nDir : NORMAL;
					float3 localnDir : TEXCOORD0;
					float2 uv1 : TEXCOORD1;
					float2 uv2 : TEXCOORD2;
					float2 uv3 : TEXCOORD3;
					//float2 uv4 : TEXCOORD4;
				};

				V2F vert(Data data){
					V2F o;
						o.pos = UnityObjectToClipPos(data.vertex);
						o.nDir = normalize(mul(data.normal, (float3x3)unity_WorldToObject));
						o.localnDir = normalize(data.normal);
						o.uv1 = data.uv1;
						o.uv2 = data.uv2;
						o.uv3 = data.uv3;
						//o.uv4 = data.uv4;
					return o;
				}

				float4 frag(V2F i):SV_Target{

					//return float4(i.uv1,0.,1.);
					return tex2D(_MainTex,i.uv1);
					return float4(i.localnDir.xyz,2.);
				}

			ENDCG
		}
		
	}
}
