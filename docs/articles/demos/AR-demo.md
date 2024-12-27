# Augmented Reality

This is a demo of a simple augmented reality, where there are four objects placed in front, and the view of the object is dependent on the observer’s position.

You can download this demo from the `demos` folder in the [examples repository](https://github.com/bonvision/examples). The demo is called ClosedLoop.

The workflow is encapsulated below which you can copy into your Bonsai Window.

> [!NOTE]
> The workflow still requires the custom `Models` folder which can be downloaded above.

:::workflow
![Close Loop Demo](../../workflows/demo-closedloop.bonsai)
:::

## Usage
This demo shows how to construct, render and interact with a more complicated environment, as you might in ‘augmented reality’. After pressing run, several objects (‘models’) appear on the floor of a 3D environment, one of which is rotating around its core. The downloaded program listens to the webcam installed on your computer (if present) and tracks the position of a bright light source (like your cell phone’s flashlight). You can move through this environment by moving the flashlight towards and away from the webcam, and/or from side-to-side – your view of the scene should change as you move.

Below is an example of what part of the visual display looks like during an experiment.

![Augmented Reality Demo](~/images/DemoAR_V3.webp){width=500} 


