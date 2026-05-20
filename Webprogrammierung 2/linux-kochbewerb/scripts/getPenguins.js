const ENDPOINT =
  "https://users.ct.fh-salzburg.ac.at/~fhs47779/cooking-competition/penguins.php";

export async function getPenguins() {
  try {
    const response = await fetch(ENDPOINT);

    if (!response.ok) {
      throw new Error("Fehler beim Laden der Pinguine");
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error(error);
    return [];
  }
}