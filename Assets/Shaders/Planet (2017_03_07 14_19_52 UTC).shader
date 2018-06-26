// Planet Surface Shader by Matt Mechtley
// Licensed Under a Creative Commons Attribution license
// http://creativecommons.org/licenses/by/3.0/
Shader "Custom/Planet" {
	Properties {
		_MainTex ("Diffuse(RGB) Spec(A)", 2D) = "white" {}
		_BumpMap ("Bumpmap", 2D) = "bump" {}
		_EmissionMap("Night Lights Map", 2D) = "white" {}
		_EmissionStr("Night Lights Strength", Range(0,1)) = 0.5
		_EmissionColor("Night Lights Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SpecColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		_SpecPower ("Specular Power", Float) = 48.0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM

		#pragma surface surf Planet noambient
		float _Shininess;
		float _SpecPower;
		float _EmissionStr;
		half4 _EmissionColor;

		half4 LightingPlanet (SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) {
			half3 lightView = normalize (lightDir + viewDir);
			half diffuse = max (0, dot (s.Normal, lightDir));
			float specStr = max (0, dot(s.Normal, lightView));
			float spec = pow (specStr, _SpecPower);
			half4 c;
			c.rgb = _LightColor0.rgb * atten * (s.Albedo * diffuse + 
				spec * s.Specular * _Shininess * _SpecColor) +
				(saturate(1.0-2*diffuse) * s.Alpha * _EmissionStr * _EmissionColor);
			c.a = s.Specular;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_EmissionMap;
			float3 viewDir;
		};
		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _EmissionMap;

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
			o.Specular = tex2D (_MainTex, IN.uv_MainTex).a;
			o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
			o.Alpha = length(tex2D (_EmissionMap, IN.uv_EmissionMap).rgb);
		}

		ENDCG
	}
	Fallback "Diffuse"
}
