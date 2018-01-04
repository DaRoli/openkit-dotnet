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

using Dynatrace.OpenKit.API;
using Dynatrace.OpenKit.Core;
using Dynatrace.OpenKit.Core.Configuration;
using Dynatrace.OpenKit.Protocol.SSL;
using Dynatrace.OpenKit.Providers;

namespace Dynatrace.OpenKit
{

    /// <summary>
    ///  This factory creates instances of the OpenKit to work with.
    ///  It can be used to create instances for both Dynatrace AppMon and Dynatrace SaaS/Managed.
    /// </summary>
    public class OpenKitFactory
    {

        /// <summary>
        ///  Name of Dynatrace HTTP header which is used for tracing web requests.
        /// </summary>
        public const string WEBREQUEST_TAG_HEADER = "X-dynaTrace";

        /// <summary>
        ///  Creates a Dynatrace SaaS instance of the OpenKit.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="applicationID">the application ID (must be a valid application UUID)</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the beacon forwarder to send the data to</param>
        /// <returns>Dynatrace SaaS instance of the OpenKit</returns>
        public static IOpenKit CreateDynatraceInstance(string applicationName, string applicationID, long deviceID, string endpointURL)
        {
            return CreateDynatraceInstance(applicationName, applicationID, deviceID, endpointURL, false);
        }

        /// <summary>
        ///  Creates a Dynatrace SaaS instance of the OpenKit, optionally with verbose logging.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="applicationID">the application ID (must be a valid application UUID)</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the beacon forwarder to send the data to</param>
        /// <param name="verbose">if true, turn on verbose logging on stdout</param>
        /// <returns>Dynatrace SaaS instance of the OpenKit</returns>
        public static IOpenKit CreateDynatraceInstance(string applicationName, string applicationID, long deviceID, string endpointURL, bool verbose)
        {
            return CreateDynatraceInstance(applicationName, applicationID, deviceID, endpointURL, verbose, new SSLStrictTrustManager());
        }

        /// <summary>
        ///  Creates a Dynatrace SaaS instance of the OpenKit, optionally with verbose logging.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="applicationID">the application ID (must be a valid application UUID)</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the beacon forwarder to send the data to</param>
        /// <param name="verbose">if true, turn on verbose logging on stdout</param>
        /// <param name="sslTrustManager">For overriding SSL certificate checks</param>
        /// <returns>Dynatrace SaaS instance of the OpenKit</returns>
        public static IOpenKit CreateDynatraceInstance(string applicationName, string applicationID, long deviceID, string endpointURL, bool verbose, ISSLTrustManager sslTrustManager)
        {
            var openKit = new Core.OpenKit(new DefaultLogger(verbose), new DynatraceConfiguration(applicationName, applicationID, deviceID, endpointURL, sslTrustManager, new DefaultSessionIDProvider()));
            openKit.Initialize();

            return openKit;
        }

        /// <summary>
        ///  Creates a Dynatrace Managed instance of the OpenKit.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="applicationID">the application ID (must be a valid application UUID)</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the beacon forwarder to send the data to</param>
        /// <param name="tenantID">the id of the tenant</param>
        /// <returns>Dynatrace Managed instance of the OpenKit</returns>
        public static IOpenKit CreateDynatraceManagedInstance(string applicationName, string applicationID, long deviceID, string endpointURL, string tenantID)
        {
            return CreateDynatraceManagedInstance(applicationName, applicationID, deviceID, endpointURL, tenantID, false);
        }

        /// <summary>
        ///  Creates a Dynatrace Managed instance of the OpenKit, optionally with verbose logging.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="applicationID">the application ID (must be a valid application UUID)</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the beacon forwarder to send the data to</param>
        /// <param name="tenantID">the id of the tenant</param>
        /// <param name="verbose">if true, turn on verbose logging on stdout</param>
        /// <returns>Dynatrace Managed instance of the OpenKit</returns>
        public static IOpenKit CreateDynatraceManagedInstance(string applicationName, string applicationID, long deviceID, string endpointURL, string tenantID, bool verbose)
        {
            return CreateDynatraceManagedInstance(applicationName, applicationID, deviceID, endpointURL, tenantID, verbose, new SSLStrictTrustManager());
        }

        /// <summary>
        ///  Creates a Dynatrace Managed instance of the OpenKit, optionally with verbose logging.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="applicationID">the application ID (must be a valid application UUID)</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the beacon forwarder to send the data to</param>
        /// <param name="tenantID">the id of the tenant</param>
        /// <param name="verbose">if true, turn on verbose logging on stdout</param>
        /// <param name="sslTrustManager">For overriding SSL certificate checks</param>
        /// <returns>Dynatrace Managed instance of the OpenKit</returns>
        public static IOpenKit CreateDynatraceManagedInstance(string applicationName, string applicationID, long deviceID, string endpointURL, string tenantID, bool verbose, ISSLTrustManager sslTrustManager)
        {
            var openKit = new Core.OpenKit(new DefaultLogger(verbose), new DynatraceManagedConfiguration(tenantID, applicationName, applicationID, deviceID, endpointURL, sslTrustManager, new DefaultSessionIDProvider()));
            openKit.Initialize();

            return openKit;
        }

        /// <summary>
        ///  Creates a Dynatrace AppMon instance of the OpenKit.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the Java/Webserver Agent to send the data to</param>
        /// <returns>Dynatrace AppMon instance of the OpenKit</returns>
        public static IOpenKit CreateAppMonInstance(string applicationName, long deviceID, string endpointURL)
        {
            return CreateAppMonInstance(applicationName, deviceID, endpointURL, false);
        }

        /// <summary>
        ///  Creates a Dynatrace AppMon instance of the OpenKit, optionally with verbose logging.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the Java/Webserver Agent to send the data to</param>
        /// <param name="verbose">if true, turn on verbose logging on stdout</param>
        /// <returns>Dynatrace AppMon instance of the OpenKit</returns>
        public static IOpenKit CreateAppMonInstance(string applicationName, long deviceID, string endpointURL, bool verbose)
        {
            var openKit = new Core.OpenKit(new DefaultLogger(verbose), new AppMonConfiguration(applicationName, deviceID, endpointURL, new SSLStrictTrustManager(), new DefaultSessionIDProvider()));
            openKit.Initialize();

            return openKit;
        }

        /// <summary>
        ///  Creates a Dynatrace AppMon instance of the OpenKit, optionally with verbose logging.
        /// </summary>
        /// <param name="applicationName">the application name</param>
        /// <param name="deviceID">unique device identifier</param>
        /// <param name="endpointURL">the URL of the Java/Webserver Agent to send the data to</param>
        /// <param name="verbose">if true, turn on verbose logging on stdout</param>
        /// <param name="sslTrustManager">For overriding SSL certificate checks</param>
        /// <returns>Dynatrace AppMon instance of the OpenKit</returns>
        public static IOpenKit CreateAppMonInstance(string applicationName, long deviceID, string endpointURL, bool verbose, ISSLTrustManager sslTrustManager)
        {
            var openKit = new Core.OpenKit(new DefaultLogger(verbose), new AppMonConfiguration(applicationName, deviceID, endpointURL, sslTrustManager, new DefaultSessionIDProvider()));
            openKit.Initialize();

            return openKit;
        }
    }
}
