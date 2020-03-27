// Upgrade NOTE: upgraded instancing buffer 'TextureSliderShader' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TextureSliderShader"
{
	Properties
	{
		_MainTexture("MainTexture", 2D) = "white" {}
		_TextureSlider("TextureSlider", Range( 0 , 1)) = 1
		_MainColor("MainColor", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _MainColor;
		uniform sampler2D _MainTexture;

		UNITY_INSTANCING_BUFFER_START(TextureSliderShader)
			UNITY_DEFINE_INSTANCED_PROP(float4, _MainTexture_ST)
#define _MainTexture_ST_arr TextureSliderShader
			UNITY_DEFINE_INSTANCED_PROP(float, _TextureSlider)
#define _TextureSlider_arr TextureSliderShader
		UNITY_INSTANCING_BUFFER_END(TextureSliderShader)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _MainTexture_ST_Instance = UNITY_ACCESS_INSTANCED_PROP(_MainTexture_ST_arr, _MainTexture_ST);
			float2 uv_MainTexture = i.uv_texcoord * _MainTexture_ST_Instance.xy + _MainTexture_ST_Instance.zw;
			float _TextureSlider_Instance = UNITY_ACCESS_INSTANCED_PROP(_TextureSlider_arr, _TextureSlider);
			o.Albedo = ( _MainColor + ( _MainColor * ( tex2D( _MainTexture, uv_MainTexture ) * _TextureSlider_Instance ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17800
0;365;1209;513;823.254;497.0751;1.308808;True;True
Node;AmplifyShaderEditor.RangedFloatNode;3;-538.0761,40.58084;Inherit;False;InstancedProperty;_TextureSlider;TextureSlider;1;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-558.7901,-191.6807;Inherit;True;Property;_MainTexture;MainTexture;0;0;Create;True;0;0;False;0;-1;2ae8d36eee565fc4099d596143ae93fe;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-184.3991,-137.5548;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;4;-376.1118,-420.6016;Inherit;False;Property;_MainColor;MainColor;2;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;7.411285,-182.2249;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;88.00641,-381.0464;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;309.5835,-360.8254;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;TextureSliderShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;2;1;3;0
WireConnection;6;0;4;0
WireConnection;6;1;2;0
WireConnection;7;0;4;0
WireConnection;7;1;6;0
WireConnection;0;0;7;0
ASEEND*/
//CHKSM=EB6B3D305FDFD0E03BF9D6F2151F2367D8BBB430