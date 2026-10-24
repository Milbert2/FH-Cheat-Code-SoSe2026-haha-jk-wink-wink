# Memory Game

Eine Coding Challenge als Nachprüfung im Fach "Webprogrammierung 2"
aus dem Sommersemester 2026.


## Rahmenbedingungen

- Die Coding Challenge besteht aus drei Teilen: PHP, Javascript, und Bonus Javascript
- Die Bearbeitungszeit beträgt 90 Minuten.

Es gelten strenge Anforderungen an deine Arbeitsumgebung:

- Zulässig: 1 Blatt, A4, alles selbst mit der Hand geschrieben, nicht ausgedruckt, nicht kopiert.
- Keine anderen Unterlagen (analog oder digital), keine Hilfestellungen oder bereits gelöste Übungen.
- Dein Editor darf nur dieses Projekt zeigen
- Dein Browser darf nur zum Darstellen dieses Projekts verwendet werden
    - alle Dateien Endpunkte sind ok
    - Developer Tools sind ok
- Alle AI-Features im Editor und Browser sind deaktiviert
- keine weiteren Geräte (Smartphone, AR-Brigitte, Kopfhörer, ...)
- der Ordner-Name im Webspace und im Repository ist memory-game/

### Zwischenabgabe

1. Committe und pushe oft (spätestens nach jedem Teil) den aktuellen Stand in dein Repository.
2. Lade das Projekt nach jedem Teil in den Webspace hoch.

### Endabgabe

1. Lade dein Projekt in deinen Webspaces hoch und
2. pushe den finalen Stand ins Abgabe-Repository.

---

## Setup

Entpacke die Zip-Datei, es entsteht der Ordner `/memory-game/`.
Verschiebe ihn an die richtige Stelle auf deinem Computer.

Betrachte zuerst die beiden Videos, sie Zeigen wie das Spiel aussehen soll:

* [Video: Spielablauf](memory-game.mp4)

Studiere die Dateien genau, es ist schon viel Code vorhanden.
Für deine drei Aufgaben musst du meist Code ergänzen
und manchmal vorhandenen Code ändern.

## Teil 1: Game-Setup Laden

Mit der Funktion `getSetup()` in der Datei `setup.js` werden Daten
geladen, die den gesamten Ablauf des Spieles bestimmen.

Derzeit wird eine Datei geladen: `example-setup.json`.

Die Arbeit an einem echten PHP Backend hat schon begonnen, es
gibt eine Datenbank mit einer Tabelle `game_pictures` am Server:

- Struktur siehe Datei `setup.sql`
- Echte Datenbank `hotel_db` am Server, Zugang mit Username `db1_reader` und geheimen Passwort

du kannst die datenbank inspizieren mit

    psql -U db1_reader -W -h users.ct.fh-salzburg.ac.at hotel_db

und es gibt schon ein paar Zeilen code in der Datei `setup.php`.


### Aufgabe: setup.php fertig stellen

Zuerst: erstelle config.php.

Dann: Ergänze das Programm `setup.php` so, dass es Daten aus der Datenbank holen kann
und als JSON ausliefern kann.  Das JSON soll immer die gleiche Struktur haben
wie `example-setup.json`, aber bei jedem Aufruf sollen zwei Aspekte zufällig sein:

- die Bilder im "grid" sollen zufällig aus allen Bildern in der Tabelle ausgewählt sein
- Fragen in "questions" sollen auch zufällig sein. Nur Bilder aus dem grid kommen in Frage.
- Bei den Fragen sollen nie zwei gleiche hintereinander sein. Hier wäre also das zweite "Gabel" nicht zulässig, das dritte wäre wieder ok:

  "questions": [
    "Kamera",
    "Gabel",
    "Gabel",
    "Tasse",
    "Gabel",
    ...
  ]

Probier das PHP-Programm direkt aus.

Nur wenn du dir sicher bist, dass es funktioniert:
ändere `setup.js` so, dass `setup.php` wirklich aufgerufen wird.

## Teil 2: Spiel fertig stellen

Ein Teil des Spiels ist bereits implementiert:
Das Spiel wird geladen, aber dann passiert nichts mehr.

Implementiere das Spiel - noch ohne Timer.

### Aufgabe a): Alle Bilder im grid anzeigen

In der Datei `initGrid()` fehlt das Zusammenbauen
der DOM-Elemente für das Anzeigen der Bilder.

Die Buttons sollen alle auf `click` die Funktion `handleClick` aufrufen.


### Aufgabe b): Frage anzeigen

Jetzt den Start-Button aktivieren, er soll `startGame()` aufrufen.

In  `startGame()` ist schon viel implementiert, aber `showQuestion()` muss
noch fertig programmiert werden.


### Aufgabe c): Click auf einen Button

Nun muss die Funktion `handleClick` fertig gestellt werden.

### Aufgabe d): Anzeige des Scores

Nun muss die Funktion  `displayScore()`  fertig gestellt werden.

### Aufgabe e): nächste Frage

Nun muss die Funktion  `nextQuestion()`  fertig gestellt werden.



## Teil 3 (Bonus): Timer einbauen

Mit Zeitdruck wird es schwieriger! Das könne so aussehen:

* [Video: Wenn die Zeit ausgeht](memory-game-timesup.mp4)


### Aufgabe a): Timer starten

### Aufgabe b): Sekunden anzeigen

### Aufgabe c): Timeout = Spiel endet

