using UnityEngine;
/*

https://github.com/zambari/Kntrl.Unity3D

//v 2.0 generic

*/
public interface IKntrlValueSource
{
    float GetValue();

    // implicitly implenetd by all monobehaviours
    // string  GetMessage();//
    bool enabled { get; }
    GameObject gameObject {get;}
    Transform transform {get;}

}