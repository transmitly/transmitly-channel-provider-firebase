﻿// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
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

using FirebaseAdmin.Messaging;
using System;

namespace Transmitly.ChannelProvider.Firebase
{
	internal sealed class FirebaseDispatchResult : IDispatchResult
	{
		public FirebaseDispatchResult()
		{

		}
		public FirebaseDispatchResult(SendResponse response)
		{
			DispatchStatus = response.IsSuccess ? DispatchStatus.Delivered : DispatchStatus.Exception;
			ResourceId = response.MessageId;
		}
		public string? ResourceId { get; set; }

		public DispatchStatus DispatchStatus { get; set; }

		public string? ChannelProviderId { get; }

		public string? ChannelId { get; }

		public Exception? Exception { get; set; }
	}
}