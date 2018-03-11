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
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
//#pragma exclude_renderers d3d11 gles
				#pragma target 3.0
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
				};

				struct V2F{
					float4 pos : SV_POSITION;
					float3 nDir : NORMAL;
					float3 localnDir : TEXCOORD0;
					float2 uv1 : TEXCOORD1;
					float2 uv2 : TEXCOORD2;
					float2 uv3 : TEXCOORD3;
				};

				V2F vert(Data data){
					V2F o;
						o.pos = UnityObjectToClipPos(data.vertex);
						o.nDir = normalize(mul(data.normal, (float3x3)unity_WorldToObject));
						o.localnDir = normalize(data.normal);
						o.uv1 = data.uv1;
						o.uv2 = data.uv2;
						o.uv3 = data.uv3;
					return o;
				}

				float4 frag(V2F i):SV_Target{

					float2 e = float2(1./512,0.);
					//return float4(i.uv1,0.,1.);
					float4 cc0 = tex2D(_MainTex,i.uv1);
					float4 cc[8];
					cc[0] = tex2D(_MainTex, i.uv1 + e.xy);
					cc[1] = tex2D(_MainTex, i.uv1 - e.xy);
					cc[2] = tex2D(_MainTex, i.uv1 + e.yx);
					cc[3] = tex2D(_MainTex, i.uv1 - e.yx);
					cc[4] = tex2D(_MainTex, i.uv1 + e.xx);
					cc[5] = tex2D(_MainTex, i.uv1 - e.xx);
					cc[6] = tex2D(_MainTex, i.uv1 + float2(e.x,-e.x));
					cc[7] = tex2D(_MainTex, i.uv1 + float2(-e.x,e.x));
					float4 col = float4(0, 0, 0, 0);
					for (int i = 0;i < 8; i++) {
						col = max(col,cc[i].a > cc0.a ? cc[i] : cc0);
					}
					//col = cc[7].a > cc0.a ? cc[7] : cc0*0.;
					//col /= 8;
					return col;
				}

			ENDCG
		}
		
	}
}
