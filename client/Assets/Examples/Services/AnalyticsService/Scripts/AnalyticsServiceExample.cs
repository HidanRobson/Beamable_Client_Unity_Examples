﻿using System.Collections.Generic;
using UnityEngine;
using Beamable.Api.Analytics;

namespace Beamable.Examples.Services.AnalyticsService
{
    /// <summary>
    /// Inspired by <see cref="SampleCustomEvent"/>.
    /// </summary>
    public class MyExampleEvent : CoreEvent 
    {
        public MyExampleEvent(string foo, string bar) : base (
            "example",  
            "my_example_event", 
            new Dictionary<string, object>
            {
                ["foo"] = foo,
                ["bar"] = bar,
                ["hello_world"] = "Hello World."
            })
        {
        }
    }
    
    /// <summary>
    /// Demonstrates <see cref="AnalyticsService"/>.
    /// </summary>
    public class AnalyticsServiceExample : MonoBehaviour
    {
        //  Unity Methods  --------------------------------

        protected void Start()
        {
            Debug.Log("Start()");

            SetupBeamable();
        }
        
        //  Methods  --------------------------------------
        private async void SetupBeamable()
        {
            var beamableAPI = await Beamable.API.Instance;
            Debug.Log($"beamableAPI.User.id = {beamableAPI.User.id}");

            var foo = "lorem ipsum 1";
            var bar = "lorem ipsum 2";
            var myExampleEvent = new MyExampleEvent(foo, bar);
            
            var sendImmediately = true;
            beamableAPI.AnalyticsTracker.TrackEvent(myExampleEvent, sendImmediately);
            
            Debug.Log($"TrackEvent() eventName = {myExampleEvent.eventName}");
        }
    }
}