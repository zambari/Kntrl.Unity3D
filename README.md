# Kntrl 

This is a yet another attempt at a minimalistic worflow for working with float values in unity 3D. Its primary goal is animation, UI design, motion design. 

With a mix of extremely simple modules very complex motions can be designed and controlled.

If you use unity3D Sliders as heavily as I do, or if you want a very flexible way of piping value transforms, you might be interesed.


# Core concepts

The main idea is to make it dead simple this time. Pure basic Update(), no object creation, and fully modular design. 

The main class is KntrlValue which is a sample interface implementation. In a most common example you would start with a Slider and add a KntrlValue to it. Now create another object that will listen to the value.


 that the value (a float number, typically normalized to 0-1) travels between components divided into three main classes. Value Sources, Value Processors and Value Targets. 



![](docs/diagram.png "Diagram")


### A "Non Event System"

With this project I wanted no events, and there are none. You update your value every frame and that's if (obviously you can just ignore the value if there is no change).

The nodes always point to the SOURCE of the value, never to target. The publisher needs to know nothing about the listeners. 

# Features


![](docs/kontrold.png "Diagram")
## Value Sources

A Value source is any object that implements *IKntrlValueSource*  interface, 
which means is has a public  *GetValue()* method. 

That's it, any of your components can be a ValueSource, no need to inherit from anything.

## Value Targets

Also reffere to as value sinks, are the components that implement *IKntrlValueTarget* 
(they have a *SetValue(float f)* method). They are the 'actuators', and are the only components that should modify the world. The most basic (still very useful) targetter is KntrlTargetRotation, which can be used to make 

## Value Processors
The most interesting part of the framework is the concept of processors (*IKntrlProcessValue* - they have a *GetProcessedValue(float inputValue)* method.

## Signal routing

The main idea is that we 

## Sources
KntrlValue aka IHaveCurrentValue is an interface for publishing your float (assuming 0-1 normalized) value. 
The primary source is KntrlSourceSlider which takes a slider and publishes its value (also taking it as an input )


