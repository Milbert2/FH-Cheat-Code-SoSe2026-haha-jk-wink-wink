# JavaScript Live Coding: Warenkorb

## 1. Setup
*  HTML & CSS sind gegeben.
*  products.json ist gegeben. Sie gibt die Produktinformationen in einer JSON-Struktur aus.
*  Bilder liegen im Ordner images/

* Öffne deinen Editor und einen Browser. Schließe alle anderen Programme / Dateien.
* Ausnahme: Das Terminal und dein FTP-Programm darf zum Bearbeiten und Hochladen deines Projektes verwendet werden.
* Deaktiviere alle KI-basierten Tools in deinem Editor.

🚨 Der Editor darf nur das Warenkorb-Projekt zeigen.  
🚨 Der Browser darf nur zum Darstellen deines Warenkorb-Projekts verwendet werden.

## 2. Vorgehensweise
*  Die Übung besteht aus drei Aufgaben. 
*  Die Bearbeitungszeit beträgt 90 Minuten.
*  Keine Hilfe von AI erlaubt.  

## Abgabe
* Lade dein Projekt in den neuen Ordner `warenkorb/` deines Webspaces hoch.
*  Committe und Pushe **nach jeder Aufgabe** den aktuellen Stand in dein Repository (WebProg2Abgabe) in den Ordner warenkorb/.

## 3. Aufgabenstellungen
### 1. Produktdaten laden und richtig darstellen
    - Lade die Produktdaten aus products.json. Verwende fetch() und async/await.
    - Stelle die Produkte jeweils in einer Produktkarte dar. HTML & CSS dafür sind bereits angelegt. Inkludiere folgende Informationen: Name des Produkts, Preis, Bild, Produktbeschreibung, Button "Add to cart"
    - Zeige **NUR** die Produkte in den Kategorien "Kleidung", "Handarbeit" und "Tech"

    - products.json gibt ein Array zurück, das mit Produktobjekten gefüllt ist. Die Struktur dafür sieht so aus:
    `{
        "id": 1,
        "name": "Produktname",
        "description": "Das ist eine Produktbeschreibung.",
        "price": "25.99",
        "image": "/images/product.png",
        "category": "Schmuck"
    }`

### 2. Warenkorb-Funktionalität
    - Produkte können mehrmals zum Warenkorb hinzugefügt werden. Zeige sie in dem Fall einfach mehrmals an. Du musst nicht extra einen Counter pro Produkt anzeigen.
    - Sobald auf den "Add to cart" Button geklickt wird, erscheint das Produkt im Warenkorb.
    - Das Produkt erscheint in der seperaten Warenkorb-Seitenleiste.
    - Produkte, die sich bereits im Warenkorb befinden können auch wieder entfernt werden, indem man auf das "x" klickt. 

### 3. Informationen berechnen & Responsive Images
    - Gib die Gesamtanzahl aller Artikel aus, die im Shop angezeigt werden. 
    - Responsive Images in den Produktkarten. Verwende dafür srcset. Du findest im /images Ordner für jedes Bild vier verschiedene Versionen: 
    1. Fallback
    2. 480w
    3. 768w
    4. 1200w
    Das ist für jedes Bild gleich.


### Tipps und Hilfestellungen
* Speichere die Produkte im Warenkorb in einem Array.
* DOM-Manipulation erfolgt über JavaScript (nicht PHP).
* Nutze async/await korrekt für fetch()
* Achte darauf, Funktionen und Variablen verständlich zu benennen. In englischer Sprache.
* number.toFixed(n) gibt eine Zahl mit maximal n Kommastellen aus.
* parseFloat() um einen String in einen Float umzuwandeln