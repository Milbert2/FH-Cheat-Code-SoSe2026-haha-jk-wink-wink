import { getPenguins } from "./getPenguins.js";
import { TeamSelectionTool } from "./tool.js";

async function init() {
  const penguins = await getPenguins();

  const parentElement = document.getElementById("tool-wrapper");

  new TeamSelectionTool(penguins, parentElement);
}

init();