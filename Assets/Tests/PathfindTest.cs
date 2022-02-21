using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Controller.MovementResolver;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

[TestFixture]
public class PathfindTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void PathfindTestSimplePasses()
    {
        var resovler = new AStarPathFindResolver();

        Assert.Pass();
    }

    
}
