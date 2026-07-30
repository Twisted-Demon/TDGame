using System;
using Dreambit;
using Microsoft.Xna.Framework;

namespace TDGame.Core;

public class OrbitalDescentPath
{
    private const float Tau = Mathf.Pi * 2f;

    private readonly Vector2 _planetCenter;

    private readonly float _startRadius;
    private readonly float _endRadius;

    private readonly float _startAngle;
    private readonly float _angleTravel;

    //used for evaluating logarithmic spiral
    private readonly float _radiusRatio;
    private readonly float _logRadiusRatio;

    private float _progress;

    public float Progress => _progress;
    public bool IsComplete => _progress >= 1f;

    public Vector2 Position => Evaluate(_progress);
    public Vector2 Forward => EvaluateForward(_progress);

    /// <summary>
    /// Class Constructor
    /// </summary>
    /// <param name="planetCenter">Position of the planet</param>
    /// <param name="spawnPosition">Start of the descent path</param>
    /// <param name="impactRadius">How far from the center is impact</param>
    /// <param name="orbitDirection">In which direction do we orbit?</param>
    /// <param name="turns">Number of turns before impact</param>
    public OrbitalDescentPath(
        Vector2 planetCenter,
        Vector2 spawnPosition,
        float impactRadius,
        int orbitDirection,
        float turns)
    {
        _planetCenter = planetCenter;
        
        var spawnOffset = spawnPosition - planetCenter;

        _startRadius = spawnOffset.Length();
        _endRadius = impactRadius;

        if (_startRadius <= _endRadius)
            throw new ArgumentException("Spawn position must be outside of impact radius.");

        _startAngle = Mathf.Atan2(spawnOffset.Y, spawnOffset.X);

        orbitDirection = orbitDirection >= 0 ? 1 : -1;

        _angleTravel = Tau * turns * orbitDirection;
        
        _radiusRatio = _endRadius / _startRadius;
        _logRadiusRatio = MathF.Log(_radiusRatio);
    }

    public void Update(float movementSpeed)
    {
        if (IsComplete)
            return;

        var pathUnitsPerProgress = EvaluateDerivativeLength(_progress);

        if (pathUnitsPerProgress <= Mathf.Epsilon)
        {
            _progress = 1f;
            return;
        }

        _progress += movementSpeed * Time.DeltaTime / pathUnitsPerProgress;
        _progress = Mathf.Clamp(_progress, 0f, 1f);
    }

    
    private Vector2 Evaluate(float progress)
    {
        progress = Mathf.Clamp(progress, 0f, 1f);

        var radius = EvaluateRadius(progress);
        var angle = EvaluateAngle(progress);
        
        var radialDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        return _planetCenter + radialDirection * radius;
    }

    private Vector2 EvaluateForward(float progress)
    {
        var derivative = EvaluateDerivative(progress);

        if (derivative.LengthSquared() <= Mathf.Epsilon)
            return Vector2.UnitX;

        return Vector2.Normalize(derivative);
    }

    private float EvaluateAngle(float progress)
    {
        return _startAngle + _angleTravel * progress;
    }

    private float EvaluateRadius(float progress)
    {
        return _startRadius * Mathf.Pow(_radiusRatio, progress);
    }
    
    private float EvaluateDerivativeLength(float progress)
    {
        return EvaluateDerivative(progress).Length();
    }
    
    private Vector2 EvaluateDerivative(float progress)
    {
        var radius = EvaluateRadius(progress);
        var angle = EvaluateAngle(progress);
        
        var radialDirection = new Vector2(
            MathF.Cos(angle),
            MathF.Sin(angle));

        var tangentDirection = new Vector2(
            -MathF.Sin(angle),
            MathF.Cos(angle));

        var radiusDerivative = radius * _logRadiusRatio;
        var angleDerivative = _angleTravel;

        return radialDirection * radiusDerivative +
               tangentDirection * (radius * angleDerivative);
    }
}