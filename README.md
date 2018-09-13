# BonVision

BonVision is a common workflow template and set of extensions, shaders and resources for quickly designing visual stimulation experiments in Bonsai. It also provides operators for generating parametric trial sequences, event logging, and gamma correction.

## Getting started

1. BonVision is currently built on top of the 2.4-preview release of Bonsai. You can download this release from the [official Bonsai website](https://bonsai-rx.org).
2. Install the `Bonsai.Numerics` package.
3. Open the `BonVision.bonsai` workflow to get started.
4. *** Required extensions of Bonsai to added

## Extensions

The following extensions are currently available in BonVision:

* `DrawGratings`: Draws parameterized 2D sinewave gratings, with support for interactive manipulation of spatio-temporal frequency, angle, location, size, envelope, etc.
* `DrawCheckerboard`: Draws parameterized checkerboards, with support for interactive manipulation of number of columns, rows, and grid phase used for flickering stimuli.
* `DrawImage`: Draws an affine transformed 2D image. Textures need to be added to the Shader configuration window first. Double-click on the `RenderFrame` node at the top and select the `Textures` tab. You can drag-and-drop arbitrary image files to the window. This operator also supports interactive manipulation of location, size, etc.
* `GammaCorrection`: Renders the current scene to a texture and applies gamma correction as a post-processing effect using a look-up table specified via an external image file.
* `MeshMapping`: Renders the current scene to a texture and applies mesh mapping and brightness correction as a post-processing effect using a mesh grid specified via an external calibration file.
* `ParameterRange`: Generates a sequence of parameter values between a specified min and max range. This operator can be useful for automating generation of parameterized trial sequences.
* `RangeAnimation`: Animates a sequence of parameter values between a min and max range at the specified cycles per second. This operator can be useful to specify continuous changes in parameter values.
* `GratingsSpecification`: Creates a sequence of grating parameters used for stimulus presentation. This operator can be useful for specifying parametric trial sequences.
* `LogEvent`: Logs the specified value into the common event file. This operator can be used for keeping a general record of session events in chronological order.

## Examples

* 'HandMapping'
* 'DirectionTuning'
* 'ContrastTuning'

