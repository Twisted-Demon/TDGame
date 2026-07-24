#include "DefaultVS.fxh"

Texture2D<float4> TextureSampler : register(t0);
SamplerState TextureSamplerState : register(s0);

float4 MainPS(PS_INPUT input) : SV_Target0
{
    return TextureSampler.Sample(
        TextureSamplerState,
        input.TexCoord
    ) * input.Color;
}

technique MainTechnique
{
    pass P0
    {
        VertexShader = compile vs_6_0 DefaultVS();
        PixelShader  = compile ps_6_0 MainPS();
    }
}