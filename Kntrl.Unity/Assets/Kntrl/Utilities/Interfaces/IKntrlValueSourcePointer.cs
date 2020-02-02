using UnityEngine;
/*

https://github.com/zambari/Kntrl.Unity3D

*/
public interface IKntrlValueSourcePointer
{
    IKntrlValueSource GetSource();

    // implicitly implenetd by all monobehaviours
    string name { get; }
    bool enabled { get; }
    GameObject gameObject {get;}
    Transform transform {get;}

}