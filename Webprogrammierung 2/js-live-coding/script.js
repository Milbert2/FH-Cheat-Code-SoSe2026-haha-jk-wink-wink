// Arrays zum Speichern aller geladenen Produkte
// und aller Produkte im Warenkorb
let products = [];
let cart = [];


// Holt wichtige Bereiche aus dem HTML,
// damit später Inhalte eingefügt werden können
const productsWrapper = document.querySelector(".products-wrapper");
const cartWrapper = document.querySelector(".products-in-cart");



// Lädt die Daten aus der products.json Datei
// Verwendet fetch() mit async/await
// Relevant für:
// - Angabe 1
// - Angabe 2
// - Angabe 3
async function loadProducts()
{
    try
    {
        // products.json laden
        const response = await fetch("./products.json");

        // JSON Daten in JavaScript Objekt umwandeln
        const data = await response.json();



        // Durchläuft jedes Produkt aus der JSON Datei
        data.forEach(product =>
        {

            // ==================================================
            // KATEGORIEN FILTER
            // ==================================================

            // ANGABE 1:
            // Kleidung, Handarbeit, Tech

            // ANGABE 2:
            // Möbel, Handarbeit, Kleidung

            // ANGABE 3:
            // Nur Tech

            if (
                product.category === "Kleidung" ||
                product.category === "Handarbeit" ||
                product.category === "Tech"
            )
            {
                // Produkt im products Array speichern
                products.push(product);

                // Produktkarte im HTML anzeigen
                createProductCard(product);
            }
        });



        // Gesamtanzahl aller Produkte anzeigen
        // Relevant für:
        // - Angabe 1
        // - Angabe 2
        showTotalShopProducts();
    }
    catch(error)
    {
        console.error("Fehler beim Laden:", error);
    }
}


// Startet das Laden der Produkte
loadProducts();



// Erstellt eine einzelne Produktkarte
// und fügt sie im Shop ein
// Relevant für:
// - Angabe 1
// - Angabe 2
// - Angabe 3
function createProductCard(product)
{
    // Äußere Produktkarte erstellen
    const productCard = document.createElement("div");

    productCard.classList.add("product");



    // ==================================================
    // PRODUKTBILD
    // ==================================================

    // Bild Element erstellen
    const img = document.createElement("img");



    // ==================================================
    // BILDPFAD
    // ==================================================

    // ANGABE 1:
    // product.image enthält bereits den kompletten Pfad
    // Beispiel:
    // /images/laptop.png

    // ANGABE 2:
    // product.image enthält KEINE Dateiendung
    // Deshalb muss .jpg oder .png ergänzt werden

    // ANGABE 3:
    // product.image enthält ebenfalls bereits den Pfad

    img.src = "." + product.image + ".jpg";



    // ==================================================
    // RESPONSIVE IMAGES MIT SRCSET
    // ==================================================

    // Relevant für:
    // - Angabe 1
    // - Angabe 2
    // - Angabe 3

    // Je nach Bildschirmgröße wird automatisch
    // eine passende Bildgröße geladen

    img.srcset = `
        .${product.image}-480w.jpg 480w,
        .${product.image}-768w.jpg 768w,
        .${product.image}-1200w.jpg 1200w
    `;

    // Definiert wann welche Bildgröße verwendet wird
    img.sizes = "(max-width: 768px) 100vw, 300px";


    // ==================================================
    // PRODUKTNAME
    // ==================================================

    const title = document.createElement("h3");

    // textContent verhindert XSS Angriffe
    // Relevant besonders für Angabe 3
    title.textContent = product.name;



    // ==================================================
    // PRODUKTBESCHREIBUNG
    // ==================================================

    // Relevant für:
    // - Angabe 1
    // - Angabe 3

    // Angabe 2 benötigt keine Beschreibung

    const description = document.createElement("p");

    description.classList.add("product-description");

    description.textContent = product.description;



    // ==================================================
    // PRODUKTPREIS
    // ==================================================

    const price = document.createElement("p");

    price.classList.add("product-price");

    // Preis in Zahl umwandeln
    // und immer mit 2 Nachkommastellen anzeigen
    price.textContent =
        parseFloat(product.price).toFixed(2) + " €";



    // ==================================================
    // ADD TO CART BUTTON
    // ==================================================

    const button = document.createElement("button");

    button.textContent = "Add to cart";



    // Klick Event:
    // Fügt das Produkt dem Warenkorb hinzu
    button.addEventListener("click", () =>
    {
        addToCart(product.id);
    });



    // ==================================================
    // ELEMENTE ZUR PRODUKTKARTE HINZUFÜGEN
    // ==================================================

    productCard.appendChild(img);

    productCard.appendChild(title);

    productCard.appendChild(description);

    productCard.appendChild(price);

    productCard.appendChild(button);



    // Produktkarte im Shop anzeigen
    productsWrapper.appendChild(productCard);
}



