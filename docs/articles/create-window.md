# Create Window
:::workflow
![Create Window](../workflows/create-window.bonsai)
:::

The `CreateWindow` operator creates an OpenGL window where all the stimuli will be rendered and contains many properties for adjusting the visual environment.

- The `ClearColor` property specifies the background color of the visual environment.
- The `DisplayDevice` property controls which monitor the window will be displayed on (for instance if you have multiple monitors).
- The `TargetRenderFrequency` property controls how fast the stimuli are rendered (by default 60fps).
- The `TargetUpdateFrequency` property by default is set to the same as the `TargetRenderFrequency` but can actually be set higher (for instance 120fps) (useful if you have a high refresh rate monitor but cannot render at the higher `TargetRenderFrequency`).
- The `WindowBorder`property can be used to hide the window border or make it resizable (for instance, if you want the window to span multiple displays)
- The `WindowState` property controls how the the window will be displayed (for instance, you can make it full-screen)


## Video Walkthrough
<div style="max-width: 500px">
<iframe width=100% height="282" src="https://www.youtube.com/embed/gHn9YVvM55I" title="BonVision quick tips: Using CreateWindow" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>


