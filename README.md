# SimpleHUD
A Simple and customizable HUD system for VRChat.

Preview video: https://youtu.be/gmYI42rgkzo
(Outdated, not from v2.0)

## Info:
SimpleHUD is a HUD system I made for VRChat, because I wanted to make my own system that was easy to customize, had nice clean UI, and motion smoothing. So here it is! Free for anyone to use in their projects, and should be very simple to customize to your liking.

## Compatibility:
This system was developed and tested on Unity 2022.3.22f1. Should work on 2022.3.6f1 as well, and no testing done for Unity 2019.4.31f1.

## Features:
In this package, I include prefabs for a simple version, and an "extra" version. Each prefab includes a UI menu to change the HUD's options in-game, or if you don't want a UI with the HUD, simply delete "Settings" from the hierarchy. To change the default settings, refer to the bottom paragraph.

The "simple" version of the HUD system includes the HUD canvas, and one parent object for the text. The "extra" version of the prefab includes top, bottom, left, and right side text parents. This can be seen in the [Preview video](https://youtu.be/gmYI42rgkzo) linked here and at the top of this ReadMe.

### HUD System Simple menu:
<img width="995" height="1243" alt="Screenshot 2026-02-05 175529" src="https://github.com/user-attachments/assets/912d7ae8-9053-4090-a5da-f1a1e2c0ca28" />

### HUD System Extra menu:
<img width="1706" height="1240" alt="Screenshot 2026-02-05 175553" src="https://github.com/user-attachments/assets/57f5cb85-cd3c-487e-8ea3-7415a5b7ef64" />

By default, the HUD's options may not be what you prefer. To change the default values, enter playmode and select the object with the HUD Settings component in the hierarchy. Then, enable "Match default values to UI sliders," and mess with the UI sliders until the HUD looks good to you. Once the sliders are set, right click the HUD Settings component, select "Copy Component," exit playmode, right click the component again, and select "Paste Component As Values." This will copy the default values you set in playmode, and paste those values onto the component in edit mode, so that it saves.

If you need any help with this system, you may contact me directly through Discord DMs: "skull_z162", or you can join my Discord server: discord.gg/UUva6jUcPB
