# Animating Stimuli

Basic animation in `BonVision` involves manipulating one or more properties in a `DrawX` node. For instance, to make a circle 
move across the screen, you would vary the `LocationX` parameter in a `DrawCircle` node.

To get started:
1) Right click on any `DrawX` node and externalize the property you want to manipulate.
2) `BonVision` provides a `RangeAnimation` operator to generate a linear range of values for a specified duration of time. Connect a `RangeAnimation` operator to the parameter you externalized and change the `RangeBegin`, `RangeEnd` and `Duration`.
3) Add a `Repeat` operator if you want the animation to repeat between the `RangeAnimation` and `DrawX` node.

The workflow will look something like this (which shows a circle moving across the screen repeatedly)

:::workflow
![Animating Circle](../workflows/animating-stimuli-circle-locationx.bonsai)
:::

> [!Note]
> The `RangeBegin` and `RangeEnd` parameter depends on the [drawing region](drawing-region.md). For a `NormalizedView` -1 to 1 represents the edges of the screen.

> [!Note]
> To vary more than 1 parameter at a time if they share the same `RangeAnimation` parameters, you can externalize more than 1 property at a time and they will be grouped together.
> To prevent parameters from being grouped together, connect a `RangeAnimation` operator to the first externalized property before externalizing a second property.

<div style="max-width: 500px">
<iframe width=100% height=282 src="https://www.youtube.com/embed/YUNJNsJj6co" title="BonVision quick tips: Making a animated feature" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>

For an additional example of this, see the `Looming/Sweeping` demo in the [Demo](/demos/VR-demo.md) page.










