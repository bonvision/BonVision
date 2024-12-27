# Draw Stimuli

`Bonvision` comes with a set of operators/primitives to draw basic shapes, display images and video, render common visual neuroscience stimuli, and display complex scenes and 3d models.

The starting point to test these out is a basic workflow (which was covered in the [overview](basic-workflow.md) section). 
:::workflow
![Basic Workflow](../workflows/overview-draw-circle.bonsai)
:::

## Basic primitives

:::workflow
![Basic Primitives](../workflows/draw-stimuli-basic-primitives.bonsai)
:::

Using these primitives are straightforward, there are properties to change the location as well as the size and color of the element being drawn.


## Images and video primitives
:::workflow
![Image and Video Primitives](../workflows/draw-stimuli-image-video-primitives.bonsai)
:::

In order to display images and videos, they have to be first loaded as a resource during the `Create Section` portion of the workflow using the `TextureResources` operator. Double click the `TextureResources`operator and add the image as a `ImageTexture` or `VideoTexture`.

:::workflow
![Draw Image](../workflows/draw-stimuli-draw-image.bonsai)
:::

`BonVision` also provides a way to display an image from a set of images using an `ImageSequence`. To do so, package the set of images as a video file and load them into the `TextureResources` operator as an `ImageSequence`.
We can then select from the images to display by using a `BindTexture` operator in front of the `DrawImage` operator to choose which index/frame of the video we want to display.
In this example workflow, pressing the spacebar samples from a random number distribution to select a random image to display.

:::workflow
![Draw Image Sequence](../workflows/draw-stimuli-draw-image-sequence.bonsai)
:::


### Video Walkthrough
<div style="max-width: 500px;">
<iframe width=100% height="282" src="https://www.youtube.com/embed/I3JuU_-PJuE" title="BonVision quick tips: How to display an image" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>

<div style="max-width: 500px">
<iframe width=100% height="282" src="https://www.youtube.com/embed/gYlgBgvSrmg" title="BonVision quick tips: Playing a movie" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>


## Neuroscience primitives
`Bonvision` also comes with a set of primitives for drawing common visual neuroscience stimuli.

:::workflow
![Neuroscience Primitives](../workflows/draw-stimuli-neuroscience-primitives.bonsai)
:::

These neuroscience primitives are covered in more detail in the [Neuroscience Stimuli](stimuli-neuroscience.md) article.


## 3D Model and Scene primitives

:::workflow
![3D Model and Scene Primitives](../workflows/draw-stimuli-scene-model-primitives.bonsai)
:::

### DrawModel

`DrawModel` is used to display 3D models without textures. `DrawModel` supports 3D models in the `.obj` file format.
The models have to be first loaded as a resource during the `Create Section` portion of the workflow using the `MeshResources` operator (which loads mesh or geometry resources).

To try the example workflow out, download a 3D model (such as the icosphere) from the [Bonvision Examples Repository](https://github.com/bonvision/examples/tree/master/Primitives).
Double click on the `MeshResources` operator and add the downloaded model as a TexturedModel, giving it a name like `icosphere`.
In the `DrawModel` node, select the model `icosphere` under the `MeshName` property

:::workflow
![Draw Model](../workflows/draw-stimuli-model.bonsai)
:::

### DrawTexturedModel

`DrawTexturedModel` is used to display 3D models with textures.  To use `DrawTexturedModel`, during the `Create Section` portion of the workflow the model needs to be loaded using using the  `MeshResources` operator and the texture needs to be loaded using the `TextureResources` operator (which loads image or video resources).

To try the example workflow out, download the 3D model `TextureSphere` and the `BlueMarble.jpg` from the [Bonvision Examples Repository](https://github.com/bonvision/examples/tree/master/Primitives).
Double click on the `MeshResources` operator and add the downloaded model as a `TexturedModel`. Double click on the `TextureResources` operator and add the downloaded texture as a `ImageTexture`. 
Before drawing the  `TexturedModel`, add a `BindTexture` operator with the following parameters:

- `ShaderName` - TexturedModel
- `TextureName` - BlueMarble
- `TextureTarget` - Texture2D

Lastly add the `DrawTexturedModel` operator after the `BindTexture` operator, choosing the `TextureSphere` model under the `MeshName` property.

:::workflow
![Draw Textured Model](../workflows/draw-stimuli-textured-model.bonsai)
:::

> [!Note]
> A common use case for this node would be to create walls for VR environments with different textures and shapes. Bonvision comes with a inbuilt mesh called `Plane` that can be used for this purpose (it does not need to be loaded first with the `MeshResources` operator). Check out the VR corridor under the Demo section of the website for a walkthrough.

### DrawSceneModel

`DrawSceneModel` is used to display complex 3D scenes and models.  To use the `DrawSceneModel`, during the `Create Section` portion of the workflow the scene needs to be loaded using using the `SceneResources` operator.The `SceneResources` operator uses the [Open Asset Import Library (assimp)](https://github.com/assimp/assimp) for loading scene files. 

> [!Warning]
> Technically all the file formats supported by `assimp` should work, but certain custom properties (such as inbuilt animations/fancy custom effects/skeletons) are not supported. Loading Blender `.blend` files tends to be finicky and newer Blender version files are not supported. We recommend loading `.obj` files for now due to their simplicity.

> [!Warning]
> For complex scenes, `.obj` files often come packaged with a `.mtl` material file that contains definitions for how to shade/color the 3D model. The `SceneResources` operator will load both at once if it can find it. Technically, the `.mtl` file isn't a texture file, so when choosing a shader to render the model with, we choose the `Model` shader.

To try this example workflow out, download this 3D model [IronMan](https://free3d.com/3d-model/ironman-rigged-original-model--98611.html). Make sure both the `.obj` and `.mtl` file are in the same location. Double click on the `SceneResources` operator and add the downloaded model as a `SceneConfiguration`. Choose `Model` for the shader. Adjust the properties of the `PerspectiveView` to view  large 3D scenes and adjust the orientation if they appear misaligned.

:::workflow
![Draw Scene](../workflows/draw-stimuli-scene.bonsai)
:::





