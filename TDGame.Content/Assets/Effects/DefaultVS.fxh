#ifndef DREAMBIT_DEFAULT_VS
#define DREAMBIT_DEFAULT_VS

float4x4 MatrixTransform;

struct VS_INPUT
{
    float4 Position : POSITION0;
    float4 Color    : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

struct PS_INPUT
{
    float4 Position : SV_Position;
    float4 Color    : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

PS_INPUT DefaultVS(VS_INPUT input)
{
    PS_INPUT output;

    output.Position = mul(input.Position, MatrixTransform);
    output.Color = input.Color;
    output.TexCoord = input.TexCoord;

    return output;
}

#endif