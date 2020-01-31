Shader "Custom/Mosaic" {
	Properties{
		_MainTex("Source", 2D) = "white" {}
		_Size("Size", Float) = 1
	}
		SubShader{
			ZTest Always
			Cull Off
			ZWrite Off
			Fog { Mode Off }

			Pass{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"

				struct v2f {
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				v2f vert(appdata_img v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
					return o;
				}

				sampler2D _MainTex;
				float _Size;

				fixed4 frag(v2f i) : SV_TARGET {
					float2 delta = _Size / _ScreenParams.xy;
					float2 uv = (floor(i.uv / delta) + 0.5) * delta;

					return tex2D(_MainTex, uv);
				}
				ENDCG
			}
		}
			FallBack Off
}