# Multiple Stimuli
`BonVision` has three methods for drawing multiple stimuli in the same window.

## Array Primitives
For several stimuli primitives, `BonVision` includes array versions of these primitives that can be used to draw several of them simultaneously.

:::workflow
![Array Primitives](../workflows/array-primitives.bonsai)
:::

These primitives can only be used to draw duplicate stimuli but with different positions.

- Shawn's note - I would like to add more details here about generating the input for these primitives but I am unable to find an example workflow to copy from and have not been able to figure it out myself. I saw an example on https://bonsai-rx.org/docs/tutorials/scripting.html but have trouble getting the `CSharpTransform` operator to work.


## Using SelectMany to Pass An Array of Values
For finer control over the stimulus parameters, one can pass multiple values for each of the properties in a `DrawX` stimuli using a [SelectMany](https://bonsai-rx.org/docs/api/Bonsai.Reactive.SelectMany.html) operator.
In this example workflow, 

1) We have generated two ranges of values using a `ParameterRange` operator, which we then `Zip` and pass along to a `SelectMany` operator named `Create Gratings`. The `SelectMany` operator generates one observable sequence for each input then merges the results into a single sequence. 
2) Double click on the `SelectMany` operator to view the embedded workflow. Within the `SelectMany` operator, we use an `InputMapping` operator to map the two values to the `LocationX` and `Contrast` of a `DrawGrating` node. 
3) Lastly we use a `CombineLatest` operator to issue a `Draw` call for each element that is produced. The end result is a row of gratings with different contrast and position.

- Shawn's note - This is how I understand this example workflow I saw on the Bonvision Examples repo, not sure if the explaination is correct.

:::workflow
![SelectMany Example](../workflows/multiple-stimuli-selectmany.bonsai)
:::

> [!Note]
> The `DrawStimuli` nodes in the example workflow are a `PublishSubject` and `SubscribeSubject` pair which we will cover below.


## Publish and Subscribe Subject
For different stimuli, we can use the `PublishSubject` and `SubscribeSubject` operators to give multiple drawing commands to the `RenderFrame` operator.
[Subjects](https://bonsai-rx.org/docs/articles/subjects.html) are a special type of operator that allow reusing and sharing of observable sequences.

To get started:
1) Connect a `PublishSubject` operator to the viewport (in our example workflow below, a `NormalizedView`) and give it a name, like `DrawStimuli`.
2) Add multiple `SubscribeSubject` operators for each of the stimuli type you want to draw. In the `SubscribeSubject` operators, you will need to select the name you have given to the `Subject` you have created (in this example, `DrawStimuli`)
3) Connect `DrawX` operators to each `SubscribeSubject` operator

The end result should look something like this example workflow, which draws a circle and quad side by side.

:::workflow
![Publish and Subscribe](../workflows/multiple-stimuli-publish.bonsai)
:::

For a video walkthrough of this process, see this youtube video.

<div style="max-width: 500px">
<iframe width=100%  height="282" src="https://www.youtube.com/embed/NudcbbQ2AMg" title="BonVision quick tips: Presenting two stimuli on screen simultaneously" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>


### Presenting multiple stimuli sequentially 
To present multiple stimuli sequentially, we can use the same `PublishSubject` and `SubscribeSubject` operators but add timers to them. Timing will be covered in the [Timing Stimuli](stimuli-timing.md) article,