function showGameHUDMessage(type, title, message) {

    const hud = document.getElementById("gameHudMessage");
    const hudTitle = document.getElementById("hudTitle");
    const hudMessage = document.getElementById("hudMessage");

    hud.classList.remove("hud-success", "hud-error", "hud-warning");

    hudTitle.textContent = title;
    hudMessage.textContent = message;

    hud.classList.add("hud-" + type);

    // show
    hud.classList.add("show");

    // auto hide after 3 seconds
    setTimeout(() => {
        hud.classList.remove("show");
    }, 3000);

}