let products = [];
let cart = [];

let cartWrapper = document.querySelector(".products-in-cart");

loadProducts();

async function loadProducts()
{
    let response = await fetch("./products.json");
    let productsData = await response.json();

    for (let i = 1; i <= 20; i++)
    {
        if(productsData[i].category == "Tech" || productsData[i].category == "Möbel" || productsData[i].category == "Handarbeit")
        {
            let product = new Product(i, productsData[i], document.querySelector(".products-wrapper"));
            products[i-1] = product;            
        }
    }
}

class Product
{
    constructor(id, data, parent)
    {
        this.id = id;
        this.name = data.name;
        this.description = data.description;
        this.price = data.price;
        this.image = data.image;
        this.category = data.category;
        this.parent = parent;

        this.createProduct();
    }

    createProduct()
    {
        let productDiv = document.createElement("div");
        productDiv.classList.add("product");

        let productImg = document.createElement("img");
        productImg.src = "." + this.image + ".jpg";
        productImg.srcset = "." + this.image + "-480w.jpg 480w, ." + this.image + "-768w.jpg 768w, ." + this.image + "-1200.jpg 1200w";
        productImg.sizes = "250px";

        let productTitle = document.createElement("h3");
        productTitle.innerText = this.name;

        let productDescription = document.createElement("p");
        productDescription.classList.add("product-description");

        let productPrice = document.createElement("p");
        productPrice.classList.add("product-productPrice");
        productPrice.innerText = this.price + " €";

        let btn = document.createElement("button");
        btn.innerText = "Add to Cart";
        btn.id = this.id;
        btn.setAttribute("style", "cursor: pointer;");

        btn.addEventListener("click", () => {addToCart(this.id);})

        productDiv.appendChild(productImg);
        productDiv.appendChild(productTitle);
        productDiv.appendChild(productDescription);
        productDiv.appendChild(productPrice);
        productDiv.appendChild(btn);

        this.parent.appendChild(productDiv);
    }
}

function calcTotalPrice()
{
    let totalPrice = 0;

    cart.forEach(cProduct => {
        totalPrice += cProduct.price;
    });

    document.querySelector(".total-price").innerText = totalPrice.toFixed(2) + " €";
}

function updateCartCount()
{
    document.querySelector(".total-products-count").innerText = cart.length;
}

function deleteFromCart(event)
{
    event.target.parentNode.remove();
    cart.splice(event.target.parentNode.id-1, 1);

    let count = 1;
    document.querySelectorAll(".products-in-cart li").forEach(element => {
        element.id = count;
        count++;
    });

    updateCartCount();
    calcTotalPrice();
}

function addToCart(id)
{
    let product = products[id-1];

    cart.push(product);

    let productLi = document.createElement("li");
    productLi.id = cart.length;

    let pName = document.createElement("span");
    pName.classList.add("product-name");
    pName.innerText = product.name;

    let pPrice = document.createElement("span");
    pPrice.innerText = product.price + " €";

    let pCancel = document.createElement("span");
    pCancel.innerText = "x";
    pCancel.classList.add("cancel-order-button");
    pCancel.addEventListener("click", (event) => {deleteFromCart(event)});
    pCancel.setAttribute("style", "cursor: pointer;");

    productLi.appendChild(pName);
    productLi.appendChild(pPrice);
    productLi.appendChild(pCancel);

    cartWrapper.appendChild(productLi);

    updateCartCount();
    calcTotalPrice();
}