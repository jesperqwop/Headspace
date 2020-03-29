// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( PostProcessPPSRenderer ), PostProcessEvent.BeforeStack, "PPS", true )]
public sealed class PostProcessPPSSettings : PostProcessEffectSettings
{
	[Tooltip( "Screen" )]
	public TextureParameter _MainTex = new TextureParameter {  };
	[Tooltip( "Color 0" )]
	public ColorParameter _Color0 = new ColorParameter { value = new Color(0f,0f,0f,0f) };
}

public sealed class PostProcessPPSRenderer : PostProcessEffectRenderer<PostProcessPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "PostProcess" ) );
		if(settings._MainTex.value != null) sheet.properties.SetTexture( "_MainTex", settings._MainTex );
		sheet.properties.SetColor( "_Color0", settings._Color0 );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
