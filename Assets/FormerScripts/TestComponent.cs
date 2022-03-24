using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script Execution order is NOT guaranteed
//Unity will search the tree,and try its best,but its never a sure thing
public class TestComponent : MonoBehaviour
{
    private GameObject testGameObject;
    //void OnEnable is called any time the component is Enabled
    private void OnEnable()
    {//Load is (static method)generic method.
        var prefab = Resources.Load<GameObject>("ResourceTest");
        testGameObject = Instantiate(prefab);
    }

    private void OnDisable()
    {
        Destroy(this.gameObject);
    }
}
