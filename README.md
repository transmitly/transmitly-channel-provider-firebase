# Transmitly.ChannelProvider.Firebase

A [Transmitly](https://github.com/transmitly/transmitly) channel provider that enables sending push notifications to iOS, Android and other apps/devices using [firebase](https://firebase.google.com/).

### Getting started

To use the Firebase channel provider, first install the [NuGet package](https://nuget.org/packages/transmitly.channelprovider.firebase):

```shell
dotnet add package Transmitly.ChannelProvider.Firebase
```

Then add the channel provider using `AddFirebaseSupport()`:

```csharp
using Transmitly;
...
var communicationClient =
	new CommunicationsClientBuilder()
	.AddFirebaseSupport(options =>
	{
		options.Credential = FirebaseCredential.GetApplicationDefault();
	})
	.AddPipeline("WelcomeKit", options =>
	{
		options.AddPushNotification(push =>
		{
			push.Title.AddStringTemplate("Welcome!");
			push.ImageUrl.AddStringTemplate("https://transmit.ly/assets/welcome.png");
			push.Body.AddStringTemplate("Thanks for signing up! Here's 500 bonus points on us.");
		});
	});
```
* See the [Transmitly](https://github.com/transmitly/transmitly) project for more details on what a channel provider is and how it can be configured.

### Copyright and Trademark 

Copyright © 2024–2025 Code Impressions, LLC.

Transmitly™ is a trademark of Code Impressions, LLC.

This open-source project is sponsored and maintained by Code Impressions
and is licensed under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html).

The Apache License applies to the software code only and does not grant
permission to use the Transmitly name or logo, except as required to
describe the origin of the software.
