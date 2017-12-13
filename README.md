# UniTube-UWP
UniTube is an open source YouTube client built to bring an amazing experience to the Universal Windows Platform (UWP)

## Why do we develop this app?
We develop it because we like to program and we like the UWP (Universal Windows Platform).

We don't know if many people would use the app, but if even we are the only users, we would continue developing.

## Social
We have a group in spanish on Telegram. If you want to join click [here](https://t.me/UnitubeTesters)

## Build instructions
**IMPORTANT:** You MUST have Visual Studio 2017 in order to compile this project.

1. Go to the [Google Developers Console](https://console.developers.google.com/).
2. Select or create a project.
3. On the left sidebar, select **API and authentication**. In the API list, make sure the status in **ON** for **version 3 of the YouTube data API**.
4. On the left sidebar, select **Credentials**.
5. The API supports two types of credentials. Create both:
    - OAuth 2.0
    - API keys
6. Clone the repo `git clone https://github.com/NucleuxSoft/UniTube-UWP.git`.
7. Create a new file inside `UniTube-UWP/UniTube.Core` and name it `Client.Secrets.cs`:
```csharp
namespace UniTube.Core
{
    public static partial class Client
    {
        static Client()
        {
            ApiKey = "your_api_key";
            ClientId = "your_client_id";
            ClientSecret = "your_client_secret";
        }
    }
}
```
8. Replace `your_api_key`, `your_client_id` and `your_client_secret` with the data obtained from step 5.

## Roadmap

### Current Features
- Trending videos
- Search videos
- Fluent Design System
- Live Tile

### Planned Features
- Play videos
- Manage multiple accounts
- Cast videos to any screen
- Save videos
- Upload videos
- and more...
