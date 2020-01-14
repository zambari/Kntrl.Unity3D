/*

https://github.com/zambari/Kntrl.Unity3D

*/


public interface IKntrlProcessValue
{
    float GetProcessedValue(float inputValue);
    string name { get; }
    bool enabled { get; }
}