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


namespace Transmitly.ChannelProvider.Firebase.Configuration
{
	public sealed class FirebaseCredential
	{
		public string? Json { get; private set; }
		public string? AccessToken { get; private set; }
		public string? FilePath { get; private set; }
		public Stream? Stream { get; private set; }

		public bool IsStream => Stream != null;
		public bool IsJson => !string.IsNullOrWhiteSpace(Json);
		public bool IsAccessToken => !string.IsNullOrWhiteSpace(AccessToken);
		public bool IsFilePath => !string.IsNullOrWhiteSpace(FilePath);
		public bool IsDefault { get; private set; }

		public static FirebaseCredential FromJson(string json)
		{
			return new FirebaseCredential { Json = json };
		}

		public static FirebaseCredential FromAccessToken(string accessToken)
		{
			return new FirebaseCredential { AccessToken = accessToken };
		}

		public static FirebaseCredential FromFile(string filePath)
		{
			return new FirebaseCredential { FilePath = filePath };
		}
		public static FirebaseCredential GetApplicationDefault()
		{
			return new FirebaseCredential { IsDefault = true };
		}

		public static FirebaseCredential FromStream(Stream stream)
		{
			return new FirebaseCredential { Stream = stream };
		}
	}
}