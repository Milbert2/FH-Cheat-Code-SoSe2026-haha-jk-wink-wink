import { GameApp } from './game.js';
import { getSetup } from './setup.js';

document.addEventListener('DOMContentLoaded', async () => {
  const setup = await getSetup();

  console.log(setup);

  if(!setup || !setup.grid || !setup.questions) {
    console.error("Failed to load setup data:", setup);
    return;
  }

  new GameApp(setup, document.getElementById("game"));
});
