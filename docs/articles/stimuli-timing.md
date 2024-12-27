# Timing Stimuli
`Bonsai` provides several operators that can be used in conjunction with `BonVision` operators to control the timing of stimuli presentation.

## Triggering Stimuli
To control the onset of stimuli, use the `SubscribeWhen` operator, which initiates the draw sequence when it detects a notification from a linked operator.
Starting from a basic `DrawCircle` workflow, add a `SubscribeWhen` operator after the `DrawCircle` operator.
Connect a `Timer` operator to the `SubscribeWhen` operator and adjust the `DueTime` property to control when the stimuli comes on.

This workflow draws a circle after five seconds has elapsed.

:::workflow
![Triggering Stimuli](../workflows/timing-stimuli-subscribewhen.bonsai)
:::

> [!Warning]
> When choosing a `Timer` operator, you might see different versions of it in `Bonsai`.
> Choose the `Bonsai.Shaders` version. See the [FAQ](faq.md) for an indepth discussion of when to choose between duplicate operators.

## Terminating Stimuli
To control the duration of stimuli, use a `TakeUntil` operator, which terminates the draw sequence when it detects a notification from a linked operator.
Simply replace the `SubscribeWhen` operator with the `TakeUntil` operator.

This workflow starts with a drawn circle, and the circle dissappears after five seconds.

:::workflow
![Terminating Stimuli](../workflows/timing-stimuli-takeuntil.bonsai)
:::


## Combining Triggering and Terminating
Combine the operators together to control both the onset and offset of stimuli. 

> [!Warning]
> The order of operators is important and the sequence goes from right to left. Reversing the operators will cause the sequence to not produce any output.

This workflow draws a circle after two seconds has elapsed, and the circle remains on screen for two seconds.

:::workflow
![Triggering and Terminating Stimuli](../workflows/timing-stimuli-combined-takeuntil-subscribewhen.bonsai)
:::

## Looping Stimuli
To loop stimuli, add a `Repeat` operator to the end of the sequence. 

This workflow loops a circle for two seconds on and two seconds off.

:::workflow
![Looping Stimuli](../workflows/timing-stimuli-looping.bonsai)
:::

> [!Note]
> If you want the stimuli to repeat a set number of times instead of infinitely, use a `RepeatCount` operator instead.

## Controlling Timing of Multiple Stimuli
To control the presentation of multiple stimuli, use a `PublishSubject` operator in conjunction with a `SubscribeSubject` operator to draw multiple stimuli as outlined in the [Multiple Stimuli](stimuli-multiple.md) article.
For each `DrawStimuli` branch, add separate `SubscribeWhen`, `TakeUntil` and `Repeat` operators. 
Since the branches are separate, they can have different timing progressions.

This workflow below draws a circle on the left and a quad on the right with different timings for both.

:::workflow
![Timing Multiple Stimuli](../workflows/timing-stimuli-multiple.bonsai)
:::


## Sequential Presentation of Multiple Stimuli
For sequential presentation of multiple stimuli that are not present at the same time, one can use the `Concat` operator to join the 
multiple branches of `DrawStimuli`. The `Concat` operator combines any number of observable sequences as long as each sequence terminates successfully.

> [!Note]
> The `Repeat` operator is moved out of each branch and added to the `Concat` operator. 
> Leaving it in a branch causes that branch to loop and not terminate successfully, which prevents the execution of the `Concat` step.

The workflow below alternates between a circle on the left and a quad on the right.
:::workflow
![Sequential Presentation of Stimuli](../workflows/timing-stimuli-sequential.bonsai)
:::


## Controlling Stimuli Presentation with Events instead of Time
Finally, both the `SubscribeWhen` and `TakeUntil` operators can be linked to other operators to control stimuli onset and offset using events rather than time. For instance, by replacing 
the `Timer` operator with a `KeyDown` operator, stimuli presentation can be linked to keypresses.

This workflow draws a circle when the `space bar` is pressed, and the circle disappears when the `a` button is pressed.
:::workflow
![Timing Stimuli with Keypresses](../workflows/timing-stimuli-keydown.bonsai)
:::

This also opens up possibilities for close-loop presentation of visual stimuli. For instance, visual stimuli can be linked to lever presses for operant conditioning tasks, as well as to 
subject movement (for instance when an animal crosses into an arena). 

* Shawn's note might be worth a separate expanded "close-loop" article.


## Video Walkthrough
For a video walkthrough of many of the steps covered in this article, check out this youtube video.
<div style="max-width: 500px">
<iframe width=100% height="282" src="https://www.youtube.com/embed/yHQFbI69UHw" title="BonVision quick tips: Timing a stimulus" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>








