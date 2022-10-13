Shader "Unlit/CurvedUnlitZombies"
{ 
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		_Blood("Blood", 2D) = "white" {}
		_BloodColor("BloodColor", Color) = (0.6470588,0.2569204,0.2569204,0)
		_BloodAmount("BloodAmount", Range( 0 , 1)) = 0
		_Emissive("Emissive", 2D) = "white" {}
		_EmissiveColor("Emissive Color", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" "IsEmissive" = "true"  }

		LOD 100
		Pass
		{
			Cull Back
			CGPROGRAM
			#pragma target 3.0
			#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
			#pragma vertex vert
			#pragma fragment frag
			// make fog work 
			#pragma multi_compile_fog
				
			#include "CurvedCode.cginc"

			struct Input
			{
				float2 uv_texcoord;
				float2 uv2_texcoord2;
			};
			
			uniform sampler2D _Texture;
			uniform float4 _Texture_ST;
			uniform float4 _BloodColor;
			uniform sampler2D _Blood;
			uniform float4 _Blood_ST;
			uniform float _BloodAmount;
			uniform sampler2D _Emissive;
			uniform float4 _Emissive_ST;
			uniform float4 _EmissiveColor;

			void surf( Input i , inout SurfaceOutputStandard o )
			{
				float2 uv_Texture = i.uv_texcoord * _Texture_ST.xy + _Texture_ST.zw;
				float2 uv2_Blood = i.uv2_texcoord2 * _Blood_ST.xy + _Blood_ST.zw;
				float4 lerpResult33 = lerp( float4( 0,0,0,0 ) , tex2D( _Blood, uv2_Blood, float2( 0,0 ), float2( 0,0 ) ) , _BloodAmount);
				float4 lerpResult18 = lerp( tex2D( _Texture, uv_Texture ) , _BloodColor , lerpResult33);
				o.Albedo = lerpResult18.rgb;
				float2 uv_Emissive = i.uv_texcoord * _Emissive_ST.xy + _Emissive_ST.zw;
				o.Emission = ( tex2D( _Emissive, uv_Emissive ) * _EmissiveColor ).rgb;
				o.Alpha = 1;
			}

		ENDCG
		}
	}
	Fallback "Unlit"
	CustomEditor "ASEMaterialInspector"
}
