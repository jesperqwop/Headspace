// Upgrade NOTE: upgraded instancing buffer 'TextureSliderShader' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TextureSliderShader"
{
	Properties
	{
		_MainColor("MainColor", Color) = (1,1,1,0)
		_MainTexture("MainTexture", 2D) = "white" {}
		_SpecularTexture("SpecularTexture", 2D) = "white" {}
		_NormalMap("NormalMap", 2D) = "bump" {}
		_AO("AO", 2D) = "white" {}
		_AOmultiplier("AO multiplier", Range( 0 , 1)) = 0.278
		_TextureColor("TextureColor", Color) = (1,1,1,1)
		_TextureSlider("TextureSlider", Range( 0 , 1)) = 1
		_DissolveTexture("DissolveTexture", 2D) = "white" {}
		_DissolveValue("DissolveValue", Range( 0 , 1)) = 0
		_PanSpeed("PanSpeed", Float) = 0.48
		_EmissionValue("EmissionValue", Float) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Smoothness("Smoothness", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _NormalMap;
		uniform float4 _MainColor;
		uniform float4 _TextureColor;
		uniform sampler2D _MainTexture;
		uniform float _EmissionValue;
		uniform sampler2D _SpecularTexture;
		uniform float _Smoothness;
		uniform float _AOmultiplier;
		uniform sampler2D _AO;
		uniform sampler2D _DissolveTexture;
		uniform float _PanSpeed;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(TextureSliderShader)
			UNITY_DEFINE_INSTANCED_PROP(float4, _NormalMap_ST)
#define _NormalMap_ST_arr TextureSliderShader
			UNITY_DEFINE_INSTANCED_PROP(float4, _MainTexture_ST)
#define _MainTexture_ST_arr TextureSliderShader
			UNITY_DEFINE_INSTANCED_PROP(float4, _SpecularTexture_ST)
#define _SpecularTexture_ST_arr TextureSliderShader
			UNITY_DEFINE_INSTANCED_PROP(float4, _AO_ST)
#define _AO_ST_arr TextureSliderShader
			UNITY_DEFINE_INSTANCED_PROP(float, _TextureSlider)
#define _TextureSlider_arr TextureSliderShader
			UNITY_DEFINE_INSTANCED_PROP(float, _DissolveValue)
#define _DissolveValue_arr TextureSliderShader
		UNITY_INSTANCING_BUFFER_END(TextureSliderShader)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _NormalMap_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_NormalMap_ST_arr, _NormalMap_ST);
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST_Instance.xy + _NormalMap_ST_Instance.zw;
			o.Normal = UnpackNormal( tex2D( _NormalMap, uv_NormalMap ) );
			float _TextureSlider_Instance = UNITY_ACCESS_INSTANCED_PROP(_TextureSlider_arr, _TextureSlider);
			float4 _MainTexture_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_MainTexture_ST_arr, _MainTexture_ST);
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST_Instance.xy + _MainTexture_ST_Instance.zw;
			float4 tex2DNode1 = tex2D( _MainTexture, uv_MainTexture );
			o.Albedo = ( ( _MainColor * ( 1.0 - _TextureSlider_Instance ) ) + ( _TextureColor * ( tex2DNode1 * _TextureSlider_Instance ) ) ).rgb;
			o.Emission = ( tex2DNode1 * _EmissionValue ).rgb;
			float4 _SpecularTexture_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_SpecularTexture_ST_arr, _SpecularTexture_ST);
			float2 uv_SpecularTexture = i.uv_texcoord * _SpecularTexture_ST_Instance.xy + _SpecularTexture_ST_Instance.zw;
			o.Metallic = tex2D( _SpecularTexture, uv_SpecularTexture ).r;
			o.Smoothness = _Smoothness;
			float4 _AO_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_AO_ST_arr, _AO_ST);
			float2 uv_AO = i.uv_texcoord * _AO_ST_Instance.xy + _AO_ST_Instance.zw;
			o.Occlusion = ( _AOmultiplier * tex2D( _AO, uv_AO ) ).r;
			o.Alpha = 1;
			float _DissolveValue_Instance = UNITY_ACCESS_INSTANCED_PROP(_DissolveValue_arr, _DissolveValue);
			float2 temp_cast_4 = (_PanSpeed).xx;
			float2 panner17 = ( _Time.y * temp_cast_4 + i.uv_texcoord);
			clip( ( (-0.6 + (( 1.0 - _DissolveValue_Instance ) - 0.0) * (0.6 - -0.6) / (1.0 - 0.0)) + tex2D( _DissolveTexture, panner17 ) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
-1920;608;1035;270;1095.115;803.9321;1.669506;True;True
Node;AmplifyShaderEditor.SimpleTimeNode;19;-320.8709,425.1432;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-485.1569,308.8976;Inherit;True;Property;_PanSpeed;PanSpeed;10;0;Create;True;0;0;False;0;0.48;0.51;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-487.4692,168.3903;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-568.3344,-45.23432;Inherit;False;InstancedProperty;_DissolveValue;DissolveValue;9;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-574.1893,-438.0665;Inherit;True;Property;_MainTexture;MainTexture;1;0;Create;True;0;0;False;0;-1;8a1bc300c5bf341488b04ada828aba86;2ae8d36eee565fc4099d596143ae93fe;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;3;-553.8699,-219.1297;Inherit;False;InstancedProperty;_TextureSlider;TextureSlider;7;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-172.9585,-734.1877;Inherit;False;Property;_MainColor;MainColor;0;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-270.4807,-445.2961;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PannerNode;17;-157.5776,184.0508;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;10;-278.3506,-38.5162;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-484.4526,-719.892;Inherit;False;Property;_TextureColor;TextureColor;6;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;23;-198.6997,-557.1492;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-103.1501,-257.5805;Inherit;False;Property;_EmissionValue;EmissionValue;11;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;544.7785,-30.69584;Inherit;False;Property;_AOmultiplier;AO multiplier;5;0;Create;True;0;0;False;0;0.278;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-125.8004,-485.0744;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;9;91.40947,178.2858;Inherit;True;Property;_DissolveTexture;DissolveTexture;8;0;Create;True;0;0;False;0;-1;f852220d8a50e5242928b6f6c80af34d;750b1bd7ba8bd28489650de6d0a95cc5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;569.4225,71.07123;Inherit;True;Property;_AO;AO;4;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-26.19268,-575.1411;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;12;-90.74652,-38.68478;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.6;False;4;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-37.40032,-173.371;Inherit;False;Property;_Smoothness;Smoothness;13;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;192.5977,-474.2195;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;138.3674,-309.0401;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;27;334.6102,-656.1474;Inherit;True;Property;_NormalMap;NormalMap;3;0;Create;True;0;0;False;0;-1;44af72d58debec14f9734f7dba8c6574;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;703.195,-42.44742;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;124.2404,-16.19637;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;26;570.5353,-314.4891;Inherit;True;Property;_SpecularTexture;SpecularTexture;2;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;309.5835,-360.8254;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;TextureSliderShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;12;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;17;0;20;0
WireConnection;17;2;18;0
WireConnection;17;1;19;0
WireConnection;10;0;8;0
WireConnection;23;0;3;0
WireConnection;6;0;4;0
WireConnection;6;1;2;0
WireConnection;9;1;17;0
WireConnection;24;0;14;0
WireConnection;24;1;23;0
WireConnection;12;0;10;0
WireConnection;7;0;24;0
WireConnection;7;1;6;0
WireConnection;22;0;1;0
WireConnection;22;1;21;0
WireConnection;31;0;30;0
WireConnection;31;1;29;0
WireConnection;13;0;12;0
WireConnection;13;1;9;0
WireConnection;0;0;7;0
WireConnection;0;1;27;0
WireConnection;0;2;22;0
WireConnection;0;3;26;0
WireConnection;0;4;28;0
WireConnection;0;5;31;0
WireConnection;0;10;13;0
ASEEND*/
//CHKSM=5D37449FEAEA14B5F7FBEB8F3035EE7CEAA5D2E7