# Transmitly.ChannelProvider.Firebase

A [Transmitly](https://github.com/transmitly/transmitly) channel provider for sending push notifications with [Firebase Cloud Messaging (FCM)](https://firebase.google.com/docs/cloud-messaging).

## Installation

Install the package from NuGet:

```shell
dotnet add package Transmitly.ChannelProvider.Firebase
```

## Quick Start

```csharp
using Transmitly;
using Transmitly.Channel.Push;
using Transmitly.ChannelProvider.Firebase.Configuration;

var client = new CommunicationsClientBuilder()
	.AddFirebaseSupport(options =>
	{
		options.Credential = FirebaseCredential.FromFile("firebase-service-account.json");
		options.ProjectId = "your-firebase-project-id"; // optional when available via credential/environment
		options.AppName = "default"; // optional (default: "default")
	})
	.AddPipeline("welcome-push", pipeline =>
	{
		pipeline.AddPushNotification(push =>
		{
			push.Title.AddStringTemplate("Welcome!");
			push.Body.AddStringTemplate("Thanks for signing up.");
			push.ImageUrl.AddStringTemplate("https://transmit.ly/assets/welcome.png");
		});
	})
	.BuildClient();

var recipient = new PlatformIdentityAddress(
	"FCM_DEVICE_TOKEN",
	type: PlatformIdentityAddress.Types.DeviceToken());

var result = await client.DispatchAsync(
	"welcome-push",
	[recipient],
	TransactionModel.Create(new
	{
		userId = "12345",
		campaign = "welcome"
	}));
```

## Recipients: Device Tokens and Topics

The push channel supports:
- Device token recipients (`device-token`)
- Topic recipients (`push-topic`)

Topic example:

```csharp
var topicRecipient = new PlatformIdentityAddress(
	"order-updates",
	type: PlatformIdentityAddress.Types.Topic());

await client.DispatchAsync(
	"welcome-push",
	[topicRecipient],
	TransactionModel.Create(new { orderId = "A-1001" }));
```

## Firebase Credentials

Supported credential creation methods:

```csharp
FirebaseCredential.GetApplicationDefault()
FirebaseCredential.FromFile("firebase-service-account.json")
FirebaseCredential.FromJson(json)
FirebaseCredential.FromStream(stream)
```

You can also set:
- `ProjectId`
- `ServiceAccountId`
- `AppName` (used to isolate/reuse Firebase SDK app instances)

## Multiple Firebase Provider Instances

`AddFirebaseSupport` accepts an optional `providerId`, which is useful for multi-tenant or multi-project setups.

```csharp
using Transmitly.ChannelProvider.Firebase.Configuration;

var client = new CommunicationsClientBuilder()
	.AddFirebaseSupport(options =>
	{
		options.Credential = FirebaseCredential.FromFile("tenant-a.json");
		options.AppName = "firebase-tenant-a";
	}, providerId: "tenant-a")
	.AddFirebaseSupport(options =>
	{
		options.Credential = FirebaseCredential.FromFile("tenant-b.json");
		options.AppName = "firebase-tenant-b";
	}, providerId: "tenant-b")
	.AddPipeline("otp", pipeline =>
	{
		pipeline.AddPushNotification(push =>
		{
			push.Title.AddStringTemplate("Your OTP");
			push.Body.AddStringTemplate("Code: {{code}}");
			push.AddChannelProviderFilter(Id.ChannelProvider.Firebase("tenant-a"));
		});
	})
	.BuildClient();
```

## Notes

- Push title/body/image come from your `AddPushNotification` templates.
- The transactional content model is mapped into FCM `Data` values as strings where possible.
- For general pipeline/channel concepts, see the main [Transmitly](https://github.com/transmitly/transmitly) project.

---
_Copyright (c) Code Impressions, LLC. This open-source project is sponsored and maintained by Code Impressions and is licensed under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
