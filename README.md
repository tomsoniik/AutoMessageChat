# CS2 AutoMessageChat

Uniwersalny plugin AutoMessageChat do Counter-Strike 2, zbudowany w oparciu o [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp).

## Funkcje

- **Konfigurowalny Prefix:** Możliwość zmiany prefiksu wiadomości (w tym kolorów) bezpośrednio z poziomu pliku konfiguracyjnego.
- **Konfigurowalne Wiadomości:** Łatwe dodawanie, usuwanie i edytowanie wyświetlanych wiadomości.
- **Kolorowy Czat:** Pełne wsparcie dla kolorów czatu CounterStrikeSharp (np. `{Green}`, `{Red}`, `{LightBlue}`).
- **Automatycznie generowany Config:** Plik konfiguracyjny tworzy się automatycznie przy pierwszym uruchomieniu na serwerze.

## Wymagania

- Serwer dedykowany Counter-Strike 2.
- [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master) (v2.x)
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) (v1.0.228+)

## Instalacja

1. Zainstaluj CounterStrikeSharp na swoim serwerze CS2.
2. Skompiluj projekt za pomocą polecenia `dotnet build` (lub pobierz gotowy plik `.dll`).
3. Przenieś plik `AutoMessageChat.dll` do folderu na serwerze: `addons/counterstrikesharp/plugins/AutoMessageChat/`.
4. Zrestartuj serwer lub załaduj plugin ręcznie komendą `css_plugins load "AutoMessageChat"`.

## Konfiguracja

Po pierwszym uruchomieniu w folderze `addons/counterstrikesharp/configs/plugins/AutoMessageChat/` zostanie wygenerowany plik `AutoMessageChat.json`.

Przykładowa konfiguracja:

```json
{
  "Prefix": "{Green}● {DarkRed}[{White}FG :: INFO{DarkRed}]{Default}",
  "WelcomeMessage": "{Green}Dołącz na Nasze Sociale wpisując {LightRed}!sociale{Default}",
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

- **Prefix:** Tekst wyświetlany przed każdą wiadomością na czacie. Możesz używać standardowych tagów z kolorami (jak powyżej).
- **WelcomeMessage:** Wiadomość powitalna wysyłana automatycznie do gracza (prywatna) po wejściu na serwer.
- **PlayerJoinMessage:** Wiadomość wysyłana publicznie do wszystkich, gdy gracz wchodzi na serwer. Obsługuje zmienną `{PLAYER}`, która podmienia się na nick.
- **PlayerDisconnectMessage:** Wiadomość wysyłana do wszystkich o wyjściu gracza z serwera (z obsługą `{PLAYER}`).
- **MessageIntervalSeconds:** Czas (w sekundach) pomiędzy wysyłaniem kolejnych wiadomości na czat.
- **Messages:** Lista wiadomości, które będą cyklicznie wyświetlane na serwerze (kolejno jedna po drugiej).

## Autor

Stworzone przez **tomSoNN**.
