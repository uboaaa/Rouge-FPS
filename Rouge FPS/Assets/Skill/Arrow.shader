Shader "STYLY/Examples/Arrow"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_OverTex("Base Texture (RGB)", 2D) = "white" {}
		_MaskTex("Mask Texture (RGB)", 2D) = "white" {}
		_ScrollSpeed("Scroll Speed", Float) = 1.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Standard fullforwardshadows
			#pragma target 3.0

			sampler2D _MaskTex;
			sampler2D _OverTex;

			struct Input
			{
				float2 uv_MaskTex;
				float2 uv_OverTex;
			};

			float _ScrollSpeed;
			fixed4 _Color;

			#define ANGLE (_Time.z * _RotationSpeed)



			void surf(Input IN, inout SurfaceOutputStandard o) 
			{
				fixed4 mask = tex2D(_MaskTex, IN.uv_MaskTex);
				clip(mask.r - 0.8); // do not draw if mask.r is less than 0.5

				fixed2 center = fixed2(0.5, 0.5);

				fixed2 uv_OverTex = mul(1, IN.uv_OverTex - center) + _Time * _ScrollSpeed;
				fixed4 over = tex2D(_OverTex, uv_OverTex);
				clip(over.r - 0.8);
				fixed4 c = mask * over;
				o.Emission = c.rgb;

			}
			ENDCG
		}
			FallBack "Diffuse"
}