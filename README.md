# Transmitly.ChannelProvider.Firebase

A [Transmitly](https://github.com/transmitly/transmitly) channel provider that enables sending push notifications to iOS, Android and other devices using firebase.

### Getting started

To use the Firebase channel provider, first install the [NuGet package](https://nuget.org/packages/transmitly.channelprovider.firebase):

```shell
dotnet add package Transmitly.ChannelProvider.Firebase
```

Then add the channel provider using `AddFirebaseSupport()`:

```csharp
using Transmitly;
using Google.Apis.Auth.OAuth2;
...
var communicationClient = new CommunicationsClientBuilder()
	.AddFirebaseSupport(options =>
	{
		options.Credential = GoogleCredential.GetApplicationDefault();
	})
```
* See the [Transmitly](https://github.com/transmitly/transmitly) project for more details on what a channel provider is and how it can be configured.


<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://github.com/transmitly/transmitly/assets/3877248/524f26c8-f670-4dfa-be78-badda0f48bfb">
  <img alt="an open-source project sponsored by CiLabs of Code Impressions, LLC" src="https://github.com/transmitly/transmitly/assets/3877248/34239edd-234d-4bee-9352-49d781716364" width="350" align="right">
</picture> 

---------------------------------------------------

_Copyright &copy; Code Imperssions, LLC - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
