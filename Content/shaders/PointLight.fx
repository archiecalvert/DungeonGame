﻿#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

struct VertexShaderOutput
{
	float4 Color : COLOR0;
    float2 texCoords : TEXCOORD0;
};

sampler2D texSampler;

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 col = tex2D(texSampler, input.texCoords);
	
    return (0,0,0, 0.1);
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};