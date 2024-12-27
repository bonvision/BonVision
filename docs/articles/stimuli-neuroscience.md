# Neuroscience Stimuli

## Sparse Noise

The `SparseNoise` operator generates a non-overlapping discrete sparse grid of randomly activated quads.

Drawing the stimuli produced from the `SparseNoise` operator is a little different from the other `DrawX` stimuli. 

1) Add a `Texture2D` resource called `DynamicVideo` using the `TextureResource` operator. 
2) Add a `DrawImage` operator that renders the `DynamicVideo` texture.
3) Connect a `UpdateTexture` operator to the `SparseNoise` operator and choose the `DynamicVideo` texture to update.

:::workflow
![SparseNoise](../workflows/neuroscience-stimuli-sparse-noise.bonsai)
:::

- Add logging information?


## Gratings
As grating stimuli are commonly used in visual physiology experiments, we have created a few specialized nodes that make it convenient to design grating experiments very easily. 

- Shawn's note - currently some of these nodes appear to be broken, awaiting feedback on https://github.com/bonsai-rx/bonsai/issues/1832

### Nodes to document
`GratingsSpecification`, `CreateGratingTrial`, `GratingSequence`, `DrawGrating`



