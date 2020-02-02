Shader "Unlit/Arrow" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_OverTex("Base Texture (RGB)", 2D) = "white" {}
		_MaskTex("Mask Texture (RGB)", 2D) = "white" {}

		_XShift("Xuv Shift", Range(-1.0, 1.0)) = 0.1
		_XSpeed("X Scroll Speed", Range(1.0, 100.0)) = 10.0
	}
		SubShader{
			//Tags { "RenderType" = "Opaque" }
			Tags {"Queue" = "Transparent" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard alpha : fade
			#pragma target 3.0

			sampler2D _MaskTex;
			sampler2D _OverTex;

			struct Input {
				float2 uv_MaskTex;
				float2 uv_OverTex;
			};

			fixed4 _Color;
			float _XShift;
			float _XSpeed;
			

			void surf(Input IN, inout SurfaceOutputStandard o) {
				fixed4 mask = tex2D(_MaskTex, IN.uv_MaskTex);
				clip(mask.r - 0.8); // do not draw if mask.r is less than 0.5

				_XShift = _XShift * _XSpeed;

				IN.uv_OverTex.x += _Time * _XShift;

				fixed2 uv_OverTex = IN.uv_OverTex;	

				fixed4 over = tex2D(_OverTex, uv_OverTex);
				clip(over.r - 0.8);

				fixed4 c = mask * over * _Color;
				o.Emission = c.rgb;
			}
			ENDCG
		}
			FallBack "Diffuse"
}