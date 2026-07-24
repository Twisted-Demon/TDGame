Texture2D<float4> TextureSampler      : register(t0);
SamplerState TextureSamplerState : register(s0);

float4 tintColor;

float4 MainPS(float2 texCoord : TEXCOORD0) : SV_Target0
{
    float4 color = TextureSampler.Sample(TextureSamplerState, texCoord);
    color *= tintColor;
    return color;
}

technique MainTechnique
{
    pass P0
    {
        PixelShader = compile ps_6_0 MainPS();
    }
}
