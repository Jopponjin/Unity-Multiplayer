using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuit
{
    MovmentData movmentData;

    bool canMoveInXAxsis = false;

    [UnityTest]
    public IEnumerator MoveX()
    {


        yield return new WaitForSeconds(0.5f);

        if (movmentData.moveDirection.x >= 0.1f)
        {
            Assert.True(canMoveInXAxsis);
        }
    }

    [UnityTest]
    public bool MoveZ()
    {

        return false;
    }
}
