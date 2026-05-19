// Wartet bis das komplette HTML geladen wurde.
// Erst DANACH wird die Funktion init() gestartet.
//
// document
// = die gesamte HTML Seite
//
// addEventListener()
// = hört auf ein Event
//
// "DOMContentLoaded"
// = Event das ausgelöst wird,
//   sobald das HTML fertig geladen ist
document.addEventListener("DOMContentLoaded", () =>
{
    init();
});



// ==================================================
// ARRAYS
// ==================================================

// Array für alle Produkte aus der JSON Datei
//
// let
// = Variable die später verändert werden darf
//
// []
// = leeres Array
let products = [];



// Array für Produkte im Warenkorb
let cart = [];



// ==================================================
// HTML ELEMENTE HOLEN
// ==================================================

// Sucht das HTML Element mit der Klasse
// ".products-wrapper"
//
// document.querySelector()
// = sucht ein HTML Element
//
// ".products-wrapper"
// = CSS Klassenname
//
// Das Element wird später verwendet,
// um die Produktkarten einzufügen
const productsWrapper =
    document.querySelector(".products-wrapper");



// Holt die Warenkorb Liste aus dem HTML
const cartWrapper =
    document.querySelector(".products-in-cart");



// Holt das Element für die Anzahl
// der Produkte im Warenkorb
const totalProductsCount =
    document.querySelector(".total-products-count");



// Holt das Element für den Gesamtpreis
const totalPrice =
    document.querySelector(".total-price");



// ==================================================
// INIT
// ==================================================

// async
// = erlaubt die Verwendung von await
//
// init()
// = Startfunktion der App
//
// Wird einmal beim Laden der Seite ausgeführt
async function init()
{
    // Wartet bis Produkte geladen wurden
    //
    // await
    // = wartet bis Promise fertig ist
    await loadProducts();
}



// ==================================================
// PRODUKTE LADEN
// ==================================================

// Lädt die JSON Datei mit fetch()
async function loadProducts()
{
    // fetch()
    // = lädt Daten von einer Datei oder API
    //
    // "./products.json"
    // = Datei im selben Ordner
    const response =
        await fetch("./products.json");



    // response.json()
    // = wandelt JSON in JavaScript Objekte um
    //
    // data enthält danach ein Array
    // mit allen Produkten
    const data =
        await response.json();



    // forEach()
    // = läuft durch jedes Element im Array
    //
    // product
    // = aktuelles Produkt
    data.forEach(product =>
    {
        // Nur bestimmte Kategorien anzeigen
        //
        // ||
        // = ODER
        //
        // Wenn eine Bedingung true ist,
        // wird das Produkt angezeigt
        if(
            product.category === "Kleidung" ||
            product.category === "Handarbeit" ||
            product.category === "Tech"
        )
        {
            // Produkt im products Array speichern
            //
            // push()
            // = Element zu Array hinzufügen
            products.push(product);



            // Produktkarte erstellen
            createProduct(product);
        }
    });



    // ==================================================
    // PRODUKTANZAHL IM SHOP ANZEIGEN
    // ==================================================

    // Neues HTML Element erstellen
    const count =
        document.createElement("p");



    // textContent
    // = Text in Element schreiben
    //
    // `${}`
    // = JavaScript Wert in String einsetzen
    count.textContent =
        `Produkte im Shop: ${products.length}`;



    // before()
    // = Element vor einem anderen Element einfügen
    productsWrapper.before(count);
}

/* Verwendet Backend aus dem Web statt JSON File 

async function loadProducts()
{
    try
    {
        // Daten von einer externen API holen
        const response =
            await fetch("https://example.com/api/products");

        // Antwort in JS-Objekt umwandeln
        const data =
            await response.json();



        data.forEach(product =>
        {
            if(
                product.category === "Kleidung" ||
                product.category === "Handarbeit" ||
                product.category === "Tech"
            )
            {
                products.push(product);

                createProduct(product);
            }
        });



        // Anzahl anzeigen
        const count =
            document.createElement("p");

        count.textContent =
            `Produkte im Shop: ${products.length}`;

        productsWrapper.before(count);
    }
    catch(error)
    {
        console.error("Fehler beim Laden der API:", error);
    }
}
    */

// ==================================================
// PRODUKTKARTE ERSTELLEN
// ==================================================

