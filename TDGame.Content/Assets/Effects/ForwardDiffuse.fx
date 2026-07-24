struct PS_INPUT
{
    float4 Position : SV_Position;
    float4 Color    : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

Texture2D<float4> TextureSampler : register(t0);
SamplerState TextureSamplerState : register(s0);

float4 MainPS(PS_INPUT input) : SV_Target0
{
    return TextureSampler.Sample(TextureSamplerState, input.TexCoord) * input.Color;
}

technique MainTechnique
{
    pass P0
    {
        PixelShader = compile ps_6_0 MainPS();
    }
}
