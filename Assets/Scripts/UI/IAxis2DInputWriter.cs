using System;
using UnityEngine;


public interface IAxis2DInputWriter
{
    event Action<Vector2> OnAxis2DChanged;
}