using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

public class BoardManager : MonoBehaviour 
{
    [SerializeField] GameObject _squarePrefab;
    //transport disc
    //transport disc position

    List<IPlatform> _platforms;
    Transform _transform;
    List<Vector3> _downDirections, _upDirections;

    private void Awake()
    {
        _transform = transform;
        // instantiate transport disc

        _downDirections = new List<Vector3>
        {
            new Vector3(3, -3, 0),
            new Vector3(0, -3, -3)
        };

        _upDirections = new List<Vector3>
        { 
            new Vector3(-3, 3, 0),
            new Vector3(0, 3, 3)
        };
    }

    public int unFlippedPlatforms => _platforms.Count(p => !p.isFlipped);

    public static readonly Vector3 noChange = Vector3.up;
    public static readonly Vector3 northEast = Vector3.zero;
    public static readonly Vector3 northWest = new Vector3(0, 270, 0);
    public static readonly Vector3 southEast = new Vector3(0, 90, 0);
    public static readonly Vector3 southWest = new Vector3(0, 180, 0);

    public void SetUpBoard()
    {
        // set up transport discs
        if(_platforms?.Count > 0)
        {
            ResetPlatforms();
            return;
        }

        _platforms = new List<IPlatform>();
        int blocksPerRow = 7;
        int y = 0;
        int startZ = 0;

        while(blocksPerRow > 0)
        {
            for(int i = 0; i < blocksPerRow; ++i)
            {
                GameObject square = Instantiate(_squarePrefab, _transform);
                square.transform.localPosition = new Vector3(i * 3, y, startZ + i * 3);
                _platforms.Add(square.GetComponentInChildren<IPlatform>());
            }

            --blocksPerRow;
            startZ += 3;
            y += 3; 
        }
    }

    private void ResetPlatforms()
    {
        if (unFlippedPlatforms == _platforms.Count) return;
        foreach(var platform in _platforms.Where(platform => platform.isFlipped))
        {
            platform.SetFlippedState(false);
        }
    }
}