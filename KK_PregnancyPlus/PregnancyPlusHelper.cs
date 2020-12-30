﻿using KKAPI;
using KKAPI.Chara;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace KK_PregnancyPlus
{
    internal static class PregnancyPlusHelper
    {

        internal static float FastDistance(Vector3 firstPosition, Vector3 secondPosition) 
        {
            //Calculates distance faster than vector3.distance.
            Vector3 heading;
            float distanceSquared;
    
            heading.x = firstPosition.x - secondPosition.x;
            heading.y = firstPosition.y - secondPosition.y;
            heading.z = firstPosition.z - secondPosition.z;
    
            distanceSquared = heading.x * heading.x + heading.y * heading.y + heading.z * heading.z;
            return Mathf.Sqrt(distanceSquared);
        }

        internal static SkinnedMeshRenderer GetMeshRenderer(ChaControl chaControl, string renderKey) 
        {
            var renderers = chaControl.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            var renderer = renderers.FirstOrDefault(x => (x.name + x.sharedMesh.vertexCount.ToString()) == renderKey);
            return renderer;
        }

        /// <summary>
        /// Will get any Mesh Renderers for the given ChaControl.objxxx passed in
        /// </summary>
        /// <param name="chaControlObjs">The ChaControl.objxxx to fetch mesh renderers from  Might work for other GameObjects as well</param>
        internal static List<SkinnedMeshRenderer> GetMeshRenderers(GameObject[] chaControlObjs) {            
            var renderers = new List<SkinnedMeshRenderer>();
            if (chaControlObjs == null) return renderers;

            foreach(var chaControlObj in chaControlObjs) 
            {
                if (chaControlObj == null) continue;

                var skinnedItems = GetMeshRenderers(chaControlObj);
                if (skinnedItems != null && skinnedItems.Count > 0) {
                    renderers.AddRange(skinnedItems);
                }
            }

            // PregnancyPlusPlugin.Logger.LogInfo($"GetMeshRenderers > {renderers.Count}");
            return renderers;
        }
        
        internal static List<SkinnedMeshRenderer> GetMeshRenderers(GameObject characterObj) {            
            var renderers = new List<SkinnedMeshRenderer>();
            if (characterObj == null) return renderers;

            var skinnedItem = characterObj.GetComponentsInChildren<SkinnedMeshRenderer>(true);            
            if (skinnedItem.Length > 0) {
                renderers.AddRange(skinnedItem);
            }

            return renderers;
        }

        internal static Renderer GetRenderer(ChaControl chaControl, string renderKey) 
        {
            var renderers = chaControl.GetComponentsInChildren<Renderer>(true);
            var renderer = renderers.FirstOrDefault(x => x.name == renderKey);
            return renderer;
        }

        internal static List<Renderer> GetRenderers(GameObject[] chaControlObjs) {            
            var renderers = new List<Renderer>();
            if (chaControlObjs == null) return renderers;

            foreach(var chaControlObj in chaControlObjs) 
            {
                if (chaControlObj == null) continue;

                var skinnedItems = GetRenderers(chaControlObj);
                if (skinnedItems != null && skinnedItems.Count > 0) {
                    renderers.AddRange(skinnedItems);
                }
            }

            // PregnancyPlusPlugin.Logger.LogInfo($"GetMeshRenderers > {renderers.Count}");
            return renderers;
        }

        internal static List<Renderer> GetRenderers(GameObject characterObj) {            
            var renderers = new List<Renderer>();
            if (characterObj == null) return renderers;

            var skinnedItem = characterObj.GetComponentsInChildren<Renderer>(true);            
            if (skinnedItem.Length > 0) {
                renderers.AddRange(skinnedItem);
            }

            return renderers;
        }

        internal static int GetWeeksFromPregnancyPluginData(ChaControl chaControl, string targetBehaviorId)
        {
            var kkPregCtrlInst = PregnancyPlusHelper.GetCharacterBehaviorController(chaControl, targetBehaviorId);
            if (kkPregCtrlInst == null) return -1;

            //Get the pregnancy data object
            var data = kkPregCtrlInst.GetType().GetProperty("Data").GetValue(kkPregCtrlInst, null);
            if (data == null) return -1;

            var week = Traverse.Create(data).Field("Week").GetValue<int>();
            if (week.Equals(null) || week < -1) return -1;

            return week;
        }

        internal static bool IsUncensorBody(ChaControl chaControl, string UncensorCOMName, string defaultBodyFemaleGUID) 
        {
            //Uncensor body needs vert modifications.  Check to see if this is the default body mesh or not
            var uncensorController = PregnancyPlusHelper.GetCharacterBehaviorController(chaControl, UncensorCOMName);
            if (uncensorController == null) return false;

            //Get the body type name, and see if it is the default mesh name
            var bodyData = uncensorController.GetType().GetProperty("BodyData").GetValue(uncensorController, null);
            if (bodyData == null) return false;

            var bodyGUID = Traverse.Create(bodyData).Field("BodyGUID").GetValue<string>();
            if (bodyGUID == null) return false;

            return bodyGUID != defaultBodyFemaleGUID;
        }

        internal static CharaCustomFunctionController GetCharacterBehaviorController(ChaControl chaControl, string targetBehaviorId) 
        {
            if (chaControl == null) return null;

            //Get all registered behaviors for this character
            var behaviors = CharacterApi.GetBehaviours(chaControl);
            if (behaviors == null) return null;

            foreach(var behavior in behaviors) {
                // PregnancyPlusPlugin.Logger.LogInfo($" {behavior.name} > {behavior.ExtendedDataId}"); 

                //Find the behavior with matching id (COM name)
                if (behavior.ExtendedDataId == targetBehaviorId) {
                    return behavior;
                }                
            }

            return null;
        }
    
    }
}
