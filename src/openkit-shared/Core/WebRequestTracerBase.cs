﻿/***************************************************
 * (c) 2016-2017 Dynatrace LLC
 *
 * @author: Christian Schwarzbauer
 */
using Dynatrace.OpenKit.API;
using Dynatrace.OpenKit.Protocol;
using Dynatrace.OpenKit.Providers;

namespace Dynatrace.OpenKit.Core {

    /// <summary>
    ///  Abstract base class implementation of the IWebRequestTracer interface.
    /// </summary>
    public abstract class WebRequestTracerBase : IWebRequestTracer {

        // Dynatrace tag that has to be used for tracing the web request
        private string tag = null;

        // HTTP information: URL & response code
        protected string url = "<unknown>";
        private int responseCode = -1;

        // start/end time & sequence number
        private long startTime = -1;
        private long endTime = -1;
        private int startSequenceNo = -1;
        private int endSequenceNo = -1;

        // Beacon and Action references
        private Beacon beacon;
        private Action action;

        // *** constructors ***

        public WebRequestTracerBase(Beacon beacon, Action action) {
            this.beacon = beacon;
            this.action = action;

            // creating start sequence number has to be done here, because it's needed for the creation of the tag
            startSequenceNo = beacon.NextSequenceNumber;

            tag = beacon.CreateTag(action, startSequenceNo);
        }

        // *** IWebRequestTracer interface methods ***

        public string Tag {
            get {
                return tag;
            }
        }

        public int ResponseCode {
            get {
                return responseCode;
            }
            set {
                responseCode = value;
            }
        }

        public void Start() {
            startTime = beacon.CurrentTimestamp;
        }

        public void Stop() {
            endTime = beacon.CurrentTimestamp;
            endSequenceNo = beacon.NextSequenceNumber;

            // add web request to beacon
            beacon.AddWebRequest(action, this);
        }

        // *** properties ***

        public string URL {
            get {
                return url;
            }
        }

        public long StartTime {
            get {
                return startTime;
            }
        }

        public long EndTime {
            get {
                return endTime;
            }
        }

        public int StartSequenceNo {
            get {
                return startSequenceNo;
            }
        }

        public int EndSequenceNo {
            get {
                return endSequenceNo;
            }
        }

    }

}
