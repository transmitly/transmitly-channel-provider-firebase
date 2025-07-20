// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
//  
//  Licensed under the Apache License, Version 2.0 (the "License")
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using Transmitly.ChannelProvider.Firebase.Configuration;
using Transmitly.ChannelProvider.Firebase.FirebaseAdmin;

namespace Transmitly
{
	public static class FirebaseChannelProviderExtensions
	{		
		public static CommunicationsClientBuilder AddFirebaseSupport(this CommunicationsClientBuilder communicationsClientBuilder, Action<FirebaseOptions> options, string? providerId = null)
		{
			var optionObj = new FirebaseOptions();
			options(optionObj);

			communicationsClientBuilder
				.ChannelProvider
				.Build(Id.ChannelProvider.Firebase(providerId), optionObj)
				.AddDispatcher<FirebaseAdminChannelProviderDispatcher, IPushNotification>(Id.Channel.PushNotification())
				.Register();

			return communicationsClientBuilder;
		}
	}
}