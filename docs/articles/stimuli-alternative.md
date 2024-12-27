# Alternative Stimuli Creation
## Dynamic Textures
In addition to loading premade textures and videos, `BonVision` can display dynamic textures generated online or from other sources. 

In this example workflow, we use dynamic textures to display a webcam feed.

1) Double click the `TextureResources` node and create a `Texture2D` resource and name it `DynamicVideo`.

2) Add a basic Bonvision draw image workflow with a `DrawImage` node drawing the texture `DynamicVideo`.

3) Add a `CameraCapture` node and feed the output to a `UpdateTexture` node.

:::workflow
![Dynamic Textures](../workflows/alternative-stimuli-dynamictexture.bonsai)
:::

## Loading from CSV

Alternatively, one can also load stimuli parameters from a CSV file.
In this example workflow, we load grating stimuli parameters from a CSV file to draw a series of grating stimuli.

1) Load a CSV file with 4 columns for `Orientation`, `TemporalFrequency`, `Contrast` and `Duration` using the `CsvReader` node.

2) Use an `ExpressionTransform` node with the script below to parse our integers as variables:

```C#
new(Double.Parse(it[0]) * Math.PI/180 as Orientation,
Double.Parse(it[1]) as TemporalFrequency,
Double.Parse(it[2]) as Contrast,
Double.Parse(it[3]) as Duration
)
```

3) Use an `InputMapping` node to match the variables from the output of the `ExpressionTransform` node to the properties of the `CreateGratingTrial` node (In the example workflow, because we have matched the variables the `InputMapping` node takes on the name of the variables being mapped).

:::workflow
![Loading from CSV](../workflows/alternative-stimuli-csv-loading.bonsai)
:::

- Shawn's note Does this only apply to grating stimuli? or can this be used for other stimuli too?

## Loading from scripting

* to be added