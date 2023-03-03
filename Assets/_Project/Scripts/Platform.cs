
using System;
using UnityEngine;

public class Platform : MonoBehaviour, IPlatform
{
    [SerializeField] Material _platformNormal;
    [SerializeField] Material _platformFlipped;
    [SerializeField] Renderer _platformRenderer;
    [SerializeField] AudioClip _flipSound;
    public bool isFlipped { get; private set; }

    public void SetFlippedState(bool flipped)
    {
        isFlipped = flipped;
        SetPlatformmaterial();
        if(flipped)
        {
            // play audio clip
            // add points to score
        }

        // tell game manager we flipped that platform.
    }

    public void SetPlatformColour(Color colour)
    {
        _platformRenderer.material.color = colour;
    }

    void SetPlatformmaterial()
    {
        _platformRenderer.material = isFlipped ? _platformFlipped : _platformNormal;
    }

}
