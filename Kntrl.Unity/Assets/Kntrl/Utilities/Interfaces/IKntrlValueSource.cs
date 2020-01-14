/*

https://github.com/zambari/Kntrl.Unity3D

*/
public interface IKntrlValueSource
{
    float GetValue();
    string name { get; }
    bool enabled { get; }
}