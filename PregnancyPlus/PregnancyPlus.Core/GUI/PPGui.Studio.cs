﻿using HarmonyLib;
using KKAPI.Chara;
using KKAPI.Studio;
using KKAPI.Studio.UI;
using KKAPI.Utilities;
using UniRx;
using UnityEngine;


namespace KK_PregnancyPlus
{
    //This partial class contatins all of the Studi GUI 
    public static partial class PregnancyPlusGui
    {
        internal static CurrentStateCategory cat;


        //String constants for the slider names, and GameObject identifiers
        internal const string inflationSize = "Pregnancy +";
        internal const string inflationMultiplier = "        Multiplier";
        internal const string inflationMoveY = "        Move Y";
        internal const string inflationMoveZ = "        Move Z";
        internal const string inflationStretchX = "        Stretch X";
        internal const string inflationStretchY = "        Stretch Y";
        internal const string inflationShiftY = "        Shift Y";
        internal const string inflationShiftZ = "        Shift Z";
        internal const string inflationTaperY = "        Taper Y";
        internal const string inflationTaperZ = "        Taper Z";
        internal const string inflationClothOffset = "        Cloth Offset";
        internal const string inflationFatFold = "        Fat Fold";
        internal const string inflationFatFoldHeight = "        Fat Fold Height";
        internal const string inflationRoundness = "        Roundness";
        internal const string inflationDrop = "        Drop";
        private const string blendshapeText = "Open BlendShapes";
        private const string smoothBellyMeshText = "Belly Mesh Smoothing (Give it a second)";
        private const string smoothClothMeshText = "Include cloth when smoothing";


        internal static void InitStudio(Harmony hi, PregnancyPlusPlugin instance)
        {
            if (StudioAPI.InsideStudio)
            {
                RegisterStudioControls();
            }
        }


        private static void RegisterStudioControls()
        {
            cat = StudioAPI.GetOrCreateCurrentStateCategory("Pregnancy +");

            cat.AddControl(new CurrentStateCategorySwitch("Reset P+ Shape", c =>
                {                     
                    return false;
                }))
                .Value.Subscribe(f => {
                    if (f == false) return;
                    ResetAllSliders();                   
                });
            
            cat.AddControl(new CurrentStateCategorySwitch("Restore Last P+ Shape", c =>
                {                                         
                    return false;
                }))
                .Value.Subscribe(f => {
                    if (f == false) return;
                    if (PregnancyPlusPlugin.lastBellyState.HasAnyValue()) RestoreSliders(PregnancyPlusPlugin.lastBellyState);
                 });

            cat.AddControl(new CurrentStateCategorySwitch(blendshapeText, c =>
                {                                         
                    return false;
                }))
                .Value.Subscribe(f => {
                    if (f == false) return;
                    //Open blendshape GUI on click
                    foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                    {             
                        ctrl.OnOpenBlendShapeSelected();                                                   
                    }
                });

            cat.AddControl(new CurrentStateCategorySlider(inflationSize, c =>
                {   
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationSize : 0;

                }, 
                    SliderRange.inflationSize[0], 
                    SliderRange.inflationSize[1]
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationSize == f) continue;    
                            ctrl.infConfig.inflationSize = f;       
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });

