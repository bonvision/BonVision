# Syncing

In this article, we will cover how to sync `BonVision` experiments with other systems (e.g. neural recordings) with a photodiode, which has been used historically in vision neuroscience. While there are alternative methods of syncing (such as through TTL pulses which can be sent through `Bonsai`), syncing using a photodiode is a bit more precise and closer to ground truth because we know precisely when the stimulus is presented on the screen rather than when the computer has sent out a signal to update the display (which might have some delay).

To sync with a photodiode, we have a small square in the corner of the screen (that is not visible to the subject) which we flip between black and white, which we can then observe with a photodiode.

To set this up:

1) Use a `DrawQuad` to draw a square and position the square in the corner of the screen.
2) Externalize the `ColorR`, `ColorG` and `ColorB` properties of the `DrawQuad` operator to control the color of the square.
3) Create a `BehaviourSubject` operator and name it `QuadState`, set a default `Float` value of 0 (for black), and link a `SubscribeSubject` operator subscribing to `QuadState` to the externalized `ColorR`, `ColorG` and `ColorB` properties.
4) Within our pipeline for drawing a visual stimulus, we can add a [`Sink`](https://bonsai-rx.org/docs/articles/operators.html#sink) operator that updates `QuadState` whenever the stimulus is presented or when the stimulus is turned off. `Sink` operators do not modify the input or output items in any way but can trigger an external action.

This example workflow shows a directional tuning stimulus together with a flashing square for syncing with a photodiode. You will need to expand the `CreateObservable` -> `DirStim` grouped workflows to see the `Sink` operators (named `UpdatePD`) which are embedded in the visual stimulus drawing pipeline.

:::workflow
![Syncing with photodiode](../workflows/syncing-pd.bonsai)
:::

For a video walkthrough of this process, check out this youtube video.

<div style="max-width: 500px">
<iframe width=100% height="282" src="https://www.youtube.com/embed/VzWwP04z48c" title="BonVision LiveCodingSession: Synching with photodiode" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</div>

