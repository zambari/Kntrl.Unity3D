# Kntrl 

This is a yet another attempt at a minimalistic worflow for working with float values in unity 3D. Its primary goal is animation, UI design, motion design. 

With a mix of extremely simple modules very complex motions can be designed and controlled.

If you use unity3D Sliders as heavily as I do, or if you want a very flexible way of piping value transforms, you might be interesed.


## Core concepts

The main idea is to make it dead simple this time. Pure basic Update(), no object creation, and fully modular design. 

The value (a float number, typically normalized to 0-1) travels between components divided into three main classes. Value Sources, Value Processors and Value Targets. 

Several basing 'building blocks' for value processing are provided, such as scaling, curve mapping, delay, damping (SmoothDamp), noise.

The main class is KntrlValue which can be treated as an example interface implementation, but it can also handle processor chains, and value chainging (a GameObject consisting a KntrlValue and a few modifiers can serve both serving a targetter directly, or form a chain with other nodes).

An example value flow might look like this:

We have four objects here, the larger sphere is our value source, the value it returns is proportional to the distance to the smaller sphere. 

The values are then taken by two cubes, which apply a different set of processor modules, and apply the resulting value to the scale of the cube.

![](docs/example.png "Value Flow")

When represented more schematically, like this:

![](docs/valueFlow.png "Value Flow")

Each KntrlValue has the possibility to have its own 'effect chain' created by adding 'Processor' components to its gameObject. If no processors are added, an empty KntrlValue can serve as a proxy value (for exqample for distributig values to its children), so that re-routing the signal flow is clear and easy.

The workflow is completely modular (mix and match from exising modules or add new ones), with almost no interdependencies (the only thing that has to be shared between modules is knowledge of the interfaces).

Because processors can be stacked, its possible to program quite complex behaviours using only few of the predefined components.

Processor groups can also be stacked (using KntrlExternalChain component) and reused (although reusing components that make use of time is likely to give unexpected results)




### A "Non Event System"

With this project I wanted no events, and there are none. You update your value every frame and that's if (obviously you can just ignore the value if there is no change).

The nodes always point to the SOURCE of the value, never to target. The publisher needs to know nothing about the listeners. There is no subscribtion / unsubscription so there's a lot less changes to get null pointer exceptions.



![](docs/kntrold.png "Components")

![](docs/diagram.png "Diagram")


## Extending Kntrl

### Value Sources

A Value source is any object that implements *IKntrlValueSource*  interface, 
which means is has a public  *GetValue()* method. 

As long as you mark your calss as implementing this interface, it is a valid KntrlValueSource (no need to inherit from anything if you don't want to).

Few value sources can exist on the same gameObject, and they can reference each other (there's an auto-reference protection, but if multiple objects are found, they are taken from the bottom up.)


###   Value Targets

Also reffere to as value sinks, are the components that implement *IKntrlValueTarget* 
(they have a *SetValue(float f)* method). They are the 'actuators', and are the only components that should modify the world. The most basic (still very useful) targetter is KntrlTargetRotation, which can be used to make 

As long as you apply a float value in a meaningful way, your class can be a value target (value sink for Kntrl)

![](docs/sliders_demo.png "Demo")

###  Value Processors
The most interesting part of the framework is the concept of processors (*IKntrlProcessValue* - they have a *GetProcessedValue(float inputValue)* method.

There is also a neat base class provided for value processors, which enable you to create new ones with no more than a few lines (below is a screenshot of the complete code of one of the included processor - if you use the provided base class you only need to override one method)

![](docs/processorExample.png "Processor")

This short youtube clip demonstrates one of the basic use scenarios (recorded with a slightly old version)

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/rH0a0fMOrWU/0.jpg)](https://www.youtube.com/watch?v=rH0a0fMOrWU)

https://youtu.be/rH0a0fMOrWU


### Notes

Created and tested with Unity 2017 but should work with pretty much all versions above 5.4

Pull requests welcome