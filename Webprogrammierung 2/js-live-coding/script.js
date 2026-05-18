// Produkte und Warenkorb speichern
let products = [];
let cart = [];

// Elemente aus dem DOM
const productsWrapper = document.querySelector(".products-wrapper");
const cartWrapper = document.querySelector(".products-in-cart");


// ===============================
// PRODUKTE LADEN
// ===============================
async function loadProducts()
{
    const response = await fetch("./products.json");

    const data = await response.json();

    data.forEach(productData =>
    {
        // Nur gewünschte Kategorien anzeigen
        if (
        productData.category === "Kleidung" ||
        productData.category === "Möbel" ||
        productData.category === "Handarbeit"
        )
        {
            products.push(productData);

            createProductCard(productData);
        }
    });
}

// Produkte laden
loadProducts();

// ===============================
// PRODUKTKARTE ERSTELLEN
// ===============================
function createProductCard(product)
{
    const productDiv = document.createElement("div");
    productDiv.classList.add("product");



    // Bild
    const img = document.createElement("img");

    img.src = "." + product.image + ".jpg";

    img.srcset = `
        .${product.image}-480w.jpg 480w,
        .${product.image}-768w.jpg 768w,
        .${product.image}-1200w.jpg 1200w
    `;

    img.sizes = "(max-width: 768px) 100vw, 250px";



    // Titel
    const title = document.createElement("h3");
    title.innerText = product.name;



    // Beschreibung
    const description = document.createElement("p");
    description.classList.add("product-description");
    description.innerText = product.description;



    // Preis
    const price = document.createElement("p");
    price.classList.add("product-price");
    price.innerText = Number(product.price).toFixed(2) + " €";

    // Button
    const button = document.createElement("button");
    button.innerText = "Add to cart";

    button.addEventListener("click", () =>
    {
        addToCart(product.id);
    });



    // Alles anhängen
    productDiv.appendChild(img);
    productDiv.appendChild(title);
    productDiv.appendChild(description);
    productDiv.appendChild(price);
    productDiv.appendChild(button);

    productsWrapper.appendChild(productDiv);
}



// ===============================
// ZUM WARENKORB HINZUFÜGEN
// ===============================
function addToCart(id)
{
    const product = products.find(product => product.id === id);

    cart.push(product);



    // Listenelement
    const li = document.createElement("li");



    // Name
    const name = document.createElement("span");
    name.classList.add("product-name");
    name.innerText = product.name;



    // Preis
    const price = document.createElement("span");
    price.innerText = Number(product.price).toFixed(2) + " €";



    // Entfernen Button
    const remove = document.createElement("span");
    remove.classList.add("cancel-order-button");
    remove.innerText = "x";

    remove.addEventListener("click", () =>
    {
        removeFromCart(li, product);
    });



    li.appendChild(name);
    li.appendChild(price);
    li.appendChild(remove);

    cartWrapper.appendChild(li);



    updateCart();
}

// ===============================
// AUS WARENKORB ENTFERNEN
// ===============================
function removeFromCart(li, product)
{
    li.remove();

    const index = cart.indexOf(product);

    if (index > -1)
    {
        cart.splice(index, 1);
    }

    updateCart();
}



// ===============================
// WARENKORB AKTUALISIEREN
// ===============================
function updateCart()
{
    // Anzahl
    document.querySelector(".total-products-count").innerText =
        cart.length;



    // Gesamtpreis
    let totalPrice = 0;

    cart.forEach(product =>
    {
        totalPrice += Number(product.price);
    });

    document.querySelector(".total-price").innerText =
        totalPrice.toFixed(2) + " €";
}

document.addEventListener("DOMContentLoaded", () => {
    const marketplaceWrapper = document.getElementById(".marketplace-wrapper");
})