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

using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Transmitly.Channel.Push;

namespace Transmitly.ChannelProvider.Firebase
{
    public sealed class FirebaseChannelProviderClient : ChannelProviderClient<IPushNotification>
    {
        public FirebaseChannelProviderClient(FirebaseOptions options)
        {
            Guard.AgainstNull(options);
            FirebaseApp.Create(FirebaseOptionsConverter.FromFirebaseOptions(options));
        }

        public override async Task<IReadOnlyCollection<IDispatchResult?>> DispatchAsync(IPushNotification communication, IDispatchCommunicationContext communicationContext, CancellationToken cancellationToken)
        {
            List<Message> messages = new(communication.To.Count);
            foreach (var recipient in communication.To)
            {
                messages.Add(new Message
                {
                    Data = TryConvertToDictionary(communicationContext.ContentModel?.Model),
                    Notification = new Notification
                    {
                        Title = communication.Title,
                        Body = communication.Body,
                        ImageUrl = communication.ImageUrl
                    },
                    Token = recipient.IfType(IdentityAddress.Types.DeviceToken(), recipient.Value),
                    Topic = recipient.IfType(IdentityAddress.Types.Topic(), recipient.Value)
                });
            }

            var response = await FirebaseMessaging.DefaultInstance.SendAllAsync(messages, cancellationToken);

            return response.Responses.Select(m => new FirebaseDispatchResult(m)).ToList();
        }

        private static Dictionary<string, string?>? TryConvertToDictionary(object? content)
        {
            try
            {
                if (content == null)
                    return null;
                var props = content.GetType().GetProperties();
                var pairDictionary = props.ToDictionary(x => x.Name, x => x.GetValue(content, null)?.ToString());
                return pairDictionary;
            }
            catch
            {
                return null;
            }
        }
    }
}