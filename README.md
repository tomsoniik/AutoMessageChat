# AutoMessageChat

A **Counter-Strike 2** server plugin that posts chat messages automatically: rotating announcements, a welcome message for each player, and join and leave notices. Built on [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp).

[![Release](https://img.shields.io/github/v/release/tomsoniik/AutoMessageChat?style=flat-square&color=3399FF)](https://github.com/tomsoniik/AutoMessageChat/releases/latest)
![CS2](https://img.shields.io/badge/game-CS2-3399FF?style=flat-square)
![CounterStrikeSharp](https://img.shields.io/badge/CounterStrikeSharp-1.0.228%2B-3399FF?style=flat-square)

<p align="center">
  <a href="https://github.com/tomsoniik/AutoMessageChat/releases/latest/download/AutoMessageChat.dll">
    <img src="https://img.shields.io/badge/Download-AutoMessageChat.dll-3399FF?style=for-the-badge&logo=github&logoColor=white&labelColor=0D1117" alt="Download AutoMessageChat.dll" height="40" />
  </a>
</p>

## Features

- Rotating chat messages, sent one after another at a set interval
- Private welcome message for each player who joins
- Public join and leave notices with the player's name
- Custom message prefix, colors included
- Full support for CounterStrikeSharp chat colors, e.g. `{Green}`, `{Red}`, `{LightBlue}`
- Config file is created automatically on first start

## Requirements

- Counter-Strike 2 dedicated server
- [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master) 2.x
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) 1.0.228 or newer

## Installation

1. Download [`AutoMessageChat.dll`](https://github.com/tomsoniik/AutoMessageChat/releases/latest/download/AutoMessageChat.dll) (latest version; all versions are in [Releases](https://github.com/tomsoniik/AutoMessageChat/releases)).
2. Upload it to this folder on your server:
   ```
   addons/counterstrikesharp/plugins/AutoMessageChat/
   ```
3. Restart the server or load the plugin with:
   ```
   css_plugins load "AutoMessageChat"
   ```

## Configuration

On first start the plugin creates:

```
addons/counterstrikesharp/configs/plugins/AutoMessageChat/AutoMessageChat.json
```

Example:

```json
{
  "Prefix": "{Green}● {DarkRed}[{White}FG :: INFO{DarkRed}]{Default}",
  "WelcomeMessage": "{Green}Follow us on social media, type {LightRed}!socials{Default}",
  "PlayerJoinMessage": "{LightBlue}{PLAYER} {White}just joined the server!",
  "PlayerDisconnectMessage": "{LightBlue}{PLAYER} {White}left the server.",
  "MessageIntervalSeconds": 120.0,
  "Messages": [
    "Check out our shop with {Green}!shop{Default}.",
    "{Red}Admin recruitment{Default} is open! Apply on our forum.",
    "Please keep it {LightBlue}friendly{Default} on the server."
  ],
  "ConfigVersion": 1
}
```

| Option | Description |
| :--- | :--- |
| `Prefix` | Text shown before every message. Can include colors. |
| `WelcomeMessage` | Private message sent to a player after they join. |
| `PlayerJoinMessage` | Public message when a player joins. `{PLAYER}` is replaced with their name. |
| `PlayerDisconnectMessage` | Public message when a player leaves. Supports `{PLAYER}`. |
| `MessageIntervalSeconds` | Seconds between messages from the `Messages` list. |
| `Messages` | Messages shown one after another, on a loop. |

After changing the config, reload the plugin or restart the server.

## Author

**tomSoNN** ([tomsoniik](https://github.com/tomsoniik)), co-creator of [FragHub](https://hub.fragujemy.com).
Questions and bug reports: [Issues](https://github.com/tomsoniik/AutoMessageChat/issues) or Discord `tomsoncs`.
