// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Vinicius/TexAndNormal"
{
	Properties
	{
		_Text1("Text1", 2D) = "white" {}
		_Text2("Text2", 2D) = "white" {}
		_TextureBlend("TextureBlend", Range( 0 , 1)) = 0
		_NormalMap("NormalMap", 2D) = "bump" {}
		_NormalMapForce("NormalMapForce", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalMapForce;
		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform sampler2D _Text1;
		uniform float4 _Text1_ST;
		uniform sampler2D _Text2;
		uniform float4 _Text2_ST;
		uniform float _TextureBlend;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _NormalMap, uv_NormalMap ) ,_NormalMapForce );
			float2 uv_Text1 = i.uv_texcoord * _Text1_ST.xy + _Text1_ST.zw;
			float2 uv_Text2 = i.uv_texcoord * _Text2_ST.xy + _Text2_ST.zw;
			float4 lerpResult4 = lerp( tex2D( _Text1, uv_Text1 ) , tex2D( _Text2, uv_Text2 ) , _TextureBlend);
			o.Albedo = lerpResult4.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13701
301;92;480;342;1155.624;393.6701;2.574823;False;False
Node;AmplifyShaderEditor.SamplerNode;1;-644.4824,-178.8127;Float;True;Property;_Text1;Text1;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;7;-883.9766,555.1817;Float;False;Property;_NormalMapForce;NormalMapForce;4;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;2;-642.7594,34.83805;Float;True;Property;_Text2;Text2;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;5;-639.3136,250.2117;Float;False;Property;_TextureBlend;TextureBlend;2;0;0;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;6;-628.9753,367.3748;Float;True;Property;_NormalMap;NormalMap;3;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;4;-237.8577,-46.14228;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Vinicius/TexAndNormal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;5;7;0
WireConnection;4;0;1;0
WireConnection;4;1;2;0
WireConnection;4;2;5;0
WireConnection;0;0;4;0
WireConnection;0;1;6;0
ASEEND*/
//CHKSM=1C829F3F1D98346BE4D997E3AAF99DB5F934EBFB