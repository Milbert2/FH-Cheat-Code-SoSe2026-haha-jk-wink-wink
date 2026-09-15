# Leaderboard mit PHP & JavaScript (Live-Coding)

## 1. Setup
* HTML & CSS sind gegeben
* Datenbank-Schema scores.sql ist gegeben und enthält eine Tabelle scores(id, player, points, played_at, avatar). Das haben wir für dich bereits am Server angelegt. Mit dem Username "db1_reader" und dem Passwort "geheim!" kannst du darauf zugreifen. Verwende die Datenbank am Server, um diese Aufgabe zu lösen. So funktioniert am Ende alles, wenn du deine Lösung auf den Webspace hochlädst.
* Öffne deinen Editor und einen Browser. Zusätzlich kannst du dein Terminal öffnen, falls du es brauchst. Auch dein SFTP Programm darf zum Hochladen geöffnet sein.Schließe alle anderen Programme/Dateien.
* Deaktiviere alle KI-Tools in deinem Editor.

🚨 Dein Editor darf nur das Leaderboard-Projekt zeigen.
🚨 Der Browser darf nur zum Darstellen des Leaderboard-Projekts verwendet werden.

## 2. Vorgehensweise
* Die Übung besteht aus drei Aufgaben
* Die Bearbeitungszeit beträgt 90 Minuten.
* Keine Hilfe von KI erlaubt.

## 3. Abgabe
* Lade dein Projekt in den Ordner leaderboard/ deines Webspaces hoch.
* Committe und pushe nach jeder Aufgabe den aktuellen Stand in dein Repository (WebProg2Abgabe) in den Ordner /leaderboard.

## 4. Aufgabenstellungen
### 1. Daten laden & darstellen
* list.php:
    * Lade alle Scores aus der Datenbank
    * Erlaube eine Sortierung über den GET-Parameter ?orderby=points|date|player
    * Gib die Scores als JSON zurück
    * Stelle sicher, dass orderby validiert wird (Whitelist)
    * Für Player sortiere einfach **aufsteigend nach der ID**, für Points sortiere **absteigend nach den Punkten** und für Date sortiere **absteigend nach dem Datum**.
* index.js:
    * Lade die Daten mit fetch() (async/await)
    * Stelle sie als Tabelle/Liste dar (HTML und CSS sind bereits gegeben)
    * Jede Karte enthält: ID, Avatar, Player, Punkte, Datum
    * alt-Attribut des Avatar-Bildes entspricht dem Spielernamen

### 2. Sortierung mit Radio-Buttons
* Im Frontend gibt es drei Radio Buttons (HTML dafür ist schon in der Angabe)
    * Sort by Player (Spieler)
    * Sort by Points (Anzahl der Punkte)
    * Sort by Date (Datum)
* Beim Klick auf einen Radio Button
    * Neuer fetch()-Request mit passendem orderby Parameter
    * Tabelle/Liste wird neu gerendert

### Tipps und Hilfestellungen
* Verwende $_GET['orderby'] und prüfe den Wert gegen eine Whitelist (['player', 'points', 'date'])
* PHP: in_array(wert, array) prüft, ob ein Wert in einem Array existiert und gibt einen Boolean zurück. Verwende die header() Funktion, um den richtigen Content-Type anzugeben und json_encode, um JSON auszugeben.
* Um zu vermeiden, dass für "orderby" direkter User-Input in die Query kommt, baue stattdessen eine "Map", die für jede erlaubte Option das exakte Order-By-Statement enthält. Z.B. 'points' => 'points DESC'
* Baue ein Array $scores mit den Daten, die du erhältst und gib es als JSON zurück, damit du es mit JavaScript weiterverarbeiten kannst.
* Fehlerbehandlung! Was passiert, wenn die Datenbank nichts zurückgibt?
* Gib orderby über die URL mit. Setze einen Default-Wert.



