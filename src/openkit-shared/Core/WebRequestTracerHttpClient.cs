﻿//
// Copyright 2018 Dynatrace LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

using Dynatrace.OpenKit.Protocol;

namespace Dynatrace.OpenKit.Core
{
#if (!NET40 && !NET35)
    /// <summary>
    ///  Inherited class of WebRequestTracerBase which can be used for tracing and timing of a web request provided via an HttpClient.
    /// </summary>
    public class WebRequestTracerHttpClient : WebRequestTracerBase
    {

        // *** constructors ***

        // creates web request tracer with a System.Net.Http.HttpClient
        public WebRequestTracerHttpClient(Beacon beacon, Action action, System.Net.Http.HttpClient httpClient) : base(beacon, action)
        {
            if (httpClient != null)
            {
                SetTagOnConnection(httpClient);

                this.url = httpClient.BaseAddress.AbsoluteUri;
            }
        }

        // *** private methods ***

        // set the Dynatrace tracer on the provided URLConnection
        private void SetTagOnConnection(System.Net.Http.HttpClient httpClient)
        {
            // check if header is already set
            if (!httpClient.DefaultRequestHeaders.Contains(OpenKitFactory.WEBREQUEST_TAG_HEADER))
            {
                // if not yet set -> set it now
                httpClient.DefaultRequestHeaders.Add(OpenKitFactory.WEBREQUEST_TAG_HEADER, Tag);
            }
        }

    }

#endif

}
