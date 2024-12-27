# Logging

By 'Logging' we refer to saving information relavant for the experiment. In general, there are four kinds of data one might want to log during an experiment: 
- Duration of the experiment and timing of each frame
- When were the stimuli presented
- What are the stimulus parameters used in the experiment
- Keeping track of external events

We created three nodes that can deal with all the three types of logging. These nodes are: `FrameEventLogger`, `EventLog` & `LogEvent`. The logger saves data to a .csv file and there are three aspects saved 

:::workflow
![All Loggers](../workflows/logging-all-loggers.bonsai)
:::

`FrameEventLogger`: This is used to setup a logger that can be used to  It needs to be placed after `RenderFrame`. The first time this is encountered, it starts the clocks, both the frame counter and time. It also opens the .csv file that is specified where the information is saved. A logger then becomes a behaviour subject that can be accessed in the rest of the workflow. 

> [!NOTE]
> You can start the logger either at the start of the experiment which will keep logs globally, or within a trial which will save data with respect to the relevant trial.

`EventLogger`: This is used to setup a logger that can be used to record stimulus/experiment parameters parameters but does not record frame event and timing.

`LogEvent`: This is actually used to save the data to the file. A `LogEvent` node subscribes to a `FrameEventLogger` or `EventLogger` subject, and writes to the corresponding .csv file three different aspects __Current frame index__, __Current Time__, __Logged information__.  The input to the `LogEvent` node can be the parameters that need to the saved (described below), and contain the format in which the data is saved. 

> [!WARNING]
> When there are multiple `FrameEventLogger` or `EventLogger` subjects, we need to ensure that `LogEvent` subscribes to the right subject to write to the correct file.

> [!WARNING]
> There are options to overwrite, append, etc for the files, so make sure you do not lose information by overwriting the files.

Here, we describe how to inplment the logging scenarios at the beginning of the article. 

### A. Logging screen update / refresh times
To log the frame index and the time at which every frame was presented, duplicate the `RenderFrame` node, connect a `FrameEventLogger` with a subject called `Frames` followed by a `LogEvent` node that subscribes to the `Frames` subject.

:::workflow
![Logging Frame Event](../workflows/logging-frame-event.bonsai)
:::

> [!WARNING]
> Although you can put the `FrameEventLogger` and `LogEvent` in the `DrawCircle` branch, in branches where there are conditional loops the `FrameEventLogger` might restart. Thus, its better to make a separate branch for logging events.


### B. Logging stimulus presentation
To log stimulus presentation, create a second `FrameEventLogger` branch with a new subject called `Stimulus`, but this time, place a `LogEvent` node that subscribes to the `Stimulus` subject after a `DrawX` stimuli node. In this example workflow, the stimuli is presented after 5 seonds, and the `LogEvent` only logs when the stimuli is onscreen.

:::workflow
![Logging Stimulus Presentation](../workflows/logging-stimulus-presentation.bonsai)
:::

> [!NOTE]
> This is a good example of starting multiple loggers to save different types of information in different files.


### C. Logging stimulus parameters
To log stimulus parameters, we would use the same method for logging stimulus presentation, but connect the `LogEvent` to the parameter that is being varied. 
:::workflow
![Logging Stimulus Parameters](../workflows/logging-stimulus-parameters.bonsai)
:::

### D. Logging external events
To log external events that are happening (such as keypresses or an animal carrying out a nose poke), the process is similar to that of logging stimuli presentation/presentation. Create a new `FrameEventLogger` branch, but in this case with a new subject called `Events`, and place a `LogEvent` node that subscribes to the `Events` subject after the keypresses or event you want to record.

> [!NOTE]
> The external events can be sampled at a different sampling frequency compared to the visual stimulus presentation. In the situation where the external data is at a higher rate, the frame index might not change as frequently, however, the time will remain precise

> [!NOTE]
> The two `LogEvents` after the keypresses can subscribe to the same `Events` subject so you do not need to create a separate file for every event.

:::workflow
![Logging Keydown](../workflows/logging-keydown.bonsai)
:::
