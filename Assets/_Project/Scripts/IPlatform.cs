
using UnityEngine;

public interface IPlatform 
{
    bool isFlipped { get; }
    void SetFlippedState(bool flipped);
    void SetPlatformColour(Color colour);
}