// Zeigt die Gesamtanzahl aller Produkte im Shop an
// Relevant für:
// - Angabe 1
// - Angabe 2
function showTotalShopProducts()
{
    // Neues Textelement erstellen
    const totalProducts = document.createElement("p");

    // Anzahl der Produkte anzeigen
    totalProducts.textContent =
        `Produkte im Shop: ${products.length}`;

    // Vor dem Shopbereich einfügen
    productsWrapper.before(totalProducts);
}



// Fügt ein Produkt dem Warenkorb hinzu
// Relevant für:
// - Angabe 1
// - Angabe 2
// - Angabe 3
function addToCart(id)
{
    // Passendes Produkt anhand der ID suchen
    const product =
        products.find(product => product.id === id);



    // Produkt im cart Array speichern
    cart.push(product);



    // ==================================================
    // WARENKORB EINTRAG ERSTELLEN
    // ==================================================

    const li = document.createElement("li");



    // ==================================================
    // PRODUKTNAME IM WARENKORB
    // ==================================================

    const name = document.createElement("span");

    name.classList.add("product-name");

    name.textContent = product.name;



    // ==================================================
    // PREIS IM WARENKORB
    // ==================================================

    const price = document.createElement("span");

    price.textContent =
        parseFloat(product.price).toFixed(2) + " €";



    // ==================================================
    // REMOVE BUTTON
    // ==================================================

    // Klick auf x entfernt Produkt wieder
    // Relevant für:
    // - Angabe 1
    // - Angabe 2
    // - Angabe 3

    const removeButton = document.createElement("span");

    removeButton.classList.add("cancel-order-button");

    removeButton.textContent = "x";



    // Klick Event zum Entfernen
    removeButton.addEventListener("click", () =>
    {
        removeFromCart(li, product);
    });



    // Inhalte zum Listenpunkt hinzufügen
    li.appendChild(name);

    li.appendChild(price);

    li.appendChild(removeButton);



    // Listenpunkt im Warenkorb anzeigen
    cartWrapper.appendChild(li);



    // Warenkorb Informationen aktualisieren
    updateCart();
}



// Entfernt ein Produkt aus dem Warenkorb
// Relevant für:
// - Angabe 1
// - Angabe 2
// - Angabe 3
function removeFromCart(li, product)
{
    // HTML Element entfernen
    li.remove();



    // Produkt aus dem cart Array entfernen
    const index = cart.indexOf(product);

    if(index > -1)
    {
        cart.splice(index, 1);
    }



    // Warenkorb neu berechnen
    updateCart();
}



// Aktualisiert Anzahl und Gesamtpreis
// des Warenkorbs
// Relevant für:
// - Angabe 2
// - Angabe 3
function updateCart()
{
    // ==================================================
    // GESAMTANZAHL DER PRODUKTE
    // ==================================================

    // Anzahl aller Produkte im Warenkorb anzeigen
    const totalProductsCount =
        document.querySelector(".total-products-count");

    totalProductsCount.textContent = cart.length;



    // ==================================================
    // GESAMTPREIS BERECHNEN
    // ==================================================

    // Relevant für:
    // - Angabe 3

    let totalPrice = 0;



    // Alle Preise im Warenkorb addieren
    cart.forEach(product =>
    {
        totalPrice += parseFloat(product.price);
    });



    // Gesamtpreis im HTML anzeigen
    const totalPriceElement =
        document.querySelector(".total-price");

    totalPriceElement.textContent =
        totalPrice.toFixed(2) + " €";
}
