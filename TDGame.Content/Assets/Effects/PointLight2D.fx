// Despite the historical WS suffix, this value must be supplied in
// render-target pixel coordinates because SV_Position is screen-space here.
float2 LightingPositionWS;
float3 LightColor;
float  LightRadius;
float  LightIntensity;

float3 AmbientColor;
float  SpecularPower;
float  SpecularStrength;

struct PS_INPUT
{
    float4 Position : SV_Position;
    float2 TexCoord : TEXCOORD0;
};

float SmoothFalloff(float distanceToLight, float radius)
{
    float normalizedDistance = saturate(distanceToLight / max(radius, 0.00001f));
    float falloff = 1.0f - normalizedDistance;
    return falloff * falloff * (3.0f - 2.0f * falloff);
}

float4 MainPS(PS_INPUT input) : SV_Target0
{
    float distanceToLight = length(input.Position.xy - LightingPositionWS);
    float attenuation = SmoothFalloff(distanceToLight, LightRadius);

    // The old shader exposed these values but never used them. Treat them as
    // optional controls for a tighter bright core so DXC keeps the parameters.
    float highlightPower = max(SpecularPower, 1.0f);
    float highlight = pow(saturate(attenuation), highlightPower);
    float shapedIntensity = attenuation + highlight * max(SpecularStrength, 0.0f);

    float3 light = AmbientColor + LightColor * LightIntensity * shapedIntensity;
    return float4(light, attenuation);
}

technique MainTechnique
{
    pass P0
    {
        PixelShader = compile ps_6_0 MainPS();
    }
}

