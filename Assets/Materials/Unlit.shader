// Upgrade NOTE: upgraded instancing buffer 'Unlit' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Unlit"
{
	Properties
	{
		_DissolveTexture("DissolveTexture", 2D) = "white" {}
		_DissolveValue("DissolveValue", Range( 0 , 1)) = 0
		_PanSpeed("PanSpeed", Float) = 0.48
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		_Color("Color", Color) = (0,0,0,0)
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

		uniform float4 _Color;
		uniform sampler2D _DissolveTexture;
		uniform float _PanSpeed;
		uniform float _Cutoff = 0.5;

		UNITY_INSTANCING_BUFFER_START(Unlit)
			UNITY_DEFINE_INSTANCED_PROP(float, _DissolveValue)
#define _DissolveValue_arr Unlit
		UNITY_INSTANCING_BUFFER_END(Unlit)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Emission = _Color.rgb;
			o.Alpha = 1;
			float _DissolveValue_Instance = UNITY_ACCESS_INSTANCED_PROP(_DissolveValue_arr, _DissolveValue);
			float2 temp_cast_1 = (_PanSpeed).xx;
			float2 panner17 = ( _Time.y * temp_cast_1 + i.uv_texcoord);
			clip( ( (-0.6 + (( 1.0 - _DissolveValue_Instance ) - 0.0) * (0.6 - -0.6) / (1.0 - 0.0)) + tex2D( _DissolveTexture, panner17 ) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
-1920;273;1134;508;825.9717;663.1951;1.364942;True;True
Node;AmplifyShaderEditor.SimpleTimeNode;19;-320.8709,425.1432;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-487.4692,168.3903;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-485.1569,308.8976;Inherit;True;Property;_PanSpeed;PanSpeed;3;0;Create;True;0;0;False;0;0.48;0.48;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-569.9344,-45.23432;Inherit;False;InstancedProperty;_DissolveValue;DissolveValue;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;17;-157.5776,184.0508;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;10;-278.3506,-38.5162;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;12;-90.74652,-38.68478;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.6;False;4;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;9;91.40947,178.2858;Inherit;True;Property;_DissolveTexture;DissolveTexture;1;0;Create;True;0;0;False;0;-1;f852220d8a50e5242928b6f6c80af34d;f852220d8a50e5242928b6f6c80af34d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;34;-119.6168,84.42142;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;35;-268.4169,102.0214;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;124.2404,-16.19637;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;3;-456.9517,-220.9245;Inherit;False;InstancedProperty;_TextureSlider;TextureSlider;0;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;37;-88.90302,-409.316;Inherit;False;Property;_Color;Color;5;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;309.5835,-360.8254;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Unlit;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;4;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;17;0;20;0
WireConnection;17;2;18;0
WireConnection;17;1;19;0
WireConnection;10;0;8;0
WireConnection;12;0;10;0
WireConnection;9;1;17;0
WireConnection;13;0;12;0
WireConnection;13;1;9;0
WireConnection;0;2;37;0
WireConnection;0;10;13;0
ASEEND*/
//CHKSM=C4A4E97913E1DE0B297A85F1BCE540E2A5A89832