            cat.AddControl(new CurrentStateCategorySlider(inflationMultiplier, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationMultiplier: 0;

                }, 
                    SliderRange.inflationMultiplier[0], 
                    SliderRange.inflationMultiplier[1]
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationMultiplier == f) continue;     
                            ctrl.infConfig.inflationMultiplier = f;    
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });     

            cat.AddControl(new CurrentStateCategorySlider(inflationRoundness, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationRoundness: 0;
                    
                }, 
                    SliderRange.inflationRoundness[0] * scaleLimits, 
                    SliderRange.inflationRoundness[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationRoundness == f) continue;                    
                            ctrl.infConfig.inflationRoundness = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });                                

            cat.AddControl(new CurrentStateCategorySlider(inflationMoveY, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationMoveY: 0;

                }, 
                    SliderRange.inflationMoveY[0] * scaleLimits, 
                    SliderRange.inflationMoveY[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationMoveY == f) continue;                    
                            ctrl.infConfig.inflationMoveY = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });
            
            cat.AddControl(new CurrentStateCategorySlider(inflationMoveZ, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationMoveZ: 0;

                }, 
                    SliderRange.inflationMoveZ[0] * scaleLimits, 
                    SliderRange.inflationMoveZ[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationMoveZ == f) continue;   
                            ctrl.infConfig.inflationMoveZ = f;     
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });

            cat.AddControl(new CurrentStateCategorySlider(inflationStretchX, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationStretchX: 0;

                }, 
                    SliderRange.inflationStretchX[0] * scaleLimits, 
                    SliderRange.inflationStretchX[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationStretchX == f) continue;                    
                            ctrl.infConfig.inflationStretchX = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });

            cat.AddControl(new CurrentStateCategorySlider(inflationStretchY, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationStretchY: 0;
                    
                }, 
                    SliderRange.inflationStretchY[0] * scaleLimits, 
                    SliderRange.inflationStretchY[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationStretchY == f) continue;                    
                            ctrl.infConfig.inflationStretchY = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });        
            
            cat.AddControl(new CurrentStateCategorySlider(inflationShiftY, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationShiftY: 0;

                }, 
                    SliderRange.inflationShiftY[0]  * scaleLimits, 
                    SliderRange.inflationShiftY[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationShiftY == f) continue;                    
                            ctrl.infConfig.inflationShiftY = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });

            cat.AddControl(new CurrentStateCategorySlider(inflationShiftZ, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationShiftZ: 0;

                }, 
                    SliderRange.inflationShiftZ[0] * scaleLimits, 
                    SliderRange.inflationShiftZ[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationShiftZ == f) continue;                    
                            ctrl.infConfig.inflationShiftZ = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });

            cat.AddControl(new CurrentStateCategorySlider(inflationTaperY, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationTaperY: 0;
                    
                }, 
                    SliderRange.inflationTaperY[0] * scaleLimits, 
                    SliderRange.inflationTaperY[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationTaperY == f) continue;                    
                            ctrl.infConfig.inflationTaperY = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });

            cat.AddControl(new CurrentStateCategorySlider(inflationTaperZ, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationTaperZ: 0;
                    
                }, 
                    SliderRange.inflationTaperZ[0] * scaleLimits, 
                    SliderRange.inflationTaperZ[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationTaperZ == f) continue;                    
                            ctrl.infConfig.inflationTaperZ = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });       

            cat.AddControl(new CurrentStateCategorySlider(inflationDrop, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationDrop: 0;
                    
                }, 
                    SliderRange.inflationDrop[0], 
                    SliderRange.inflationDrop[1]
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationDrop == f) continue;                    
                            ctrl.infConfig.inflationDrop = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });       

            cat.AddControl(new CurrentStateCategorySlider(inflationClothOffset, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationClothOffset: 0;
                    
                }, 
                    SliderRange.inflationClothOffset[0] * scaleLimits, 
                    SliderRange.inflationClothOffset[1] * scaleLimits
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationClothOffset == f) continue;                    
                            ctrl.infConfig.inflationClothOffset = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });
                    
            cat.AddControl(new CurrentStateCategorySlider(inflationFatFold, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationFatFold: 0;
                    
                }, 
                    SliderRange.inflationFatFold[0], 
                    SliderRange.inflationFatFold[1]
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationFatFold == f) continue;                    
                            ctrl.infConfig.inflationFatFold = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });
                    
            cat.AddControl(new CurrentStateCategorySlider(inflationFatFoldHeight, c =>
                {                                       
                    var ctrl = GetCharCtrl(c);                                                   
                    return ctrl != null ? ctrl.infConfig.inflationFatFoldHeight: 0;
                    
                }, 
                    SliderRange.inflationFatFoldHeight[0], 
                    SliderRange.inflationFatFoldHeight[1]
                ))
                    .Value.Subscribe(f => { 
                        foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                        {  
                            if (ctrl.infConfig.inflationFatFoldHeight == f) continue;                    
                            ctrl.infConfig.inflationFatFoldHeight = f;
                            ctrl.MeshInflate(new MeshInflateFlags(ctrl), "StudioSlider");                             
                        }
                    });

            cat.AddControl(new CurrentStateCategorySwitch(smoothBellyMeshText, c =>
                {                                         
                    return false;
                }))
                .Value.Subscribe(f => {
                    if (f == false) return;
                    //Try to apply smoothing to belly mesh on click
                    foreach (var ctrl in StudioAPI.GetSelectedControllers<PregnancyPlusCharaController>()) 
                    {             
                        ctrl.ApplySmoothing(includeClothSmoothing);                                                   
                    }
                });

            cat.AddControl(new CurrentStateCategorySwitch(smoothClothMeshText, c =>
                {                                         
                    return false;
                }))
                .Value.Subscribe(f => {
                    includeClothSmoothing = f;
                });
                    
        }


        //Reduce, reuse, recycle methods
        internal static PregnancyPlusCharaController GetCharCtrl(Studio.OCIChar c) 
        {
            if (c.charInfo == null) return null;
            var controller = c.charInfo.GetComponent<PregnancyPlusCharaController>();
            if (controller == null) return null;    
            return controller;
        }


        //Reset all sliders to 0 on Reset btn click
        internal static void ResetAllSliders(float resetTo = 0) 
        {
            if (cat == null) return;

            //For each ui item check if its a slider
            foreach(CurrentStateCategorySubItemBase subItem in cat.SubItems) 
            {
                if (!subItem.Created) continue;
                var itemGo = subItem.RootGameObject;
                var sliders = itemGo.GetComponentsInChildren<UnityEngine.UI.Slider>();

                //For each slider component (should just be one per subItem) set to 0
                foreach(var slider in sliders) 
                {
                    slider.value = resetTo;
                }
            }
        }


        //Reset a single slider
        internal static void ResetSlider(string sliderName, float resetTo = 0) 
        {
            if (cat == null) return;

            //For each ui item check if its a slider
            foreach(CurrentStateCategorySubItemBase subItem in cat.SubItems) 
            {
                if (!subItem.Created) continue;
                var itemGo = subItem.RootGameObject;
                var sliders = itemGo.GetComponentsInChildren<UnityEngine.UI.Slider>();

                //For each slider component (should just be one per subItem) set to 0
                foreach(var slider in sliders) 
                {
                    if (slider.name == "Slider " + sliderName) slider.value = resetTo;
                }
            }
        }


        //Restore sliders to last non zero config
        internal static void RestoreSliders(PregnancyPlusData _infConfig) 
        {
            if (cat == null) return;            

            //For each ui item check if its a slider
            foreach(CurrentStateCategorySubItemBase subItem in cat.SubItems) 
            {
                if (!subItem.Created) continue;
                var itemGo = subItem.RootGameObject;
                var sliders = itemGo.GetComponentsInChildren<UnityEngine.UI.Slider>();

                //For each slider component (should just be one per subItem) set to last good value
                foreach(var slider in sliders) 
                {
                    if (PregnancyPlusPlugin.DebugLog.Value) PregnancyPlusPlugin.Logger.LogInfo($" Restoring slider > {slider.name}");

                    //Set the correct slider with it's old config value
                    switch (slider.name) 
                    {
#region Look away! im being lazy                        
                        case "Slider " + inflationSize:
                            slider.value = _infConfig.inflationSize;
                            continue;

                        case "Slider " + inflationMultiplier:
                            slider.value = _infConfig.inflationMultiplier;
                            continue;

                        case "Slider " + inflationMoveY:
                            slider.value = _infConfig.inflationMoveY;
                            continue;

                        case "Slider " + inflationMoveZ:
                            slider.value = _infConfig.inflationMoveZ;
                            continue;

                        case "Slider " + inflationStretchX:
                            slider.value = _infConfig.inflationStretchX;
                            continue;

                        case "Slider " + inflationStretchY:
                            slider.value = _infConfig.inflationStretchY;
                            continue;

                        case "Slider " + inflationShiftY:
                            slider.value = _infConfig.inflationShiftY;
                            continue;

                        case "Slider " + inflationShiftZ:
                            slider.value = _infConfig.inflationShiftZ;
                            continue;

                        case "Slider " + inflationTaperY:
                            slider.value = _infConfig.inflationTaperY;
                            continue;

                        case "Slider " + inflationTaperZ:
                            slider.value = _infConfig.inflationTaperZ;
                            continue;

                        case "Slider " + inflationClothOffset:
                            slider.value = _infConfig.inflationClothOffset;
                            continue;

                        case "Slider " + inflationFatFold:
                            slider.value = _infConfig.inflationFatFold;
                            continue;

                        case "Slider " + inflationFatFoldHeight:
                            slider.value = _infConfig.inflationFatFoldHeight;
                            continue;

                        case "Slider " + inflationRoundness:
                            slider.value = _infConfig.inflationRoundness;
                            continue;

                        case "Slider " + inflationDrop:
                            slider.value = _infConfig.inflationDrop;
                            continue;

                        default:
                            continue;
#endregion
                    }
                    
                }
            }
        }


        //Reset a toggle (Breaks the game currently lol)
        internal static void ResetToggle(string toggleName, bool desiredState = false) 
        {
            if (cat == null) return;

            //For each ui item check if its a toggle
            foreach(CurrentStateCategorySubItemBase subItem in cat.SubItems) 
            {
                if (!subItem.Created || !subItem.Name.Contains(toggleName)) continue;
                
                var itemGo = subItem.RootGameObject;
                var sliders = itemGo.GetComponentsInChildren<UnityEngine.UI.Toggle>();

                //For each toggle item (should just be one per subItem), set the desited state
                foreach(var slider in sliders) 
                {
                    slider.isOn = desiredState;
                }
            }
        }

    }
}
