// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Vinicius/SelfIlluminated"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_EmisionMap("EmisionMap", 2D) = "white" {}
		_SelfIlluminatedForce("SelfIlluminatedForce", Float) = 0
		_ColorLight("ColorLight", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform sampler2D _EmisionMap;
		uniform float4 _EmisionMap_ST;
		uniform float _SelfIlluminatedForce;
		uniform float4 _ColorLight;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			o.Albedo = tex2D( _MainTex, uv_MainTex ).rgb;
			float2 uv_EmisionMap = i.uv_texcoord * _EmisionMap_ST.xy + _EmisionMap_ST.zw;
			o.Emission = ( tex2D( _EmisionMap, uv_EmisionMap ) * _SelfIlluminatedForce * _ColorLight ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13701
301;92;480;342;1236.483;216.7088;2.484005;True;False
Node;AmplifyShaderEditor.SamplerNode;2;-526.7884,71.39613;Float;True;Property;_EmisionMap;EmisionMap;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;5;-503.7578,406.7538;Float;False;Property;_ColorLight;ColorLight;3;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;3;-529.4124,280.2006;Float;False;Property;_SelfIlluminatedForce;SelfIlluminatedForce;2;0;0;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-463.2496,-150.6329;Float;True;Property;_MainTex;MainTex;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-212.2677,200.2553;Float;False;3;3;0;COLOR;0.0;False;1;FLOAT;0.0;False;2;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Vinicius/SelfIlluminated;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;4;2;5;0
WireConnection;0;0;1;0
WireConnection;0;2;4;0
ASEEND*/
//CHKSM=BEC6D2677D34CF927B70AD2125E95837262AA1AA