// Erstellt eine Produktkarte für EIN Produkt
function createProduct(product)
{
    // Neues div erstellen
    const div =
        document.createElement("div");



    // ==================================================
    // BILD
    // ==================================================

    // img Element erstellen
    const img =
        document.createElement("img");



    // src
    // = Bildpfad
    //
    // "." davor:
    // macht aus:
    // /images/laptop
    //
    // => ./images/laptop
    //
    // + ".jpg"
    // ergänzt die Dateiendung
    img.src =
        "." + product.image + ".jpg";



    // ==================================================
    // RESPONSIVE IMAGES
    // ==================================================

    // srcset
    // = verschiedene Bildgrößen
    //
    // Browser entscheidet automatisch,
    // welches Bild geladen wird
    img.srcset = `
        .${product.image}-480w.jpg 480w,
        .${product.image}-768w.jpg 768w,
        .${product.image}-1200w.jpg 1200w
    `;



    // sizes
    // = sagt dem Browser wie groß
    // das Bild ungefähr angezeigt wird
    img.sizes =
        "(max-width: 768px) 100vw, 300px";



    // ==================================================
    // NAME
    // ==================================================

    // h3 Überschrift erstellen
    const title =
        document.createElement("h3");



    // Produktname einsetzen
    title.textContent =
        product.name;



    // ==================================================
    // BESCHREIBUNG
    // ==================================================

    const description =
        document.createElement("p");



    // Produktbeschreibung anzeigen
    description.textContent =
        product.description;



    // ==================================================
    // PREIS
    // ==================================================

    const price =
        document.createElement("p");



    // parseFloat()
    // = String in Zahl umwandeln
    //
    // toFixed(2)
    // = immer 2 Nachkommastellen
    //
    // Beispiel:
    // 25 => 25.00
    price.textContent =
        parseFloat(product.price)
            .toFixed(2) + " €";



    // ==================================================
    // BUTTON
    // ==================================================

    const button =
        document.createElement("button");



    // Text auf Button
    button.textContent =
        "Add to cart";



    // EventListener für Klick
    //
    // Wenn geklickt wird:
    // addToCart(product)
    button.addEventListener("click", () =>
    {
        addToCart(product);
    });



    // ==================================================
    // ALLES ZUR KARTE HINZUFÜGEN
    // ==================================================

    // appendChild()
    // = HTML Element in anderes Element einfügen
    div.appendChild(img);

    div.appendChild(title);

    div.appendChild(description);

    div.appendChild(price);

    div.appendChild(button);



    // Produktkarte im HTML anzeigen
    productsWrapper.appendChild(div);
}



// ==================================================
// PRODUKT ZUM WARENKORB
// ==================================================

function addToCart(product)
{
    // Produkt im cart Array speichern
    cart.push(product);



    // Neues Listenelement erstellen
    const li =
        document.createElement("li");



    // ==================================================
    // NAME
    // ==================================================

    const name =
        document.createElement("span");



    // Produktname anzeigen
    name.textContent =
        product.name;



    // ==================================================
    // PREIS
    // ==================================================

    const price =
        document.createElement("span");



    // Preis anzeigen
    price.textContent =
        parseFloat(product.price)
            .toFixed(2) + " €";



    // ==================================================
    // REMOVE BUTTON
    // ==================================================

    const remove =
        document.createElement("span");



    // x anzeigen
    remove.textContent = "x";



    // Klick auf x
    remove.addEventListener("click", () =>
    {
        // HTML Element entfernen
        li.remove();



        // indexOf()
        // = findet Position im Array
        const index =
            cart.indexOf(product);



        // splice()
        // = entfernt Element aus Array
        //
        // splice(start, anzahl)
        //
        // Beispiel:
        // splice(2,1)
        // = entferne 1 Element ab Position 2
        cart.splice(index, 1);



        // Warenkorb neu berechnen
        updateCart();
    });



    // ==================================================
    // ELEMENTE ZUSAMMENBAUEN
    // ==================================================

    li.appendChild(name);

    li.appendChild(price);

    li.appendChild(remove);



    // Produkt im Warenkorb anzeigen
    cartWrapper.appendChild(li);



    // Warenkorb aktualisieren
    updateCart();
}



// ==================================================
// WARENKORB AKTUALISIEREN
// ==================================================

function updateCart()
{
    // ==================================================
    // ANZAHL AKTUALISIEREN
    // ==================================================

    // Anzahl der Produkte im Warenkorb anzeigen
    //
    // cart.length
    // = Anzahl Elemente im Array
    totalProductsCount.textContent =
        cart.length;



    // ==================================================
    // GESAMTPREIS BERECHNEN
    // ==================================================

    // Variable für Gesamtsumme
    let sum = 0;



    // Alle Produkte im Warenkorb durchlaufen
    cart.forEach(product =>
    {
        // Preis addieren
        sum +=
            parseFloat(product.price);
    });



    // Gesamtpreis anzeigen
    totalPrice.textContent =
        sum.toFixed(2) + " €";
}
