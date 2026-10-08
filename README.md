# AutoMessageChat

Plugin do serwerów **Counter-Strike 2**, który automatycznie wysyła wiadomości na czat: cykliczne ogłoszenia, powitanie dla gracza oraz informację o wejściu i wyjściu z serwera. Działa na [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp).

[![Release](https://img.shields.io/github/v/release/tomsoniik/AutoMessageChat?style=flat-square&color=3399FF)](https://github.com/tomsoniik/AutoMessageChat/releases/latest)
![CS2](https://img.shields.io/badge/game-CS2-3399FF?style=flat-square)
![CounterStrikeSharp](https://img.shields.io/badge/CounterStrikeSharp-1.0.228%2B-3399FF?style=flat-square)

## Funkcje

- Cykliczne wiadomości na czacie, wysyłane po kolei w ustalonym odstępie czasu
- Prywatna wiadomość powitalna dla gracza po wejściu na serwer
- Publiczna informacja o wejściu i wyjściu gracza (z jego nickiem)
- Własny prefiks wiadomości, także w kolorach
- Pełne wsparcie kolorów czatu CounterStrikeSharp, np. `{Green}`, `{Red}`, `{LightBlue}`
- Plik konfiguracyjny tworzy się sam przy pierwszym uruchomieniu

## Wymagania

- Serwer dedykowany Counter-Strike 2
- [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master) 2.x
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) 1.0.228 lub nowszy

## Instalacja

1. Pobierz `AutoMessageChat.dll` z [najnowszego wydania](https://github.com/tomsoniik/AutoMessageChat/releases/latest).
2. Wgraj plik na serwer do folderu:
   ```
   addons/counterstrikesharp/plugins/AutoMessageChat/
   ```
3. Zrestartuj serwer albo załaduj plugin komendą:
   ```
   css_plugins load "AutoMessageChat"
   ```

## Konfiguracja

Przy pierwszym uruchomieniu plugin tworzy plik:

```
addons/counterstrikesharp/configs/plugins/AutoMessageChat/AutoMessageChat.json
```

Przykład:

```json
{
  "Prefix": "{Green}● {DarkRed}[{White}FG :: INFO{DarkRed}]{Default}",
  "WelcomeMessage": "{Green}Dołącz na nasze sociale wpisując {LightRed}!sociale{Default}",
  "PlayerJoinMessage": "{LightBlue}{PLAYER} {White}właśnie dołączył na serwer!",
  "PlayerDisconnectMessage": "{LightBlue}{PLAYER} {White}opuścił serwer.",
  "MessageIntervalSeconds": 120.0,
  "Messages": [
    "Zapraszamy do naszego sklepu pod komendą {Green}!sklep{Default}.",
    "Trwa {Red}rekrutacja na admina{Default}! Złóż podanie na naszym forum.",
    "Pamiętaj o zachowaniu {LightBlue}kultury{Default} na serwerze."
  ],
  "ConfigVersion": 1
}
```

| Pole | Opis |
| :--- | :--- |
| `Prefix` | Tekst wyświetlany przed każdą wiadomością. Może zawierać kolory. |
| `WelcomeMessage` | Prywatna wiadomość dla gracza po wejściu na serwer. |
| `PlayerJoinMessage` | Publiczna wiadomość o wejściu gracza. `{PLAYER}` zamienia się na jego nick. |
| `PlayerDisconnectMessage` | Publiczna wiadomość o wyjściu gracza. Obsługuje `{PLAYER}`. |
| `MessageIntervalSeconds` | Odstęp w sekundach między kolejnymi wiadomościami z listy `Messages`. |
| `Messages` | Lista wiadomości wyświetlanych po kolei, w kółko. |

Po zmianie konfiguracji przeładuj plugin albo zrestartuj serwer.

## Autor

**tomSoNN** ([tomsoniik](https://github.com/tomsoniik)), twórca [FragHub](https://hub.fragujemy.com).
Pytania i błędy zgłaszaj w [Issues](https://github.com/tomsoniik/AutoMessageChat/issues) albo na Discordzie: `tomsoncs`.
