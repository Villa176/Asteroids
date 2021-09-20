#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float Percentage;
sampler TextureSampler: register(s0);
float4 MainPS(float2 Tex: TEXCOORD0) : COLOR
{
	float4 Color = tex2D(TextureSampler, Tex);
	float r = Color.r;
	float g = Color.g;
	float b = Color.b;
	Color.rgb = dot(Color.rgb, float3(0.9 * Percentage, 0.9 * Percentage, 0.9 * Percentage));
	r = r - (r - Color.rgb) * Percentage;
	g = g - (g - Color.rgb) * Percentage;
	b = b - (b - Color.rgb) * Percentage;
	Color.r = r;
	Color.g = g;
	Color.b = b;

	return Color;
}

technique Hit
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};