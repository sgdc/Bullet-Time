﻿Shader "Custom/ShadowShaderNoNormal" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MetallicGlossMap("Metallic", 2D) = "black" {}
	}
	SubShader {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _MetallicGlossMap;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			fixed4 m = tex2D(_MetallicGlossMap, IN.uv_MainTex);
			o.Metallic = m.r;
			o.Smoothness = m.a;
			o.Alpha = c.a * (0.1 *sin(IN.worldPos.y * 25) + 0.6);
		}
		ENDCG
	}
	FallBack "Diffuse"
}