export class TeamSelectionTool {
  constructor(penguins, parentElement) {
    this.penguins = penguins;
    this.parentElement = parentElement;

    this.team = [];

    this.MAX_COOKS_ALLOWED = 3;

    this.penguinsWrapper = document.getElementById("penguins-wrapper");
    this.selectedPenguinsWrapper = document.getElementById(
      "selected-penguins-wrapper"
    );

    this.teamStatus = document.createElement("p");
    this.teamStatus.classList.add("team-status");

    const teamWrapper = document.getElementById("team-wrapper");
    teamWrapper.insertBefore(
      this.teamStatus,
      this.selectedPenguinsWrapper
    );

    this.updateTeamStatus();

    this.displayPenguins();
  }

  displayPenguins() {
    this.penguins.forEach((penguin) => {
      const card = document.createElement("div");
      card.classList.add("penguin-card");

      const image = document.createElement("img");
      image.src = penguin.imageUrl;
      image.alt = penguin.name;

      const title = document.createElement("h2");
      title.textContent = penguin.name;

      const backstory = document.createElement("p");
      backstory.textContent = penguin.backstory;

      const button = document.createElement("button");
      button.classList.add("select-button");
      button.textContent = "Auswählen";

      button.addEventListener("click", () => {
        this.selectPenguin(penguin, button, card);
      });

      card.append(image, title, backstory, button);

      this.penguinsWrapper.append(card);
    });
  }

  selectPenguin(penguin, button, card) {
  
    const alreadySelected = this.team.find(
      (member) => member.name === penguin.name
    );

    if (alreadySelected) {
      return;
    }

    if (this.team.length >= this.MAX_COOKS_ALLOWED) {
      return;
    }

    this.team.push(penguin);

    button.disabled = true;
    button.textContent = "Bereits ausgewählt";

    card.classList.add("penguin-selected");

    this.renderSelectedPenguins();
    this.updateTeamStatus();

    if (this.team.length >= this.MAX_COOKS_ALLOWED) {
      const buttons = document.querySelectorAll(".select-button");

      buttons.forEach((btn) => {
        btn.disabled = true;
      });
    }
  }

  renderSelectedPenguins() {
    this.selectedPenguinsWrapper.innerHTML = "";

    this.team.forEach((penguin) => {
      const card = document.createElement("div");

      card.classList.add("penguin-card", "team-member");

      const image = document.createElement("img");
      image.src = penguin.imageUrl;
      image.alt = penguin.name;

      const title = document.createElement("h2");
      title.textContent = penguin.name;

      card.append(image, title);

      this.selectedPenguinsWrapper.append(card);
    });
  }

  updateTeamStatus() {
    const remaining = this.MAX_COOKS_ALLOWED - this.team.length;

    if (remaining > 0) {
      this.teamStatus.textContent = `${remaining} Plätze frei`;
      this.teamStatus.classList.remove("team-full");
    } else {
      this.teamStatus.textContent =
        "Das Team steht fest. Viel Erfolg beim Bewerb!";
      this.teamStatus.classList.add("team-full");
    }
  }
}