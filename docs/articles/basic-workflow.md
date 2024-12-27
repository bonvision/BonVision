# Overview

There are five basic sections to making a workflow in BonVision. These sections will be covered in more detail in individual articles which you can access from the sidebar.

```mermaid
flowchart TD

A(Create Window) --> B(Drawing Region) --> C(Draw Stimuli) --> D(Map Stimuli*) --> E(Define Display*)
```

> [!NOTE]
> Map Stimuli and Define Display steps are optional and can be skipped when prototyping

## Create Window

All BonVision workflows need to start by creating a display window and loading the essential BonVision resources. If additional resources
such as 3D models and textures are required, load them at this step using the optional `SceneResources` and `TextureResources` node.

> [!NOTE]
> Copy the workflows below directly into Bonsai by clicking the copy button on the top right of the container to test them out.

:::workflow
![Create Window](../workflows/overview-create-window.bonsai)
:::

## Drawing Region

This defines which region of visual space is used for the visual world as well as the units for the parameters (sizes/locations) that stimuli are drawn with. For the purpose of prototyping stimuli, use `NormalisedView`.

:::workflow
![Drawing Region](../workflows/overview-normalized.bonsai)
:::

### Draw Stimuli

This is where you generate all the aspects of the visual environment.
:::workflow
![Draw Stimuli](../workflows/overview-draw-stimuli.bonsai)
:::

> [!NOTE]
> At this point you have all the basic parts necessary to create and prototype visual environments. The workflow below encapsulates all these steps and draws a simple black and white circle. 
> Copy it into Bonsai and try it out!

:::workflow
![Basic Workflow](../workflows/overview-draw-circle.bonsai)
:::

### Map Stimuli (optional)

This is where 2D stimuli are rendered onto a surface for display in 3D environments using the `SphereMapping` operator. More information can be found in the [Display Environments](display-environment-basics.md).

### Define Display (optional)

Here we define the parameters of the display system by adding a `ViewingWindow` (which are windows into the visual environment) and a `DrawViewport` (for configuring 1 or more monitors) operator.

:::workflow
![Sphere Mapping](../workflows/overview-map-stimuli-define-display.bonsai)
:::

### Video Walkthrough
<div style="max-width: 500px">
<iframe width=100%  height="282" src="https://www.youtube.com/embed/x-J6kIZvOwA" title="BonVision quick tips: Making your first BonVision workflow" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>



