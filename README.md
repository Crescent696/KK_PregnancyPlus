# Introduction
This repository contains the PregnancyPlus plugin, that adds additional belly sliders in Studio and Maker.  It is intended to compliment the [KK_Pregnancy](https://github.com/ManlyMarco/KoikatuGameplayMods) plugin, but can be used without it.   (For example: HS2)  
The belly shape will persist in any game mode when slider values are saved to the characters card, or scene.

** 
 This project now has all the features I originally intended, so nothing new in the pipeline.  I will continue to provide bugfixes and support. 
**

<img src="https://github.com/thojmr/KK_PregnancyPlus/blob/master/images/result.png" height="575"></img>
<img src="https://github.com/thojmr/KK_PregnancyPlus/blob/master/images/P%2BMakerGUI.PNG" height="575"></img>
<img src="https://github.com/thojmr/KK_PregnancyPlus/blob/master/images/P%2BStudioGUI.PNG" width="476.5"></img>

## Features
- Adds a number of sliders that will allow you to change the size and shape of a characters belly in Studio, Maker, and Main Game.
    - Slider values will save to scene or character card.
    - In the main game you can further tweak all characters belly shapes with the F1 plugin config sliders.
- Adds Timeline (KK) and VNGE (HS2/AI) integration for animating the belly by creating blendshapes that save to character card (see specific features below).
- Adds 3 configurable keybinds in plugin config that can be used to increase or decrease the belly size in Main Game, on the fly.   
- Substitutes for stomach bulges/fat bellies as well, but it's original intent is pregnancy.    
- Adds a "Main Game" mode config option.  Disable if you want to turn off this plugins' features temporarily while playing.
- Adds a "Mesh Smoothing" button in Studio and Maker, that allows you to smooth the belly mesh and reduce any edges resulting from some slider combinations.
    - The smoothing will reset on slider change or character load, so it's mostly for screenshots, animations, and blendshapes.
    - The smoothed shape will be captured by blendshapes!
    - Optionally you can smooth any skin tight clothing as well, to help reduce clipping while smoothing, but clothing doesn't always look great afterwords.
- Somewhat compatible with "[ddaa33] Pregnant plugin (ShapeKeyPlugin)" if you wish to combine the effects of both, you can.  But the effects applied by that plugin will not save to the character card by default. (Only with Pregnancy+ captured blendshapes)

## Koikatsu and AI Only Features
- When using the KK/AI_Pregnancy `inflation` or `pregnancy` features, any saved Pregnancy+ belly sliders will be added in addition to KK/AI_Pregnancy's shape, but only both pregnancy plugins are installed.  You can control the amount of Pregnancy+ belly shape applied on top of the KK/AI_Pregnancy belly with the "Max Additional Belly Size" slider in plugin config. 0 being no additional Preg+ slider effect, and max being the full Preg+ slider effect.  You can use the plugin config sliders to further adjust the results for all pregnant characters at once.
    - This can be used to tweak the final pregnant shape, or make the max size larger than what KK/AI_Pregnancy allows.
    - Toggle this feature off by setting "Max Additional Belly Size" to 0.
- *Key thing to note*: If you just want to alter the KK/AI_Pregnancy shape.  Make sure to set the "Inflation Size" slider to 0 before saving character card.  When it is above 0 it will always be visible in Main Game even when not pregnant. 

## Koikatsu Only Features
- Studio Timeline integration via blendshapes.  Here's how to:
  - Set the P+ character sliders to the desired shape you want (including belly smoothing).
  - Click "Open Blendshapes" button. You will see a popup that will show you any existing P+ blendshapes.  If none are found, then use the "Create New" button.
    - Do not alter the Preg+ blendshapes ending in \[temp\] They are temporary and will not be saved.
  - Move your new blendshape sliders to the desired position.  At least one blendshape slider must be green (touched) before the next step
  - Open Timeline with Ctrl+T, search for "Blendshape" and middle click to add.
  - Follow Timeline guides for further info

## Honey Select 2 and AI Only Features
- Studio VNGE integration via blendshapes.  Here's how to:
  - Set the P+ character sliders to the desired shape you want (including belly smoothing).
  - Click "Open Blendshapes" button. You will see a popup that will show you any existing P+ blendshapes.  If none are found, then use the "Create New" button.
    - Do not alter the Preg+ blendshapes ending in \[temp\] They are temporary and will not be saved.
  - Move your new blendshape sliders to the desired position.  At least one blendshape slider must be green (touched) before the next step
  - Open VNGE, and add the blendshape(s) under "Clip Manager" 
    - To use BlendShapes in VNGE set ExportChara_XXPE_BlendShapes=1 in vnactor.ini everywhere it is found (may become obsolete)
  - Follow VNGE guides for further info

## Bigger!
- For additonal effect in HS2 you can mimic what KK_Pregnancy does to belly bones to make the final shape larger/rounder
- Here's how 
    - In HS2 or AI: Studio > HS2PE > Adv.mode > Bones
    - Bone: cf_J_kosi01_s  Set the following: PositionZ: 0.6, ScaleZ: 1.6, RotateX: 11
    - Bone: cf_spine01_s  Set the following: PositionZ: 0.6, ScaleZ: 1.6, RotateX: 351
    - Now apply any P+ sliders you want
- This will however make slight changes to the characters spine shape, so keep that in mind.  
- Also since this is altering bones, you may have some unintended cosequences down the road.

## FAQ - Troubleshooting
- Q: Where do I put the PregnancyPlus.dll?
    - A: It should end up under {Root game folder}/BepinEx/Plugins/xx_PregnancyPlus.dll
- Q: Why are some outfits not affected by the sliders?
    - A: Some outifts in Unity are marked as not readable, and the mesh of these outfits can not be altered at runtime.
- Q: Some of the sliders are not working?
    - A: First make sure you don't have the debug mesh option enabled.  Then try adjusting your 'Move Y' 'Move Z' sliders to make sure their centers are not outside your characters body.  Third, make sure P+ gameplay is enabled in Plugin Config, and on the character card (It will be by default).  Worst case scenario you can try turning on Preg+ debug logging to look for any errors in the Plugin Config and report to me. 
- Q: The belly size is suddenly changing when the character moves, or the first time I adjust a slider.
    - A: The default belly size is calculated based on the hip and rib bone width.  In rare cases It can be due to strange character animations, or character size adjustments.   
- Q: There are no slider effects when the character has no legs.
    - A: The character must have a leg scale > 0 for the belly sliders to work correctly.
- Q: The heck is a BlendShape?
    - A: Put simply a blendshape is a copy of the mesh that has some deformation that you want to be able to slide into.  Like visually morphing from originalMesh -> targetMesh (Preggo).
- Q: The belly size/shape is different in Maker than in Studio?
    - A: This was fixed in v3.5 and some cards with belly shapes before that version need to be re-saved again.  Only old characters with ABMX adjustments to the torso will have this issue.

## Notes
- There will be cloth clipping.  You can use the Cloth Offset slider to help with it, but It's a difficult problem to solve.

## How to download
You can grab the latest plugin release [here](https://github.com/thojmr/KK_PregnancyPlus/releases), or build it yourself (developers only).  Explained further below.
This plugin works in Koikatsu, Honey Select 2, and AI.  Grab the KK zip for Koikatsu, HS2 zip for Honey Select 2, and AI zip for AI [here](https://github.com/thojmr/KK_PregnancyPlus/releases)

## How to install
1. Make sure you have at least BepInEx 5.4.4 and KKAPI 1.17 for PregnancyPlus v3.0+
2. Download the latest release of the plugin you want [here](https://github.com/thojmr/KK_PregnancyPlus/releases).
3. Extract the zip archive into your root game directory. The plugin .dll will end up inside your BepInEx\plugins\ directory.
    - like {root game}/BepInEx/plugins/XX_PregnancyPlus.dll
4. Check if there are no warnings on game startup, if the plugin loaded it should appear in Plugin Config.

## (Developers only) Compiling with Visual Studio 2019 (The official way)
<details>
  <summary>Click to expand</summary>
 Simply clone this repository to your drive and use the free version of Visual Studio 2019 for C# to compile it. Hit build and all necessary dependencies should be automatically downloaded. Check the following links for useful tutorials. If you are having trouble or want to try to make your own plugin/mod, feel free to ask for help in modding channels of either the [Koikatsu](https://discord.gg/hevygx6) or [IllusionSoft](https://discord.gg/F3bDEFE) Discord servers.
- https://help.github.com/en/github/creating-cloning-and-archiving-repositories/cloning-a-repository
- https://docs.microsoft.com/en-us/visualstudio/get-started/csharp/?view=vs-2019
- https://docs.microsoft.com/en-us/visualstudio/ide/troubleshooting-broken-references?view=vs-2019
 </details>

## (Developers only) Compiling with Visual Studio Code (Not the suggested way, but my way)
<details>
  <summary>Click to expand</summary>
Simply clone this repository to your drive and use Visual Studio Code.  
Install the C# extension for VSCode. 
Make sure the following directory exists `C:/Program Files (x86)/Microsoft Visual Studio/2019/Community/MSBuild/Current/Bin/msbuild.exe`.  
If not you will need to install the VS2019 MS build tools (There may be other ways to build, but this is the one that eventually worked for me)
Install nuget.exe and set the environment path to it. 
Use `nuget install -OutputDirectory ../packages` to install the dependancies from the \KK_PregnancyPlus.csproj directory.  
Finally create a build script with tasks.json in VSCode.
If you see a .net version error, you will need to install that version of .net development kit (probably 3.5 for KK)
Example build task:
```json
{
    "label": "build-KK_PregnancyPlus",
    "command": "C:/Program Files (x86)/Microsoft Visual Studio/2019/Community/MSBuild/Current/Bin/msbuild.exe",
    "type": "process",
    "args": [
        "${workspaceFolder}/KK_PregnancyPlus/KK_PregnancyPlus.csproj",
        "/property:GenerateFullPaths=true;Configuration=Debug",
        "/consoleloggerparameters:NoSummary"
    ],
    "presentation": {
        "reveal": "silent"
    },
    "problemMatcher": "$msCompile",
},
{
    "label": "build-and-copy",
    "type": "shell",
    "command": "cp ./bin/KK_PregnancyPlus.dll '<KK_Install_DIR>/BepInEx/plugins/'",
    "dependsOn": "build-KK_PregnancyPlus",
    "group": {
        "kind": "build",
        "isDefault": true
    },
    "presentation": {
        "echo": true,
        "reveal": "silent",
        "focus": false,
        "panel": "shared",
        "showReuseMessage": true,
        "clear": false
    }
}
```
If sucessfull you should see a KK_PregnancyPlus.dll file nested in .\bin\
</details>

### Some KK_PregnancyPlus technical details
- Instead of manipulating the bones like KK_Pregnancy does, this mod alters the mesh itself via computed blendshapes which has benefits and drawbacks
    - A blendshape is generated at runtime for every mesh near the belly.  The sliders alter the shape of the pre calculated blendshape before re-applying it. 
- Integrates with KK/AI_Pregnancy in Story Mode so that both plugins can work together.  This can be configured in plugin config

### Some of the drawbacks of generating blendshapes instead of the manipulating bones directly
- Right now clothing can be hit or miss, because of the way the belly grows the mesh loses its local positional data causing clipping.  With bone scaling, clothes shift automagically via bone weights which usually results in less clipping.
    - There are some clothing items in HS2 and AI that simply wont work at all with blendshapes because they are marked as not readable in Unity
- Acessories won't automatically move with the mesh as they do when you manipulate bones
- It has bigger impact on performance (only when changing a slider) because of the computation it has to perform. However once the shape is calculated the performance is equally as fast as bone manipulation.
- Unity doesn't have great blendshape support in older versions like KK is running on.
- Since blendshapes are tied to a single mesh, if the mesh is changed (like uncensors), any saved blendshape will become invalid, and a new blendshape will need to be made.
