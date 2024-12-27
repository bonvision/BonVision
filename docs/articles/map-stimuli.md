# Map Stimuli

## Sphere Mapping

When rendering 3D environments, in the case of 3D stimuli, `BonVision` applies by default a cubemap rendering approach and no additional transformation/operators are necessary.

However, in the case of 2D stimuli, we use `SphereMapping` to render them onto the inside of a 3D sphere to display them in a 3D space.
More info can be found in the [Display Environments](display-environment-basics.md)

![SphericalCoord](~/images/DisplayLogic/SphericalCoord_resized.png){width=500} 

This workflow below draws a checkboard which is mapped to a sphere using `SphereMapping`.

:::workflow
![Sphere Mapping](../workflows/overview-map-stimuli-define-display.bonsai)
:::

> [!NOTE]
> `SphereMapping` requires a `ViewWindow`.

> [!NOTE]
> When drawing stimuli, `SphereMapping` requires a `PublishSubject` and `SubscribeSubject` pair (see the [multiple stimuli](stimuli-multiple.md) for instructions on how to use `Subjects` and this [link](https://github.com/orgs/bonsai-rx/discussions/1835) for an explanation of the reasons).


